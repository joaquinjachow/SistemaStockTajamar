using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmListaStockFenólicos : Form
    {
        private clsFenolicos fenolicos;
        private ComboBox cmbSede;
        private TextBox txtBuscar;

        public frmListaStockFenólicos()
        {
            InitializeComponent();
            fenolicos = new clsFenolicos();
            ConfigurarFiltrosDemo();
            clsUi.Aplicar(this);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarStock();
        }

        private void frmListaStockFenólicos_Load(object sender, EventArgs e)
        {
            try
            {
                cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
                cmbSede.DisplayMember = "Nombre";
                cmbSede.SelectedIndex = cmbSede.FindStringExact(clsDatabase.SedeCordoba);
                cmbCalidad.DataSource = fenolicos.ObtenerCalidadesFenolicos();
                cmbCalidad.DisplayMember = "Calidad";
                cmbEspesor.DataSource = fenolicos.ObtenerEspesor();
                cmbEspesor.DisplayMember = "Espesor";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las calidades y espesor de los fenolicos: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string calidad = cmbCalidad.Text;
                string espesor = cmbEspesor.Text;
                if (string.IsNullOrWhiteSpace(calidad) || string.IsNullOrWhiteSpace(espesor))
                {
                    MessageBox.Show("Seleccione calidad y espesor antes de agregar stock.");
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

                fenolicos.SumarCantidadHojas(cantidad, calidad, espesor, SedeSeleccionada);
                MessageBox.Show("Cantidad de hojas agregada correctamente.");
                txtCantidad.Text = "";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cantidad de hojas: " + ex.Message);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            try
            {
                string calidad = cmbCalidad.Text;
                string espesor = cmbEspesor.Text;
                if (string.IsNullOrWhiteSpace(calidad) || string.IsNullOrWhiteSpace(espesor))
                {
                    MessageBox.Show("Seleccione calidad y espesor antes de restar stock.");
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

                    fenolicos.RestarCantidadHojas(cantidad, calidad, espesor, SedeSeleccionada, egreso.Detalle, egreso.ClienteId);
                }

                MessageBox.Show("Cantidad de hojas restada correctamente.");
                txtCantidad.Text = "";
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restar cantidad de hojas: " + ex.Message);
            }
        }

        private void cmbCalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string calidadSeleccionada = cmbCalidad.Text;
                DataTable espesorFiltrado = fenolicos.ObtenerEspesorPorCalidad(calidadSeleccionada);
                cmbEspesor.DataSource = espesorFiltrado;
                cmbEspesor.DisplayMember = "Espesor";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar los espesores por calidad: " + ex.Message);
            }
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            await fenolicos.GenerarReporteFenolicosAsync(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnWhatsApp_Click(object sender, EventArgs e)
        {
            fenolicos.EnviarStockWhatsApp(SedeSeleccionada, txtBuscar.Text.Trim());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string calidad = cmbCalidad.Text;
                string espesor = cmbEspesor.Text;
                if (string.IsNullOrWhiteSpace(calidad) || string.IsNullOrWhiteSpace(espesor))
                {
                    MessageBox.Show("Seleccione calidad y espesor antes de eliminar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("Seguro que desea eliminar el fenolico seleccionado?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                fenolicos.EliminarFenolicos(calidad, espesor);
                MessageBox.Show("Fenolico eliminado correctamente.");
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

            GrillaFenolicos.Location = new Point(7, 58);
            GrillaFenolicos.Size = new Size(690, 553);
            GrillaFenolicos.AllowUserToAddRows = false;
            GrillaFenolicos.ReadOnly = true;
            GrillaFenolicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            fenolicos.ListarFenolicos(GrillaFenolicos, SedeSeleccionada, txtBuscar == null ? string.Empty : txtBuscar.Text.Trim());
        }
    }
}
