using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmTransferenciaStock : Form
    {
        private ComboBox cmbRubro;
        private ComboBox cmbProducto;
        private ComboBox cmbOrigen;
        private ComboBox cmbDestino;
        private TextBox txtCantidad;
        private TextBox txtDetalle;
        private DataTable productos;

        public frmTransferenciaStock()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Label lblRubro = new Label();
            Label lblProducto = new Label();
            Label lblOrigen = new Label();
            Label lblDestino = new Label();
            Label lblCantidad = new Label();
            Label lblDetalle = new Label();
            Button btnTransferir = new Button();
            cmbRubro = new ComboBox();
            cmbProducto = new ComboBox();
            cmbOrigen = new ComboBox();
            cmbDestino = new ComboBox();
            txtCantidad = new TextBox();
            txtDetalle = new TextBox();

            lblRubro.AutoSize = true;
            lblRubro.Location = new Point(18, 22);
            lblRubro.Text = "Rubro:";
            cmbRubro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRubro.Items.AddRange(new object[] { "Pino", "MaderaDura", "Machimbre", "Fenolicos" });
            cmbRubro.Location = new Point(112, 19);
            cmbRubro.Size = new Size(230, 21);
            cmbRubro.SelectedIndexChanged += cmbRubro_SelectedIndexChanged;

            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(18, 58);
            lblProducto.Text = "Producto:";
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.Location = new Point(112, 55);
            cmbProducto.Size = new Size(360, 21);

            lblOrigen.AutoSize = true;
            lblOrigen.Location = new Point(18, 94);
            lblOrigen.Text = "Desde:";
            cmbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrigen.Location = new Point(112, 91);
            cmbOrigen.Size = new Size(160, 21);

            lblDestino.AutoSize = true;
            lblDestino.Location = new Point(18, 130);
            lblDestino.Text = "Hacia:";
            cmbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestino.Location = new Point(112, 127);
            cmbDestino.Size = new Size(160, 21);

            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(18, 166);
            lblCantidad.Text = "Cantidad:";
            txtCantidad.Location = new Point(112, 163);
            txtCantidad.Size = new Size(160, 20);

            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(18, 202);
            lblDetalle.Text = "Detalle:";
            txtDetalle.Location = new Point(112, 199);
            txtDetalle.Size = new Size(360, 20);

            btnTransferir.Location = new Point(342, 240);
            btnTransferir.Size = new Size(130, 32);
            btnTransferir.Text = "Transferir";
            btnTransferir.Click += btnTransferir_Click;

            ClientSize = new Size(492, 294);
            Controls.Add(lblRubro);
            Controls.Add(cmbRubro);
            Controls.Add(lblProducto);
            Controls.Add(cmbProducto);
            Controls.Add(lblOrigen);
            Controls.Add(cmbOrigen);
            Controls.Add(lblDestino);
            Controls.Add(cmbDestino);
            Controls.Add(lblCantidad);
            Controls.Add(txtCantidad);
            Controls.Add(lblDetalle);
            Controls.Add(txtDetalle);
            Controls.Add(btnTransferir);
            Name = "frmTransferenciaStock";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transferencia entre sedes";
            Load += frmTransferenciaStock_Load;
        }

        private void frmTransferenciaStock_Load(object sender, EventArgs e)
        {
            DataTable sedes = clsStockRepository.ObtenerSedes();
            cmbOrigen.DataSource = sedes.Copy();
            cmbOrigen.DisplayMember = "Nombre";
            cmbDestino.DataSource = sedes.Copy();
            cmbDestino.DisplayMember = "Nombre";
            cmbRubro.SelectedIndex = 0;
        }

        private void cmbRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            productos = clsStockRepository.Consultar(@"
SELECT IdProducto, Rubro, Medida, Secado, Especie, Calidad, Espesor,
       TRIM(COALESCE(Secado || ' ', '') || COALESCE(Especie || ' ', '') || COALESCE(Calidad || ' ', '') || COALESCE(Medida || ' ', '') || COALESCE(Espesor, '')) AS Descripcion
FROM Productos
WHERE Activo = 1 AND Rubro = @Rubro
ORDER BY Descripcion;", new System.Data.SQLite.SQLiteParameter("@Rubro", cmbRubro.Text));
            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "Descripcion";
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedItem == null || !int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Seleccione un producto e ingrese una cantidad valida.");
                    return;
                }

                DataRowView row = (DataRowView)cmbProducto.SelectedItem;
                clsStockRepository.TransferirStock(
                    cmbRubro.Text,
                    row["Medida"].ToString(),
                    row["Secado"].ToString(),
                    row["Especie"].ToString(),
                    row["Calidad"].ToString(),
                    row["Espesor"].ToString(),
                    cantidad,
                    cmbOrigen.Text,
                    cmbDestino.Text,
                    txtDetalle.Text);
                MessageBox.Show("Transferencia registrada correctamente.");
                txtCantidad.Text = "";
                txtDetalle.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al transferir stock: " + ex.Message);
            }
        }
    }
}
