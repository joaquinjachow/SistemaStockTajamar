using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmHistorialMovimientos : Form
    {
        private TextBox txtBuscar;
        private TextBox txtCliente;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private CheckBox chkDesde;
        private CheckBox chkHasta;
        private ComboBox cmbTipo;
        private ComboBox cmbSede;
        private ComboBox cmbRubro;
        private Button btnExportar;
        private DataGridView grdMovimientos;

        public frmHistorialMovimientos()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Label lblBuscar = new Label();
            Label lblTipo = new Label();
            Label lblSede = new Label();
            Label lblRubro = new Label();
            Label lblCliente = new Label();
            Button btnBuscar = new Button();
            btnExportar = new Button();
            txtBuscar = new TextBox();
            txtCliente = new TextBox();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            chkDesde = new CheckBox();
            chkHasta = new CheckBox();
            cmbTipo = new ComboBox();
            cmbSede = new ComboBox();
            cmbRubro = new ComboBox();
            grdMovimientos = new DataGridView();

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(16, 17);
            lblBuscar.Text = "Buscar:";

            txtBuscar.Location = new Point(70, 14);
            txtBuscar.Size = new Size(250, 20);

            chkDesde.AutoSize = true;
            chkDesde.Location = new Point(335, 16);
            chkDesde.Text = "Desde";
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(392, 14);
            dtpDesde.Size = new Size(95, 20);

            chkHasta.AutoSize = true;
            chkHasta.Location = new Point(500, 16);
            chkHasta.Text = "Hasta";
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(555, 14);
            dtpHasta.Size = new Size(95, 20);

            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(16, 49);
            lblTipo.Text = "Tipo:";
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Items.AddRange(new object[] { "Todos", "Alta", "Ingreso", "Egreso", "Ajuste", "Transferencia" });
            cmbTipo.Location = new Point(70, 46);
            cmbTipo.Size = new Size(150, 21);

            lblSede.AutoSize = true;
            lblSede.Location = new Point(240, 49);
            lblSede.Text = "Sede:";
            cmbSede.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSede.Location = new Point(285, 46);
            cmbSede.Size = new Size(140, 21);

            lblRubro.AutoSize = true;
            lblRubro.Location = new Point(445, 49);
            lblRubro.Text = "Rubro:";
            cmbRubro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRubro.Items.AddRange(new object[] { "Todos", "Pino", "MaderaDura", "Machimbre", "Fenolicos" });
            cmbRubro.Location = new Point(495, 46);
            cmbRubro.Size = new Size(155, 21);

            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(668, 49);
            lblCliente.Text = "Cliente:";
            txtCliente.Location = new Point(720, 46);
            txtCliente.Size = new Size(210, 20);

            btnBuscar.Location = new Point(668, 12);
            btnBuscar.Size = new Size(105, 28);
            btnBuscar.Text = "Filtrar";
            btnBuscar.Click += btnBuscar_Click;

            btnExportar.Location = new Point(785, 12);
            btnExportar.Size = new Size(145, 28);
            btnExportar.Text = "Exportar Excel";
            btnExportar.Click += btnExportar_Click;

            grdMovimientos.AllowUserToAddRows = false;
            grdMovimientos.AllowUserToDeleteRows = false;
            grdMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdMovimientos.Location = new Point(16, 82);
            grdMovimientos.ReadOnly = true;
            grdMovimientos.Size = new Size(940, 470);

            ClientSize = new Size(972, 568);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(chkDesde);
            Controls.Add(dtpDesde);
            Controls.Add(chkHasta);
            Controls.Add(dtpHasta);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
            Controls.Add(lblSede);
            Controls.Add(cmbSede);
            Controls.Add(lblRubro);
            Controls.Add(cmbRubro);
            Controls.Add(lblCliente);
            Controls.Add(txtCliente);
            Controls.Add(btnBuscar);
            Controls.Add(btnExportar);
            Controls.Add(grdMovimientos);
            Name = "frmHistorialMovimientos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Historial de movimientos";
            Load += frmHistorialMovimientos_Load;
        }

        private void frmHistorialMovimientos_Load(object sender, EventArgs e)
        {
            cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
            cmbSede.DisplayMember = "Nombre";
            cmbTipo.SelectedIndex = 0;
            cmbRubro.SelectedIndex = 0;
            CargarMovimientos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarMovimientos();
        }

        private void CargarMovimientos()
        {
            if (!FechasValidas())
            {
                return;
            }

            grdMovimientos.DataSource = clsStockRepository.ListarMovimientos(txtBuscar.Text.Trim(), FechaDesde(), FechaHasta(), TipoSeleccionado(), cmbSede.Text, RubroSeleccionado(), txtCliente.Text.Trim());
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FechasValidas())
                {
                    return;
                }

                string archivo = clsReportes.GenerarReporteHistorial(txtBuscar.Text.Trim(), FechaDesde(), FechaHasta(), TipoSeleccionado(), cmbSede.Text, RubroSeleccionado(), txtCliente.Text.Trim());
                MessageBox.Show("Reporte de historial generado correctamente: " + archivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte de historial: " + ex.Message);
            }
        }

        private bool FechasValidas()
        {
            if (chkDesde.Checked && chkHasta.Checked && dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.");
                return false;
            }

            return true;
        }

        private string FechaDesde()
        {
            return chkDesde.Checked ? dtpDesde.Value.ToString("yyyy-MM-dd") : string.Empty;
        }

        private string FechaHasta()
        {
            return chkHasta.Checked ? dtpHasta.Value.ToString("yyyy-MM-dd") : string.Empty;
        }

        private string TipoSeleccionado()
        {
            return cmbTipo.Text == "Todos" ? string.Empty : cmbTipo.Text;
        }

        private string RubroSeleccionado()
        {
            return cmbRubro.Text == "Todos" ? string.Empty : cmbRubro.Text;
        }
    }
}
