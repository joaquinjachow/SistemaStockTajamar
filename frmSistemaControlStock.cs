using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmSistemaControlStock : Form
    {
        private static readonly bool MostrarBackupManual = false;

        public frmSistemaControlStock()
        {
            InitializeComponent();
            ConfigurarPantallaPrincipal();
            ConstruirMenuPrincipal();
            Load += frmSistemaControlStock_Load;
        }

        private void ConfigurarPantallaPrincipal()
        {
            Text = "Tajamar Molduras - Sistema de Gestion de Stock";
            BackColor = Color.FromArgb(245, 247, 250);
            MinimumSize = new Size(900, 600);
            menuStrip1.Dock = DockStyle.Top;
            clsUi.AplicarMenu(menuStrip1);
        }

        private void ConstruirMenuPrincipal()
        {
            menuStrip1.Items.Clear();

            if (clsSesion.PuedeVerEstadisticas)
            {
                ToolStripMenuItem inicio = CrearMenu("Inicio");
                inicio.DropDownItems.Add(Item("Resumen de stock", (sender, args) => AbrirPantalla(new frmResumenStock())));
                menuStrip1.Items.Add(inicio);
            }

            ToolStripMenuItem stock = CrearMenu("Stock");
            stock.DropDownItems.Add(Item("Listado de stock", (sender, args) => AbrirPantalla(new frmListadoStock())));
            if (clsSesion.PuedeAgregar)
            {
                stock.DropDownItems.Add(Item("Agregar producto", (sender, args) => AbrirPantalla(new frmAgregarProducto())));
            }
            if (clsSesion.PuedeEditar)
            {
                stock.DropDownItems.Add(new ToolStripSeparator());
                stock.DropDownItems.Add(Item("Editar productos", (sender, args) => AbrirPantalla(new frmEditarProductos())));
                stock.DropDownItems.Add(Item("Configurar stock minimo", (sender, args) => AbrirPantalla(new frmStockMinimo())));
                stock.DropDownItems.Add(Item("Transferencia entre sedes", (sender, args) => AbrirPantalla(new frmTransferenciaStock())));
            }
            if (clsSesion.PuedeVerEstadisticas)
            {
                if (stock.DropDownItems.Count > 0)
                {
                    stock.DropDownItems.Add(new ToolStripSeparator());
                }
                stock.DropDownItems.Add(Item("Productos con stock bajo", (sender, args) => AbrirPantalla(new frmStockBajo())));
            }

            ToolStripMenuItem clientes = CrearMenu("Clientes");
            if (clsSesion.PuedeAgregar)
            {
                clientes.DropDownItems.Add(Item("Agregar cliente", (sender, args) => AbrirPantalla(new frmAgregarCliente())));
            }
            clientes.DropDownItems.Add(Item(clsSesion.PuedeEditar ? "Listado y edicion" : "Listado de clientes", (sender, args) => AbrirPantalla(new frmListadoClientes())));

            ToolStripMenuItem reportes = null;
            if (clsSesion.PuedeVerReportes)
            {
                reportes = CrearMenu("Reportes");
                reportes.DropDownItems.Add(Item("Reporte general Excel", reporteGeneral_Click));
                reportes.DropDownItems.Add(Item("Historial de movimientos", (sender, args) => AbrirPantalla(new frmHistorialMovimientos())));
                reportes.DropDownItems.Add(new ToolStripSeparator());
                reportes.DropDownItems.Add(Item("Grafico de stock", (sender, args) => AbrirPantalla(new frmGraficoStock())));
            }

            ToolStripMenuItem sistema = CrearMenu("Sistema");
            if (MostrarBackupManual && clsSesion.EsAdministrador)
            {
                sistema.DropDownItems.Add(Item("Backup manual", backup_Click));
                sistema.DropDownItems.Add(new ToolStripSeparator());
            }
            sistema.DropDownItems.Add(Item("Salir", (sender, args) => Close()));

            ToolStripMenuItem usuario = CrearMenu("Usuario: " + clsSesion.Usuario + " (" + clsSesion.Rol + ")");
            usuario.Alignment = ToolStripItemAlignment.Right;
            usuario.Enabled = false;

            menuStrip1.Items.Add(stock);
            menuStrip1.Items.Add(clientes);
            if (reportes != null)
            {
                menuStrip1.Items.Add(reportes);
            }
            menuStrip1.Items.Add(sistema);
            menuStrip1.Items.Add(usuario);
        }

        private ToolStripMenuItem CrearMenu(string texto)
        {
            return new ToolStripMenuItem(texto);
        }

        private ToolStripMenuItem Item(string texto, EventHandler click)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(texto);
            item.Click += click;
            return item;
        }

        private void frmSistemaControlStock_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                if (clsSesion.PuedeVerEstadisticas)
                {
                    AbrirPantalla(new frmResumenStock());
                    return;
                }
                AbrirPantalla(new frmListadoStock());
            }));
        }
        private void AbrirPantalla(Form ventana)
        {
            using (ventana)
            {
                ventana.StartPosition = FormStartPosition.CenterParent;
                ventana.FormBorderStyle = FormBorderStyle.Sizable;
                ventana.MaximizeBox = true;
                ventana.MinimizeBox = true;
                ventana.WindowState = FormWindowState.Maximized;
                ventana.ShowDialog(this);
            }
        }

        private void reporteGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                if (!clsSesion.PuedeVerReportes)
                {
                    MessageBox.Show("El usuario actual no tiene permiso para generar reportes.");
                    return;
                }
                string archivo = clsReportes.GenerarReporteGeneral(clsStockRepository.SedeTotal);
                MessageBox.Show("Reporte general generado correctamente: " + archivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte general: " + ex.Message);
            }
        }

        private void backup_Click(object sender, EventArgs e)
        {
            try
            {
                string archivo = clsBackup.CrearBackupManual();
                MessageBox.Show("Backup generado correctamente: " + archivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar backup: " + ex.Message);
            }
        }
    }
}
