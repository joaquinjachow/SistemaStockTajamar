using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmListaStockPino : Form
    {
        private clsPino pino;
        private ComboBox cmbSede;
        private TextBox txtBuscar;

        public frmListaStockPino()
        {
            InitializeComponent();
            pino = new clsPino();
            ConfigurarFiltrosDemo();
            clsUi.Aplicar(this);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarStock();
        }

        private void frmListaStockMaderas_Load(object sender, EventArgs e)
        {
            try
            {
                cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
                cmbSede.DisplayMember = "Nombre";
                cmbSede.SelectedIndex = cmbSede.FindStringExact(clsDatabase.SedeCordoba);
                cmbSecado.DataSource = pino.ObtenerSecadoPino();
                cmbSecado.DisplayMember = "Secado";
                cmbPino.DataSource = pino.ObtenerMedidasPino();
                cmbPino.DisplayMember = "Medida";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las medidas y secado de maderas: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbPino.Text;
                string secado = cmbSecado.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(secado))
                {
                    MessageBox.Show("Seleccione secado y medida antes de agregar stock.");
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad valida.");
                    txtCantidad.Focus();
                    return;
                }

                if (!SedeValidaParaMovimiento())
                {
                    return;
                }

                pino.SumarCantidadPaquetes(medida, secado, cantidad, SedeSeleccionada);
                MessageBox.Show("Cantidad de paquetes agregada correctamente.");
                txtCantidad.Text = "";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cantidad de paquetes: " + ex.Message);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbPino.Text;
                string secado = cmbSecado.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(secado))
                {
                    MessageBox.Show("Seleccione secado y medida antes de restar stock.");
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad valida.");
                    txtCantidad.Focus();
                    return;
                }

                if (!SedeValidaParaMovimiento())
                {
                    return;
                }

                using (frmRegistrarEgreso egreso = new frmRegistrarEgreso())
                {
                    if (egreso.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    pino.RestarCantidadPaquetes(medida, secado, cantidad, SedeSeleccionada, egreso.Detalle, egreso.ClienteId);
                }

                MessageBox.Show("Cantidad de paquetes restada correctamente.");
                txtCantidad.Text = "";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restar cantidad de paquetes: " + ex.Message);
            }
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await pino.GenerarReportePinoAsync(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnWhatsApp_Click(object sender, EventArgs e)
        {
            pino.EnviarStockWhatsApp(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void cmbSecado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string secadoSeleccionado = cmbSecado.Text;
                DataTable medidasFiltradas = pino.ObtenerMedidasPorSecado(secadoSeleccionado);
                cmbPino.DataSource = medidasFiltradas;
                cmbPino.DisplayMember = "Medida";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar las medidas por secado: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbPino.Text;
                string secado = cmbSecado.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(secado))
                {
                    MessageBox.Show("Seleccione secado y medida antes de eliminar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("Seguro que desea eliminar el pino seleccionado?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                pino.EliminarPino(medida, secado);
                MessageBox.Show("Pino eliminado correctamente.");
                txtCantidad.Text = "";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el paquete: " + ex.Message);
            }
        }

        private string SedeSeleccionada
        {
            get { return cmbSede == null ? clsStockRepository.SedeTotal : cmbSede.Text; }
        }

        private void ConfigurarFiltrosDemo()
        {
            Label lblSede = new Label();
            Label lblBuscar = new Label();
            cmbSede = new ComboBox();
            txtBuscar = new TextBox();

            lblSede.AutoSize = true;
            lblSede.Location = new Point(12, 26);
            lblSede.Text = "Sede:";
            cmbSede.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSede.Location = new Point(60, 23);
            cmbSede.Size = new Size(140, 21);
            cmbSede.SelectedIndexChanged += (sender, args) => ListarStock();

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(220, 26);
            lblBuscar.Text = "Buscar:";
            txtBuscar.Location = new Point(275, 23);
            txtBuscar.Size = new Size(220, 20);
            txtBuscar.TextChanged += (sender, args) => ListarStock();

            GrillaMaderas.Location = new Point(7, 58);
            GrillaMaderas.Size = new Size(767, 553);
            GrillaMaderas.AllowUserToAddRows = false;
            GrillaMaderas.ReadOnly = true;
            GrillaMaderas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grpPino.Controls.Add(lblSede);
            grpPino.Controls.Add(cmbSede);
            grpPino.Controls.Add(lblBuscar);
            grpPino.Controls.Add(txtBuscar);
        }

        private bool SedeValidaParaMovimiento()
        {
            if (SedeSeleccionada == clsStockRepository.SedeTotal)
            {
                MessageBox.Show("Seleccione Cordoba o Misiones para modificar stock.");
                return false;
            }

            return true;
        }

        private void ListarStock()
        {
            pino.ListarPino(GrillaMaderas, SedeSeleccionada, txtBuscar == null ? string.Empty : txtBuscar.Text.Trim());
        }
    }
}
