using System;
using System.Diagnostics;

namespace ControlStock
{
    internal static class clsWhatsApp
    {
        public static void EnviarTexto(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                throw new InvalidOperationException("No hay datos para enviar por WhatsApp.");
            }

            string url = "https://wa.me/?text=" + Uri.EscapeDataString(mensaje);

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
