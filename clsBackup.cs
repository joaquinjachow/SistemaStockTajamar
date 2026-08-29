using System;
using System.IO;

namespace ControlStock
{
    internal static class clsBackup
    {
        public static string CrearBackupManual()
        {
            string origen = clsDatabase.DatabasePath;
            if (!File.Exists(origen))
            {
                throw new FileNotFoundException("No se encontro la base de datos ControlStock.db.", origen);
            }
            string carpeta = Path.Combine(Path.GetDirectoryName(origen), "Backups");
            Directory.CreateDirectory(carpeta);
            string destino = Path.Combine(carpeta, $"ControlStock_{DateTime.Now:yyyyMMdd_HHmmss_fff}.db");
            File.Copy(origen, destino, false);
            return destino;
        }
    }
}
