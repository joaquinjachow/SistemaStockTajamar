using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmResumenStock : Form
    {
        private DataGridView grdResumen;
        private Label lblActualizado;

        public frmResumenStock()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Button btnActualizar = new Button();
            grdResumen = new DataGridView();
            lblActualizado = new Label();

            btnActualizar.Location = new Point(16, 14);
            btnActualizar.Size = new Size(130, 30);
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;

            lblActualizado.AutoSize = true;
            lblActualizado.Location = new Point(165, 22);

            grdResumen.AllowUserToAddRows = false;
            grdResumen.AllowUserToDeleteRows = false;
            grdResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdResumen.Location = new Point(16, 58);
            grdResumen.ReadOnly = true;
            grdResumen.Size = new Size(620, 340);
            grdResumen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ClientSize = new Size(652, 414);
            Controls.Add(btnActualizar);
            Controls.Add(lblActualizado);
            Controls.Add(grdResumen);
            Name = "frmResumenStock";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Resumen de stock";
            Load += frmResumenStock_Load;
        }

        private void frmResumenStock_Load(object sender, EventArgs e)
        {
            CargarResumen();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarResumen();
        }

        private void CargarResumen()
        {
            grdResumen.DataSource = clsStockRepository.ObtenerResumenStock();
            lblActualizado.Text = "Actualizado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
