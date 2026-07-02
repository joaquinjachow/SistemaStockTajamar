using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmListaStockMachimbre : Form
    {
        private clsMachimbre machimbre;
        private ComboBox cmbSede;
        private TextBox txtBuscar;

        public frmListaStockMachimbre()
        {
            InitializeComponent();
            machimbre = new clsMachimbre();
            ConfigurarFiltrosDemo();
            clsUi.Aplicar(this);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarStock();
        }

        private void frmListaStockMachimbre_Load(object sender, EventArgs e)
        {
            try
            {
                cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
                cmbSede.DisplayMember = "Nombre";
                cmbSede.SelectedIndex = cmbSede.FindStringExact(clsDatabase.SedeCordoba);
                cmbCalidad.DataSource = machimbre.ObtenerCalidadMachimbre();
                cmbCalidad.DisplayMember = "Calidad";
                cmbMachimbre.DataSource = machimbre.ObtenerMedidasMachimbre();
                cmbMachimbre.DisplayMember = "Medida";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las medidas y calidades de los machimbres: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbMachimbre.Text;
                string calidad = cmbCalidad.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(calidad))
                {
                    MessageBox.Show("Seleccione calidad y medida antes de agregar stock.");
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

                machimbre.SumarCantidadPaquetes(medida, calidad, cantidad, SedeSeleccionada);
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
                string medida = cmbMachimbre.Text;
                string calidad = cmbCalidad.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(calidad))
                {
                    MessageBox.Show("Seleccione calidad y medida antes de restar stock.");
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

                    machimbre.RestarCantidadPaquetes(medida, calidad, cantidad, SedeSeleccionada, egreso.Detalle, egreso.ClienteId);
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

        private void cmbCalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string calidadSeleccionada = cmbCalidad.Text;
                DataTable medidasFiltradas = machimbre.ObtenerMedidasPorCalidad(calidadSeleccionada);
                cmbMachimbre.DataSource = medidasFiltradas;
                cmbMachimbre.DisplayMember = "Medida";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar las medidas por calidades: " + ex.Message);
            }
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await machimbre.GenerarReporteMachimbreAsync(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnWhatsApp_Click(object sender, EventArgs e)
        {
            machimbre.EnviarStockWhatsApp(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbMachimbre.Text;
                string calidad = cmbCalidad.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(calidad))
                {
                    MessageBox.Show("Seleccione calidad y medida antes de eliminar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("Seguro que desea eliminar el machimbre seleccionado?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                machimbre.EliminarMachimbre(medida, calidad);
                MessageBox.Show("Machimbre eliminado correctamente.");
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

            GrillaMachimbre.Location = new Point(7, 58);
            GrillaMachimbre.Size = new Size(767, 553);
            GrillaMachimbre.AllowUserToAddRows = false;
            GrillaMachimbre.ReadOnly = true;
            GrillaMachimbre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grpMaderas.Controls.Add(lblSede);
            grpMaderas.Controls.Add(cmbSede);
            grpMaderas.Controls.Add(lblBuscar);
            grpMaderas.Controls.Add(txtBuscar);
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
            machimbre.ListarMachimbre(GrillaMachimbre, SedeSeleccionada, txtBuscar == null ? string.Empty : txtBuscar.Text.Trim());
        }
    }
}
