using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmAgregarNuevaMaderaDura : Form
    {
        private ComboBox cmbSede;

        public frmAgregarNuevaMaderaDura()
        {
            InitializeComponent();
            ConfigurarSelectorSede();
            clsUi.Aplicar(this);
        }

        private void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            clsMaderaDura madera = new clsMaderaDura();

            if (string.IsNullOrWhiteSpace(txtEspecie.Text))
            {
                MessageBox.Show("Ingrese una especie.");
                txtEspecie.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMedida.Text))
            {
                MessageBox.Show("Ingrese una medida.");
                txtMedida.Focus();
                return;
            }

            if (!int.TryParse(txtCantidadPaquetes.Text, out int cantidadPaquetes) || cantidadPaquetes < 0)
            {
                MessageBox.Show("Ingrese una cantidad de paquetes valida.");
                txtCantidadPaquetes.Focus();
                return;
            }

            if (!int.TryParse(txtCantidadTablas.Text, out int cantidadTablas) || cantidadTablas <= 0)
            {
                MessageBox.Show("Ingrese una cantidad de tablas por paquete valida.");
                txtCantidadTablas.Focus();
                return;
            }

            madera.Especie = txtEspecie.Text.Trim();
            madera.CantidadPaquetes = cantidadPaquetes;
            madera.Medida = txtMedida.Text.Trim();
            madera.CantidadTablasPaquete = cantidadTablas;
            madera.SedeInicial = cmbSede.Text;

            try
            {
                madera.AgregarNuevaMaderaDura();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar madera dura: " + ex.Message);
                return;
            }

            MessageBox.Show("Madera dura agregada correctamente.");
            txtEspecie.Text = "";
            txtCantidadPaquetes.Text = "";
            txtMedida.Text = "";
            txtCantidadTablas.Text = "";
            cmbSede.SelectedItem = clsDatabase.SedeCordoba;
        }

        private void ConfigurarSelectorSede()
        {
            Label lblSede = new Label();
            cmbSede = new ComboBox();
            int labelX = 22;
            int inputX = 245;
            int inputWidth = 190;
            lblEspecie.Location = new Point(labelX, 36);
            txtEspecie.Location = new Point(inputX, 32);
            txtEspecie.Size = new Size(inputWidth, 24);
            lblCantidadPaquetes.Location = new Point(labelX, 78);
            txtCantidadPaquetes.Location = new Point(inputX, 74);
            txtCantidadPaquetes.Size = new Size(inputWidth, 24);
            lblMedida.Location = new Point(labelX, 120);
            txtMedida.Location = new Point(inputX, 116);
            txtMedida.Size = new Size(inputWidth, 24);
            lblCantidadTablas.Location = new Point(labelX, 162);
            txtCantidadTablas.Location = new Point(inputX, 158);
            txtCantidadTablas.Size = new Size(inputWidth, 24);
            lblSede.AutoSize = true;
            lblSede.Location = new Point(labelX, 204);
            lblSede.Text = "Sede inicial:";
            cmbSede.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSede.Items.AddRange(new object[] { clsDatabase.SedeCordoba, clsDatabase.SedeMisiones });
            cmbSede.Location = new Point(inputX, 200);
            cmbSede.Size = new Size(inputWidth, 24);
            cmbSede.SelectedItem = clsDatabase.SedeCordoba;
            btnAgregarNuevo.Location = new Point(inputX, 240);
            btnAgregarNuevo.Size = new Size(inputWidth, 30);
            grpCargadeDatos.Size = new Size(470, 285);
            ClientSize = new Size(492, 304);
            grpCargadeDatos.Controls.Add(lblSede);
            grpCargadeDatos.Controls.Add(cmbSede);
        }
    }
}
