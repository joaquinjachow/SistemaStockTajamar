using System;
using System.IO;

namespace ControlStock
{
    internal static class clsBackup
    {
        public static string CrearBackupManual()
        {
            string origen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ControlStock.db");
            if (!File.Exists(origen))
            {
                throw new FileNotFoundException("No se encontro la base de datos ControlStock.db.", origen);
            }
            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            Directory.CreateDirectory(carpeta);
            string destino = Path.Combine(carpeta, $"ControlStock_{DateTime.Now:yyyyMMdd_HHmmss}.db");
            File.Copy(origen, destino, false);
            return destino;
        }
    }
}