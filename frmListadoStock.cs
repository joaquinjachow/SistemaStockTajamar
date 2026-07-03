using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmListadoStock : Form
    {
        private ComboBox cmbRubro;
        private ComboBox cmbSede;
        private TextBox txtBuscar;
        private TextBox txtCantidad;
        private Label lblCantidad;
        private GroupBox acciones;
        private Button btnNuevo;
        private Button btnAgregar;
        private Button btnRestar;
        private Button btnEliminar;
        private Button btnExportar;
        private Button btnWhatsApp;
        private DataGridView grdStock;
        private DataTable datosActuales;

        public frmListadoStock()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Text = "Listado de Stock";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(980, 650);

            Label lblRubro = new Label();
            lblRubro.AutoSize = true;
            lblRubro.Location = new Point(15, 19);
            lblRubro.Text = "Rubro:";
            cmbRubro = new ComboBox();
            cmbRubro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRubro.Location = new Point(65, 16);
            cmbRubro.Size = new Size(150, 21);
            cmbRubro.SelectedIndexChanged += (sender, args) => ListarStock();

            Label lblSede = new Label();
            lblSede.AutoSize = true;
            lblSede.Location = new Point(235, 19);
            lblSede.Text = "Sede:";
            cmbSede = new ComboBox();
            cmbSede.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSede.Location = new Point(280, 16);
            cmbSede.Size = new Size(140, 21);
            cmbSede.SelectedIndexChanged += (sender, args) => ListarStock();

            Label lblBuscar = new Label();
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(440, 19);
            lblBuscar.Text = "Buscar:";
            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(495, 16);
            txtBuscar.Size = new Size(250, 20);
            txtBuscar.TextChanged += (sender, args) => ListarStock();

            Button btnListar = new Button();
            btnListar.Text = "Listar";
            btnListar.Location = new Point(770, 12);
            btnListar.Size = new Size(90, 30);
            btnListar.Click += (sender, args) => ListarStock();

            btnNuevo = new Button();
            btnNuevo.Text = "Nuevo";
            btnNuevo.Location = new Point(875, 12);
            btnNuevo.Size = new Size(90, 30);
            btnNuevo.Click += btnNuevo_Click;

            grdStock = new DataGridView();
            grdStock.Location = new Point(12, 55);
            grdStock.Size = new Size(953, 470);
            grdStock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grdStock.AllowUserToAddRows = false;
            grdStock.AllowUserToDeleteRows = false;
            grdStock.ReadOnly = true;
            grdStock.MultiSelect = true;
            grdStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            acciones = new GroupBox();
            acciones.Text = "Acciones";
            acciones.Location = new Point(12, 535);
            acciones.Size = new Size(953, 95);
            acciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lblCantidad = new Label();
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(18, 40);
            lblCantidad.Text = "Cantidad:";
            txtCantidad = new TextBox();
            txtCantidad.Location = new Point(85, 37);
            txtCantidad.Size = new Size(100, 20);

            btnAgregar = CrearBoton("Agregar", 210, btnAgregar_Click);
            btnRestar = CrearBoton("Restar", 315, btnRestar_Click);
            btnEliminar = CrearBoton("Eliminar", 420, btnEliminar_Click);
            btnExportar = CrearBoton("Exportar", 570, btnExportar_Click);
            btnWhatsApp = CrearBoton("WhatsApp", 675, btnWhatsApp_Click);

            acciones.Controls.Add(lblCantidad);
            acciones.Controls.Add(txtCantidad);
            acciones.Controls.Add(btnAgregar);
            acciones.Controls.Add(btnRestar);
            acciones.Controls.Add(btnEliminar);
            acciones.Controls.Add(btnExportar);
            acciones.Controls.Add(btnWhatsApp);

            Controls.Add(lblRubro);
            Controls.Add(cmbRubro);
            Controls.Add(lblSede);
            Controls.Add(cmbSede);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnListar);
            Controls.Add(btnNuevo);
            Controls.Add(grdStock);
            Controls.Add(acciones);

            Load += frmListadoStock_Load;
            Resize += (sender, args) => AjustarLayoutAcciones();
        }

        private Button CrearBoton(string texto, int x, EventHandler click)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Location = new Point(x, 30);
            boton.Size = new Size(90, 35);
            boton.Click += click;
            return boton;
        }

        private void frmListadoStock_Load(object sender, EventArgs e)
        {
            cmbRubro.DataSource = clsStockRepository.ObtenerRubrosConTodos();
            cmbRubro.DisplayMember = "Rubro";
            cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
            cmbSede.DisplayMember = "Nombre";
            cmbSede.SelectedIndex = cmbSede.FindStringExact(clsDatabase.SedeCordoba);
            AplicarPermisos();
            ListarStock();
        }

        private string RubroSeleccionado
        {
            get { return cmbRubro == null ? "Todos" : cmbRubro.Text; }
        }

        private string SedeSeleccionada
        {
            get { return cmbSede == null ? clsStockRepository.SedeTotal : cmbSede.Text; }
        }

        private void ListarStock()
        {
            if (cmbRubro == null || cmbSede == null || txtBuscar == null)
            {
                return;
            }
            datosActuales = clsStockRepository.ListarStock(RubroSeleccionado, SedeSeleccionada, txtBuscar.Text.Trim());
            grdStock.DataSource = datosActuales;
            ConfigurarGrilla();
        }

        private void ConfigurarGrilla()
        {
            if (grdStock.Columns["IdProducto"] != null)
            {
                grdStock.Columns["IdProducto"].Visible = false;
            }
            ConfigurarColumnasPorRubro();
            foreach (DataGridViewColumn columna in grdStock.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            grdStock.ClearSelection();
        }

        private void ConfigurarColumnasPorRubro()
        {
            string rubro = RubroNormalizado();
            bool todos = rubro == "todos" || rubro == string.Empty;
            bool esPino = rubro == "pino";
            bool esMaderaDura = rubro == "maderadura";
            bool esMachimbre = rubro == "machimbre";
            bool esFenolicos = rubro == "fenolicos";

            MostrarColumna("Medida", todos || esPino || esMaderaDura || esMachimbre);
            MostrarColumna("Secado", todos || esPino);
            MostrarColumna("Especie", todos || esMaderaDura);
            MostrarColumna("Calidad", todos || esMachimbre || esFenolicos);
            MostrarColumna("Espesor", todos || esFenolicos);

            bool mostrarCantidadPorUnidad = todos || esPino || esMaderaDura;
            MostrarColumna("CantidadPorUnidad", mostrarCantidadPorUnidad);
            MostrarColumna("Total", mostrarCantidadPorUnidad);

            CambiarTitulo("Rubro", "Rubro");
            CambiarTitulo("Producto", "Producto");
            CambiarTitulo("Sede", "Sede");
            CambiarTitulo("Unidad", "Unidad");
            CambiarTitulo("CantidadPorUnidad", "Cantidad por paquete");
            CambiarTitulo("Total", "Total");
            if (esPino || esMaderaDura)
            {
                CambiarTitulo("Cantidad", "Paquetes");
            }
            else if (esMachimbre)
            {
                CambiarTitulo("Cantidad", "Tablas");
            }
            else if (esFenolicos)
            {
                CambiarTitulo("Cantidad", "Hojas");
            }
            else
            {
                CambiarTitulo("Cantidad", "Cantidad");
            }
        }

        private void MostrarColumna(string nombre, bool visible)
        {
            if (grdStock.Columns[nombre] != null)
            {
                grdStock.Columns[nombre].Visible = visible;
            }
        }

        private void CambiarTitulo(string nombre, string titulo)
        {
            if (grdStock.Columns[nombre] != null)
            {
                grdStock.Columns[nombre].HeaderText = titulo;
            }
        }

        private string RubroNormalizado()
        {
            return RubroSeleccionado.Trim().ToLowerInvariant().Replace(" ", "").Replace("\u00f3", "o");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!clsSesion.PuedeAgregar)
            {
                MessageBox.Show("El usuario actual no tiene permiso para agregar productos.");
                return;
            }

            using (frmAgregarProducto form = new frmAgregarProducto())
            {
                form.ShowDialog(this);
            }
            RecargarCombos();
            ListarStock();
        }

        private void RecargarCombos()
        {
            string rubro = RubroSeleccionado;
            string sede = SedeSeleccionada;
            cmbRubro.DataSource = clsStockRepository.ObtenerRubrosConTodos();
            cmbRubro.DisplayMember = "Rubro";
            cmbRubro.SelectedIndex = cmbRubro.FindStringExact(rubro);
            cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
            cmbSede.DisplayMember = "Nombre";
            cmbSede.SelectedIndex = cmbSede.FindStringExact(sede);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clsSesion.PuedeAgregar)
                {
                    MessageBox.Show("El usuario actual no tiene permiso para agregar stock.");
                    return;
                }
                if (!ProductoSeleccionado(out long idProducto))
                {
                    return;
                }
                if (!SedeValidaParaMovimiento())
                {
                    return;
                }
                if (!CantidadValida(out int cantidad))
                {
                    return;
                }
                clsStockRepository.SumarStock(idProducto, cantidad, SedeSeleccionada);
                MessageBox.Show("Stock agregado correctamente.");
                txtCantidad.Clear();
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar stock: " + ex.Message);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clsSesion.PuedeEditar)
                {
                    MessageBox.Show("El usuario actual no tiene permiso para restar stock.");
                    return;
                }
                if (!ProductoSeleccionado(out long idProducto))
                {
                    return;
                }
                if (!SedeValidaParaMovimiento())
                {
                    return;
                }
                if (!CantidadValida(out int cantidad))
                {
                    return;
                }
                using (frmRegistrarEgreso egreso = new frmRegistrarEgreso())
                {
                    if (egreso.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
                    clsStockRepository.RestarStock(idProducto, cantidad, SedeSeleccionada, egreso.Detalle, egreso.ClienteId);
                }
                MessageBox.Show("Stock restado correctamente.");
                txtCantidad.Clear();
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restar stock: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clsSesion.PuedeEditar)
                {
                    MessageBox.Show("El usuario actual no tiene permiso para eliminar productos.");
                    return;
                }
                if (!ProductoSeleccionado(out long idProducto))
                {
                    return;
                }
                DialogResult confirmacion = MessageBox.Show("Seguro que desea eliminar el producto seleccionado?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }
                clsStockRepository.EliminarProducto(idProducto);
                MessageBox.Show("Producto eliminado correctamente.");
                ListarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clsSesion.PuedeExportar)
                {
                    MessageBox.Show("El usuario actual no tiene permiso para exportar stock.");
                    return;
                }
                DataTable datos = ObtenerDatosSeleccionadosOFiltrados();
                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.");
                    return;
                }
                QuitarColumnasNoVisibles(datos);
                string archivo = clsReportes.GenerarReporteStock(datos, NombreReporteStock());
                MessageBox.Show("Reporte generado correctamente: " + archivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar stock: " + ex.Message);
            }
        }

        private void btnWhatsApp_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clsSesion.PuedeExportar)
                {
                    MessageBox.Show("El usuario actual no tiene permiso para enviar stock por WhatsApp.");
                    return;
                }
                DataTable datos = ObtenerDatosSeleccionadosOFiltrados();
                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para enviar.");
                    return;
                }
                clsWhatsApp.EnviarTexto(ArmarMensajeWhatsApp(datos));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar WhatsApp: " + ex.Message);
            }
        }
        private void AplicarPermisos()
        {
            bool puedeAgregar = clsSesion.PuedeAgregar;
            bool puedeEditar = clsSesion.PuedeEditar;
            bool puedeExportar = clsSesion.PuedeExportar;

            btnNuevo.Visible = puedeAgregar;
            lblCantidad.Visible = puedeAgregar || puedeEditar;
            txtCantidad.Visible = puedeAgregar || puedeEditar;
            btnAgregar.Visible = puedeAgregar;
            btnRestar.Visible = puedeEditar;
            btnEliminar.Visible = puedeEditar;
            btnExportar.Visible = puedeExportar;
            btnWhatsApp.Visible = puedeExportar;
            acciones.Visible = puedeAgregar || puedeEditar || puedeExportar;
            AjustarLayoutAcciones();
        }
        private void AjustarLayoutAcciones()
        {
            if (grdStock == null || acciones == null)
            {
                return;
            }

            int margen = 15;
            int altoDisponible = ClientSize.Height - grdStock.Top - margen;
            if (acciones.Visible)
            {
                acciones.Top = ClientSize.Height - acciones.Height - margen;
                altoDisponible = acciones.Top - grdStock.Top - 10;
            }
            grdStock.Height = Math.Max(150, altoDisponible);
        }

        private bool ProductoSeleccionado(out long idProducto)
        {
            idProducto = 0;
            if (grdStock.SelectedRows.Count != 1)
            {
                MessageBox.Show("Seleccione un solo producto de la tabla.");
                return false;
            }
            idProducto = Convert.ToInt64(grdStock.SelectedRows[0].Cells["IdProducto"].Value);
            return true;
        }

        private bool SedeValidaParaMovimiento()
        {
            if (SedeSeleccionada == clsStockRepository.SedeTotal)
            {
                MessageBox.Show("Seleccione Cordoba o Misiones para modificar stock.");
                return false;
            }
            return true;
        }

        private bool CantidadValida(out int cantidad)
        {
            cantidad = 0;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad valida.");
                txtCantidad.Focus();
                return false;
            }
            return true;
        }

        private DataTable ObtenerDatosSeleccionadosOFiltrados()
        {
            DataTable origen = datosActuales == null ? clsStockRepository.ListarStock(RubroSeleccionado, SedeSeleccionada, txtBuscar.Text.Trim()) : datosActuales;
            DataTable salida = origen.Clone();
            if (grdStock.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow fila in grdStock.SelectedRows)
                {
                    if (fila.DataBoundItem is DataRowView view)
                    {
                        salida.ImportRow(view.Row);
                    }
                }
            }
            else
            {
                foreach (DataRow row in origen.Rows)
                {
                    salida.ImportRow(row);
                }
            }
            if (salida.Columns.Contains("IdProducto"))
            {
                salida.Columns.Remove("IdProducto");
            }
            return salida;
        }

        private void QuitarColumnasNoVisibles(DataTable datos)
        {
            for (int i = datos.Columns.Count - 1; i >= 0; i--)
            {
                string nombre = datos.Columns[i].ColumnName;
                if (grdStock.Columns[nombre] != null && !grdStock.Columns[nombre].Visible)
                {
                    datos.Columns.Remove(nombre);
                }
            }
        }

        private string NombreReporteStock()
        {
            string rubro = RubroSeleccionado == "Todos" ? "General" : RubroSeleccionado;
            string sede = SedeSeleccionada == clsStockRepository.SedeTotal ? "Total" : SedeSeleccionada;
            return "Stock_" + rubro + "_" + sede;
        }

        private string ArmarMensajeWhatsApp(DataTable datos)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Stock Tajamar Molduras");
            sb.AppendLine("Rubro: " + RubroSeleccionado + " - Sede: " + SedeSeleccionada);
            sb.AppendLine();
            foreach (DataRow row in datos.Rows)
            {
                sb.Append("- ");
                sb.Append(row["Rubro"]);
                sb.Append(" | ");
                sb.Append(row["Producto"]);
                sb.Append(" | Sede: ");
                sb.Append(row["Sede"]);
                sb.Append(" | Cantidad: ");
                sb.Append(row["Cantidad"]);
                sb.Append(" ");
                sb.Append(row["Unidad"]);
                if (datos.Columns.Contains("CantidadPorUnidad") && datos.Columns.Contains("Total") && Convert.ToInt32(row["CantidadPorUnidad"]) > 1)
                {
                    sb.Append(" | Total: ");
                    sb.Append(row["Total"]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
