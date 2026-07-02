using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmAgregarNuevoFenólicos : Form
    {
        private ComboBox cmbSede;

        public frmAgregarNuevoFenólicos()
        {
            InitializeComponent();
            ConfigurarSelectorSede();
            clsUi.Aplicar(this);
        }

        private void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            clsFenolicos fenolicos = new clsFenolicos();

            if (string.IsNullOrWhiteSpace(txtCalidad.Text))
            {
                MessageBox.Show("Ingrese una calidad.");
                txtCalidad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEspesor.Text))
            {
                MessageBox.Show("Ingrese un espesor.");
                txtEspesor.Focus();
                return;
            }

            if (!int.TryParse(txtCantidadHojasPaquete.Text, out int cantidadHojasPaquete) || cantidadHojasPaquete <= 0)
            {
                MessageBox.Show("Ingrese una cantidad de hojas por paquete valida.");
                txtCantidadHojasPaquete.Focus();
                return;
            }

            if (!int.TryParse(txtCantidadHojasTotales.Text, out int cantidadHojasTotales) || cantidadHojasTotales < 0)
            {
                MessageBox.Show("Ingrese una cantidad de hojas totales valida.");
                txtCantidadHojasTotales.Focus();
                return;
            }

            fenolicos.Calidad = txtCalidad.Text.Trim();
            fenolicos.Espesor = txtEspesor.Text.Trim();
            fenolicos.CantidadHojasPaquete = cantidadHojasPaquete;
            fenolicos.CantidadHojasTotales = cantidadHojasTotales;
            fenolicos.SedeInicial = cmbSede.Text;

            try
            {
                fenolicos.AgregarNuevoFenolicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar fenolico: " + ex.Message);
                return;
            }

            MessageBox.Show("Fenolico agregado correctamente.");
            txtCalidad.Text = "";
            txtEspesor.Text = "";
            txtCantidadHojasPaquete.Text = "";
            txtCantidadHojasTotales.Text = "";
            cmbSede.SelectedItem = clsDatabase.SedeCordoba;
        }

        private void ConfigurarSelectorSede()
        {
            Label lblSede = new Label();
            cmbSede = new ComboBox();
            int labelX = 22;
            int inputX = 245;
            int inputWidth = 190;
            lblCalidad.Location = new Point(labelX, 36);
            txtCalidad.Location = new Point(inputX, 32);
            txtCalidad.Size = new Size(inputWidth, 24);
            lblEspesor.Location = new Point(labelX, 78);
            txtEspesor.Location = new Point(inputX, 74);
            txtEspesor.Size = new Size(inputWidth, 24);
            lblCantidadHojasPaquete.Location = new Point(labelX, 120);
            txtCantidadHojasPaquete.Location = new Point(inputX, 116);
            txtCantidadHojasPaquete.Size = new Size(inputWidth, 24);
            lblCantidadHojasTotales.Location = new Point(labelX, 162);
            txtCantidadHojasTotales.Location = new Point(inputX, 158);
            txtCantidadHojasTotales.Size = new Size(inputWidth, 24);
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
