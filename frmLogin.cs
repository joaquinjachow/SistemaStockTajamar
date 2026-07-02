using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmLogin : Form
    {
        private TextBox txtUsuario;
        private TextBox txtClave;

        public frmLogin()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Label lblUsuario = new Label();
            Label lblClave = new Label();
            Button btnIngresar = new Button();
            Button btnSalir = new Button();
            txtUsuario = new TextBox();
            txtClave = new TextBox();

            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(22, 25);
            lblUsuario.Text = "Usuario:";

            txtUsuario.Location = new Point(90, 22);
            txtUsuario.Size = new Size(210, 20);
            txtUsuario.Text = "admin";

            lblClave.AutoSize = true;
            lblClave.Location = new Point(22, 62);
            lblClave.Text = "Clave:";

            txtClave.Location = new Point(90, 59);
            txtClave.PasswordChar = '*';
            txtClave.Size = new Size(210, 20);

            btnIngresar.Location = new Point(132, 102);
            btnIngresar.Size = new Size(80, 30);
            btnIngresar.Text = "Ingresar";
            btnIngresar.Click += btnIngresar_Click;

            btnSalir.DialogResult = DialogResult.Cancel;
            btnSalir.Location = new Point(220, 102);
            btnSalir.Size = new Size(80, 30);
            btnSalir.Text = "Salir";

            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
            ClientSize = new Size(326, 154);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblClave);
            Controls.Add(txtClave);
            Controls.Add(btnIngresar);
            Controls.Add(btnSalir);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ingreso al sistema";
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (new clsUsuario().ValidarLogin(txtUsuario.Text, txtClave.Text))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                MessageBox.Show("Usuario o clave incorrectos.");
                txtClave.SelectAll();
                txtClave.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesion: " + ex.Message);
            }
        }
    }
}
