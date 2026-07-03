using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmAgregarCliente : Form
    {
        private readonly clsCliente cliente;
        private TextBox txtEmpresa;
        private TextBox txtDireccion;
        private TextBox txtTelefono;
        private TextBox txtCuit;
        private TextBox txtEmail;
        private ComboBox cmbCondicionIva;
        private TextBox txtDireccionLegal;
        private TextBox txtObservaciones;

        public frmAgregarCliente()
        {
            cliente = new clsCliente();
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            GroupBox grpDatos = new GroupBox();
            Button btnAgregar = new Button();
            grpDatos.SuspendLayout();
            SuspendLayout();

            grpDatos.Location = new Point(12, 12);
            grpDatos.Name = "grpDatos";
            grpDatos.Size = new Size(560, 405);
            grpDatos.TabStop = false;
            grpDatos.Text = "Agregar cliente";

            txtEmpresa = AgregarCampo(grpDatos, "Empresa:", 30);
            txtDireccion = AgregarCampo(grpDatos, "Direccion comercial:", 70);
            txtTelefono = AgregarCampo(grpDatos, "Telefono:", 110);
            txtCuit = AgregarCampo(grpDatos, "CUIT:", 150);
            txtEmail = AgregarCampo(grpDatos, "Email:", 190);
            cmbCondicionIva = AgregarCombo(grpDatos, "Condicion frente al IVA:", 230);
            txtDireccionLegal = AgregarCampo(grpDatos, "Direccion legal:", 270);
            txtObservaciones = AgregarCampo(grpDatos, "Observaciones:", 310);

            btnAgregar.Location = new Point(390, 360);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(145, 30);
            btnAgregar.Text = "Guardar cliente";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            grpDatos.Controls.Add(btnAgregar);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 429);
            Controls.Add(grpDatos);
            Name = "frmAgregarCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agregar cliente";

            grpDatos.ResumeLayout(false);
            grpDatos.PerformLayout();
            ResumeLayout(false);
        }

        private TextBox AgregarCampo(GroupBox grupo, string texto, int y)
        {
            Label label = new Label();
            TextBox textBox = new TextBox();

            label.AutoSize = true;
            label.Location = new Point(18, y + 3);
            label.Text = texto;
            textBox.Location = new Point(170, y);
            textBox.Size = new Size(365, 20);
            grupo.Controls.Add(label);
            grupo.Controls.Add(textBox);
            return textBox;
        }
        private ComboBox AgregarCombo(GroupBox grupo, string texto, int y)
        {
            Label label = new Label();
            ComboBox comboBox = new ComboBox();

            label.AutoSize = true;
            label.Location = new Point(18, y + 3);
            label.Text = texto;
            comboBox.Location = new Point(170, y);
            comboBox.Size = new Size(365, 21);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.AddRange(clsCliente.ObtenerCondicionesIva());
            comboBox.SelectedIndex = 0;
            grupo.Controls.Add(label);
            grupo.Controls.Add(comboBox);
            return comboBox;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmpresa.Text))
                {
                    MessageBox.Show("El nombre de la empresa es obligatorio.");
                    txtEmpresa.Focus();
                    return;
                }

                cliente.AgregarCliente(txtEmpresa.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtCuit.Text.Trim(), txtEmail.Text.Trim(), cmbCondicionIva.Text.Trim(), txtDireccionLegal.Text.Trim(), txtObservaciones.Text.Trim());
                MessageBox.Show("Cliente agregado correctamente.");
                LimpiarCampos();
                txtEmpresa.Focus();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtEmpresa.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cmbCondicionIva.SelectedIndex = 0;
            txtDireccionLegal.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
        }
    }
}
