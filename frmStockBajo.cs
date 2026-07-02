using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmStockBajo : Form
    {
        private DataGridView grdStockBajo;

        public frmStockBajo()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Button btnActualizar = new Button();
            grdStockBajo = new DataGridView();

            btnActualizar.Location = new Point(16, 14);
            btnActualizar.Size = new Size(130, 30);
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;

            grdStockBajo.AllowUserToAddRows = false;
            grdStockBajo.AllowUserToDeleteRows = false;
            grdStockBajo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdStockBajo.Location = new Point(16, 55);
            grdStockBajo.ReadOnly = true;
            grdStockBajo.Size = new Size(750, 390);

            ClientSize = new Size(782, 461);
            Controls.Add(btnActualizar);
            Controls.Add(grdStockBajo);
            Name = "frmStockBajo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Productos con stock bajo";
            Load += frmStockBajo_Load;
        }

        private void frmStockBajo_Load(object sender, EventArgs e)
        {
            CargarStockBajo();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarStockBajo();
        }

        private void CargarStockBajo()
        {
            grdStockBajo.DataSource = clsStockRepository.ListarStockBajo();
        }
    }
}
