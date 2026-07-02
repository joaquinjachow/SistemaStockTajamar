using System;
using System.Windows.Forms;

namespace ControlStock
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                clsDatabase.AsegurarBaseDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo inicializar la base de datos: " + ex.Message);
                return;
            }
            using (frmLogin login = new frmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            Application.Run(new frmSistemaControlStock());
        }
    }
}