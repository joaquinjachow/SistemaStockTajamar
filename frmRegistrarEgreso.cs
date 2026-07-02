using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmRegistrarEgreso : Form
    {
        private TextBox txtMotivo;
        private ComboBox cmbCliente;
        public string Detalle { get; private set; }
        public long? ClienteId { get; private set; }

        public frmRegistrarEgreso()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Label lblMotivo = new Label();
            Label lblCliente = new Label();
            Button btnAceptar = new Button();
            Button btnCancelar = new Button();
            txtMotivo = new TextBox();
            cmbCliente = new ComboBox();

            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(16, 18);
            lblMotivo.Text = "Motivo:";

            txtMotivo.Location = new Point(90, 15);
            txtMotivo.Size = new Size(335, 20);

            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(16, 55);
            lblCliente.Text = "Cliente:";

            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.Location = new Point(90, 52);
            cmbCliente.Size = new Size(335, 21);

            btnAceptar.Location = new Point(252, 95);
            btnAceptar.Size = new Size(83, 30);
            btnAceptar.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;

            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(342, 95);
            btnCancelar.Size = new Size(83, 30);
            btnCancelar.Text = "Cancelar";

            ClientSize = new Size(445, 143);
            Controls.Add(lblMotivo);
            Controls.Add(txtMotivo);
            Controls.Add(lblCliente);
            Controls.Add(cmbCliente);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmRegistrarEgreso";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registrar egreso";
            Load += frmRegistrarEgreso_Load;
        }

        private void frmRegistrarEgreso_Load(object sender, EventArgs e)
        {
            DataTable clientes = new clsCliente().ListarClientesConId();
            DataRow sinCliente = clientes.NewRow();
            sinCliente["IdCliente"] = DBNull.Value;
            sinCliente["Empresa"] = "Sin cliente asociado";
            clientes.Rows.InsertAt(sinCliente, 0);
            cmbCliente.DataSource = clientes;
            cmbCliente.DisplayMember = "Empresa";
            cmbCliente.ValueMember = "IdCliente";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Detalle = string.IsNullOrWhiteSpace(txtMotivo.Text) ? "Egreso de stock" : txtMotivo.Text.Trim();
            ClienteId = cmbCliente.SelectedValue == null || cmbCliente.SelectedValue == DBNull.Value ? (long?)null : Convert.ToInt64(cmbCliente.SelectedValue);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
