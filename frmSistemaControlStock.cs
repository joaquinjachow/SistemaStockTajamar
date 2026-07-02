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
            clsUi.AplicarMenu(menuStrip1);
        }

        private void ConstruirMenuPrincipal()
        {
            menuStrip1.Items.Clear();

            ToolStripMenuItem inicio = CrearMenu("Inicio");
            inicio.DropDownItems.Add(Item("Dashboard", (sender, args) => new frmDashboard().Show()));
            inicio.DropDownItems.Add(Item("Historial de movimientos", (sender, args) => new frmHistorialMovimientos().Show()));

            ToolStripMenuItem stock = CrearMenu("Stock");
            stock.DropDownItems.Add(Item("Listado de pino", (sender, args) => new frmListaStockPino().Show()));
            stock.DropDownItems.Add(Item("Listado de maderas duras", (sender, args) => new frmListadoStockMaderasDuras().Show()));
            stock.DropDownItems.Add(Item("Listado de machimbres", (sender, args) => new frmListaStockMachimbre().Show()));
            stock.DropDownItems.Add(Item("Listado de fenolicos", (sender, args) => new frmListaStockFenólicos().Show()));
            stock.DropDownItems.Add(new ToolStripSeparator());
            stock.DropDownItems.Add(Item("Agregar pino", (sender, args) => new fmrAgregarNuevoPino().Show()));
            stock.DropDownItems.Add(Item("Agregar madera dura", (sender, args) => new frmAgregarNuevaMaderaDura().Show()));
            stock.DropDownItems.Add(Item("Agregar machimbre", (sender, args) => new frmAgregarNuevoMachimbre().Show()));
            stock.DropDownItems.Add(Item("Agregar fenolico", (sender, args) => new frmAgregarNuevoFenólicos().Show()));
            stock.DropDownItems.Add(new ToolStripSeparator());
            stock.DropDownItems.Add(Item("Editar productos", (sender, args) => new frmEditarProductos().Show()));
            stock.DropDownItems.Add(Item("Configurar stock minimo", (sender, args) => new frmStockMinimo().Show()));
            stock.DropDownItems.Add(Item("Productos con stock bajo", (sender, args) => new frmStockBajo().Show()));
            stock.DropDownItems.Add(Item("Transferencia entre sedes", (sender, args) => new frmTransferenciaStock().Show()));

            ToolStripMenuItem clientes = CrearMenu("Clientes");
            clientes.DropDownItems.Add(Item("Agregar cliente", (sender, args) => new frmAgregarCliente().Show()));
            clientes.DropDownItems.Add(Item("Listado y edicion", (sender, args) => new frmListadoClientes().Show()));

            ToolStripMenuItem reportes = CrearMenu("Reportes");
            reportes.DropDownItems.Add(Item("Reporte general Excel", reporteGeneral_Click));
            reportes.DropDownItems.Add(Item("Historial de movimientos", (sender, args) => new frmHistorialMovimientos().Show()));
            reportes.DropDownItems.Add(new ToolStripSeparator());
            reportes.DropDownItems.Add(Item("Grafico pino", (sender, args) => new frmGráficoPino().Show()));
            reportes.DropDownItems.Add(Item("Grafico maderas duras", (sender, args) => new frmGráficoMaderasDuras().Show()));
            reportes.DropDownItems.Add(Item("Grafico machimbres", (sender, args) => new frmGráficoMachimbre().Show()));
            reportes.DropDownItems.Add(Item("Grafico fenolicos", (sender, args) => new frmGráficosFenólicos().Show()));

            ToolStripMenuItem sistema = CrearMenu("Sistema");
            if (MostrarBackupManual)
            {
                sistema.DropDownItems.Add(Item("Backup manual", backup_Click));
                sistema.DropDownItems.Add(new ToolStripSeparator());
            }
            sistema.DropDownItems.Add(Item("Salir", (sender, args) => Close()));

            ToolStripMenuItem usuario = CrearMenu("Usuario: " + clsSesion.Usuario);
            usuario.Alignment = ToolStripItemAlignment.Right;
            usuario.Enabled = false;

            menuStrip1.Items.Add(inicio);
            menuStrip1.Items.Add(stock);
            menuStrip1.Items.Add(clientes);
            menuStrip1.Items.Add(reportes);
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
                frmDashboard ventana = new frmDashboard();
                ventana.Show();
            }));
        }

        private void reporteGeneral_Click(object sender, EventArgs e)
        {
            try
            {
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
