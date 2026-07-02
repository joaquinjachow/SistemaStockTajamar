using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmDashboard : Form
    {
        private DataGridView grdResumen;
        private Label lblActualizado;

        public frmDashboard()
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

            ClientSize = new Size(652, 414);
            Controls.Add(btnActualizar);
            Controls.Add(lblActualizado);
            Controls.Add(grdResumen);
            Name = "frmDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard de stock";
            Load += frmDashboard_Load;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            CargarResumen();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarResumen();
        }

        private void CargarResumen()
        {
            grdResumen.DataSource = clsStockRepository.ObtenerResumenDashboard();
            lblActualizado.Text = "Actualizado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
