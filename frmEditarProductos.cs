using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmEditarProductos : Form
    {
        private TextBox txtBuscar;
        private DataGridView grdProductos;

        public frmEditarProductos()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Label lblBuscar = new Label();
            Button btnBuscar = new Button();
            Button btnGuardar = new Button();
            txtBuscar = new TextBox();
            grdProductos = new DataGridView();

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(16, 19);
            lblBuscar.Text = "Buscar:";

            txtBuscar.Location = new Point(70, 16);
            txtBuscar.Size = new Size(360, 20);

            btnBuscar.Location = new Point(442, 12);
            btnBuscar.Size = new Size(105, 28);
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;

            btnGuardar.Location = new Point(560, 12);
            btnGuardar.Size = new Size(130, 28);
            btnGuardar.Text = "Guardar cambios";
            btnGuardar.Click += btnGuardar_Click;

            grdProductos.AllowUserToAddRows = false;
            grdProductos.AllowUserToDeleteRows = false;
            grdProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdProductos.Location = new Point(16, 52);
            grdProductos.Size = new Size(870, 450);
            grdProductos.DataError += grdProductos_DataError;

            ClientSize = new Size(902, 518);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnGuardar);
            Controls.Add(grdProductos);
            Name = "frmEditarProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Editar productos";
            Load += frmEditarProductos_Load;
        }

        private void frmEditarProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdProductos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un producto.");
                    return;
                }

                grdProductos.EndEdit();
                DataRowView row = (DataRowView)grdProductos.CurrentRow.DataBoundItem;
                int cantidadPorUnidad;
                if (!ValidarProducto(row, out cantidadPorUnidad))
                {
                    return;
                }

                clsStockRepository.EditarProducto(
                    Convert.ToInt64(row["IdProducto"]),
                    row["Medida"].ToString(),
                    row["Secado"].ToString(),
                    row["Especie"].ToString(),
                    row["Calidad"].ToString(),
                    row["Espesor"].ToString(),
                    cantidadPorUnidad,
                    row["Unidad"].ToString());
                MessageBox.Show("Producto actualizado correctamente.");
                CargarProductos();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar producto: " + ex.Message);
            }
        }

        private bool ValidarProducto(DataRowView row, out int cantidadPorUnidad)
        {
            cantidadPorUnidad = 0;
            string rubro = Valor(row, "Rubro");
            string medida = Valor(row, "Medida");
            string secado = Valor(row, "Secado");
            string especie = Valor(row, "Especie");
            string calidad = Valor(row, "Calidad");
            string espesor = Valor(row, "Espesor");
            string unidad = Valor(row, "Unidad");

            if (!int.TryParse(Valor(row, "CantidadPorUnidad"), out cantidadPorUnidad) || cantidadPorUnidad <= 0)
            {
                MessageBox.Show("La cantidad por unidad debe ser un numero mayor a cero.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(unidad))
            {
                MessageBox.Show("La unidad es obligatoria.");
                return false;
            }

            switch (rubro)
            {
                case "Pino":
                    return CampoObligatorio(secado, "secado") && CampoObligatorio(medida, "medida");
                case "MaderaDura":
                    return CampoObligatorio(especie, "especie") && CampoObligatorio(medida, "medida");
                case "Machimbre":
                    return CampoObligatorio(calidad, "calidad") && CampoObligatorio(medida, "medida");
                case "Fenolicos":
                    return CampoObligatorio(calidad, "calidad") && CampoObligatorio(espesor, "espesor");
                default:
                    if (string.IsNullOrWhiteSpace(medida) && string.IsNullOrWhiteSpace(secado) && string.IsNullOrWhiteSpace(especie) && string.IsNullOrWhiteSpace(calidad) && string.IsNullOrWhiteSpace(espesor))
                    {
                        MessageBox.Show("El producto debe tener al menos un dato descriptivo.");
                        return false;
                    }

                    return true;
            }
        }

        private bool CampoObligatorio(string valor, string campo)
        {
            if (!string.IsNullOrWhiteSpace(valor))
            {
                return true;
            }

            MessageBox.Show("El campo " + campo + " es obligatorio para este rubro.");
            return false;
        }

        private static string Valor(DataRowView row, string campo)
        {
            return row.Row.Table.Columns.Contains(campo) && row[campo] != DBNull.Value ? row[campo].ToString().Trim() : string.Empty;
        }

        private void grdProductos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Revise el valor ingresado. La cantidad por unidad debe ser numerica.");
            e.ThrowException = false;
        }

        private void CargarProductos()
        {
            grdProductos.DataSource = clsStockRepository.ListarProductos(txtBuscar.Text.Trim());
            if (grdProductos.Columns.Contains("IdProducto"))
            {
                grdProductos.Columns["IdProducto"].ReadOnly = true;
            }

            if (grdProductos.Columns.Contains("Rubro"))
            {
                grdProductos.Columns["Rubro"].ReadOnly = true;
            }
        }
    }
}
