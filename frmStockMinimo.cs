using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmStockMinimo : Form
    {
        private TextBox txtBuscar;
        private DataGridView grdStock;

        public frmStockMinimo()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Label lblBuscar = new Label();
            Button btnBuscar = new Button();
            Button btnGuardar = new Button();
            txtBuscar = new TextBox();
            grdStock = new DataGridView();

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(16, 19);
            lblBuscar.Text = "Buscar:";

            txtBuscar.Location = new Point(70, 16);
            txtBuscar.Size = new Size(360, 20);

            btnBuscar.Location = new Point(442, 12);
            btnBuscar.Size = new Size(105, 28);
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;

            btnGuardar.Location = new Point(560, 12);
            btnGuardar.Size = new Size(140, 28);
            btnGuardar.Text = "Guardar minimo";
            btnGuardar.Click += btnGuardar_Click;

            grdStock.AllowUserToAddRows = false;
            grdStock.AllowUserToDeleteRows = false;
            grdStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdStock.Location = new Point(16, 52);
            grdStock.Size = new Size(780, 430);

            ClientSize = new Size(812, 498);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnGuardar);
            Controls.Add(grdStock);
            Name = "frmStockMinimo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configurar stock minimo";
            Load += frmStockMinimo_Load;
        }

        private void frmStockMinimo_Load(object sender, EventArgs e)
        {
            CargarStock();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarStock();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdStock.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un producto.");
                    return;
                }

                grdStock.EndEdit();
                DataRowView row = (DataRowView)grdStock.CurrentRow.DataBoundItem;
                clsStockRepository.ActualizarStockMinimo(Convert.ToInt64(row["IdStock"]), Convert.ToInt32(row["StockMinimo"]));
                MessageBox.Show("Stock minimo actualizado correctamente.");
                CargarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar stock minimo: " + ex.Message);
            }
        }

        private void CargarStock()
        {
            grdStock.DataSource = clsStockRepository.ListarStockMinimo(txtBuscar.Text.Trim());
            BloquearColumnas();
        }

        private void BloquearColumnas()
        {
            foreach (DataGridViewColumn column in grdStock.Columns)
            {
                column.ReadOnly = column.Name != "StockMinimo";
            }

            if (grdStock.Columns.Contains("IdStock"))
            {
                grdStock.Columns["IdStock"].Visible = false;
            }
        }
    }
}
