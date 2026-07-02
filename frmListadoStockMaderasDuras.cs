using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmListadoStockMaderasDuras : Form
    {
        private clsMaderaDura madera;
        private ComboBox cmbSede;
        private TextBox txtBuscar;

        public frmListadoStockMaderasDuras()
        {
            InitializeComponent();
            madera = new clsMaderaDura();
            ConfigurarFiltrosDemo();
            clsUi.Aplicar(this);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarStock();
        }

        private void frmListadoStockMaderasDuras_Load(object sender, EventArgs e)
        {
            try
            {
                cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
                cmbSede.DisplayMember = "Nombre";
                cmbSede.SelectedIndex = cmbSede.FindStringExact(clsDatabase.SedeCordoba);
                cmbEspecie.DataSource = madera.ObtenerEspeciesMaderaDura();
                cmbEspecie.DisplayMember = "Especie";
                cmbMadera.DataSource = madera.ObtenerMedidasMaderasDura();
                cmbMadera.DisplayMember = "Medida";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las medidas y especies de maderas: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbMadera.Text;
                string especie = cmbEspecie.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(especie))
                {
                    MessageBox.Show("Seleccione especie y medida antes de agregar stock.");
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

                madera.SumarCantidadPaquetes(medida, especie, cantidad, SedeSeleccionada);
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
                string medida = cmbMadera.Text;
                string especie = cmbEspecie.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(especie))
                {
                    MessageBox.Show("Seleccione especie y medida antes de restar stock.");
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

                    madera.RestarCantidadPaquetes(medida, especie, cantidad, SedeSeleccionada, egreso.Detalle, egreso.ClienteId);
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

        private void cmbEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string especieSeleccionada = cmbEspecie.Text;
                DataTable medidasFiltradas = madera.ObtenerMedidasPorEspecies(especieSeleccionada);
                cmbMadera.DataSource = medidasFiltradas;
                cmbMadera.DisplayMember = "Medida";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar las medidas por especies: " + ex.Message);
            }
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await madera.GenerarReporteMaderaDuraAsync(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnWhatsApp_Click(object sender, EventArgs e)
        {
            madera.EnviarStockWhatsApp(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string medida = cmbMadera.Text;
                string especie = cmbEspecie.Text;
                if (string.IsNullOrWhiteSpace(medida) || string.IsNullOrWhiteSpace(especie))
                {
                    MessageBox.Show("Seleccione especie y medida antes de eliminar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("Seguro que desea eliminar la madera dura seleccionada?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                madera.EliminarMadera(medida, especie);
                MessageBox.Show("Madera dura eliminada correctamente.");
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
            GrillaMaderas.Size = new Size(735, 553);
            GrillaMaderas.AllowUserToAddRows = false;
            GrillaMaderas.ReadOnly = true;
            GrillaMaderas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            madera.ListarMaderasDuras(GrillaMaderas, SedeSeleccionada, txtBuscar == null ? string.Empty : txtBuscar.Text.Trim());
        }
    }
}
