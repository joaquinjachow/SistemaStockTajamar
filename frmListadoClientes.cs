using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmListadoClientes : Form
    {
        private readonly clsCliente cliente;
        private DataGridView grdClientes;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnListar;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnExportar;

        public frmListadoClientes()
        {
            cliente = new clsCliente();
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            GroupBox grpListado = new GroupBox();
            Label lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnListar = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnExportar = new Button();
            grdClientes = new DataGridView();

            grpListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdClientes).BeginInit();
            SuspendLayout();

            grpListado.Controls.Add(lblBuscar);
            grpListado.Controls.Add(txtBuscar);
            grpListado.Controls.Add(btnBuscar);
            grpListado.Controls.Add(btnListar);
            grpListado.Controls.Add(btnGuardar);
            grpListado.Controls.Add(btnEliminar);
            grpListado.Controls.Add(btnExportar);
            grpListado.Controls.Add(grdClientes);
            grpListado.Location = new Point(12, 12);
            grpListado.Name = "grpListado";
            grpListado.Size = new Size(980, 500);
            grpListado.TabStop = false;
            grpListado.Text = "Listado de clientes";

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(16, 30);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(43, 13);
            lblBuscar.Text = "Buscar:";

            txtBuscar.Location = new Point(68, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(430, 20);

            btnBuscar.Location = new Point(510, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(105, 27);
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;

            btnListar.Location = new Point(620, 23);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(105, 27);
            btnListar.Text = "Listar todo";
            btnListar.UseVisualStyleBackColor = true;
            btnListar.Click += btnListar_Click;

            btnGuardar.Location = new Point(19, 458);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(130, 27);
            btnGuardar.Text = "Guardar cambios";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;

            btnEliminar.Location = new Point(158, 458);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(105, 27);
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;

            btnExportar.Location = new Point(275, 458);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(130, 27);
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;

            grdClientes.AllowUserToAddRows = false;
            grdClientes.AllowUserToDeleteRows = false;
            grdClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdClientes.Location = new Point(19, 66);
            grdClientes.Name = "grdClientes";
            grdClientes.ReadOnly = false;
            grdClientes.Size = new Size(946, 380);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 524);
            Controls.Add(grpListado);
            Name = "frmListadoClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Listado Clientes";
            Load += frmListadoClientes_Load;

            grpListado.ResumeLayout(false);
            grpListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grdClientes).EndInit();
            ResumeLayout(false);
        }

        private void frmListadoClientes_Load(object sender, EventArgs e)
        {
            CargarListadoCompleto();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CargarListadoCompleto();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tabla = cliente.BuscarClientes(txtBuscar.Text.Trim());
                grdClientes.DataSource = tabla;
                BloquearIdCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar clientes: " + ex.Message);
            }
        }

        private void CargarListadoCompleto()
        {
            try
            {
                grdClientes.DataSource = cliente.ListarClientes();
                BloquearIdCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar clientes: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdClientes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un cliente.");
                    return;
                }

                grdClientes.EndEdit();
                DataRowView row = (DataRowView)grdClientes.CurrentRow.DataBoundItem;
                cliente.EditarCliente(Convert.ToInt64(row["IdCliente"]), row["Empresa"].ToString(), row["Direccion"].ToString(), row["Telefono"].ToString(), row["Cuit"].ToString(), row["Email"].ToString(), row["CuentaBancaria"].ToString(), row["Observaciones"].ToString());
                MessageBox.Show("Cliente actualizado correctamente.");
                CargarListadoCompleto();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cliente: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdClientes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un cliente.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("Seguro que desea eliminar el cliente seleccionado?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                DataRowView row = (DataRowView)grdClientes.CurrentRow.DataBoundItem;
                cliente.EliminarCliente(Convert.ToInt64(row["IdCliente"]));
                MessageBox.Show("Cliente eliminado correctamente.");
                CargarListadoCompleto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string archivo = clsReportes.GenerarReporteClientes(txtBuscar.Text.Trim());
                MessageBox.Show("Reporte de clientes generado correctamente: " + archivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar clientes: " + ex.Message);
            }
        }

        private void BloquearIdCliente()
        {
            if (grdClientes.Columns.Contains("IdCliente"))
            {
                grdClientes.Columns["IdCliente"].ReadOnly = true;
            }
        }
    }
}
