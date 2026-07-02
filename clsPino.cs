using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ControlStock
{
    internal class clsPino
    {
        public Int32 CantidadPaquetes;
        public String Medida;
        public Int32 CantidadTablasPaquete;
        public String SedeInicial = clsDatabase.SedeCordoba;
        public enum Secado { SecadoHorno, SecadoNatural };
        public Secado MetodoSecado;
        public DataTable ObtenerSecadoPino()
        {
            return clsStockRepository.ObtenerValores("Pino", "Secado");
        }
        public DataTable ObtenerMedidasPorSecado(string secado)
        {
            return clsStockRepository.ObtenerValoresFiltrados("Pino", "Medida", "Secado", secado);
        }
        public DataTable ObtenerMedidasPino()
        {
            return clsStockRepository.ObtenerValores("Pino", "Medida");
        }
        public void AgregarNuevoPino()
        {
            clsStockRepository.AgregarProducto("Pino", Medida, MetodoSecado.ToString(), null, null, null, CantidadTablasPaquete, "Paquetes", CantidadPaquetes, SedeInicial);
        }
        private string FormatearEnumSecado(string enumSecado)
        {
            string formattedSecado = System.Text.RegularExpressions.Regex.Replace(enumSecado, "(\\B[A-Z])", " $1");
            return string.IsNullOrEmpty(formattedSecado) ? string.Empty : char.ToUpper(formattedSecado[0]) + formattedSecado.Substring(1);
        }
        public void ListarPino(DataGridView Grilla, string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                DataTable datos = clsStockRepository.ListarPino(sede, filtro);
                Grilla.Rows.Clear();

                foreach (DataRow row in datos.Rows)
                {
                    Grilla.Rows.Add(
                        FormatearEnumSecado(row["Secado"].ToString()),
                        row["Medida"].ToString(),
                        Convert.ToInt32(row["Cantidad"]),
                        Convert.ToInt32(row["CantidadPorUnidad"]),
                        Convert.ToInt32(row["Total"])
                    );
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al listar pino: " + e.Message);
            }
        }
        public void SumarCantidadPaquetes(string medida, string secado, int cantidad, string sede)
        {
            clsStockRepository.SumarStock("Pino", medida, secado, null, null, null, cantidad, sede);
        }
        public void RestarCantidadPaquetes(string medida, string secado, int cantidad, string sede, string detalle, long? clienteId)
        {
            clsStockRepository.RestarStock("Pino", medida, secado, null, null, null, cantidad, sede, detalle, clienteId);
        }
        public void EliminarPino(string medida, string secado)
        {
            clsStockRepository.EliminarProducto("Pino", medida, secado, null, null, null);
        }
        public DataTable ObtenerStockPorMedida()
        {
            return clsStockRepository.ObtenerStockPorMedida("Pino");
        }
        public Task GenerarReportePinoAsync(string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Pino");
                    LlenarReporte(worksheet, clsStockRepository.ListarPino(sede, filtro), sede);
                    string filePath = clsReportes.CrearArchivoReporte("StockPino");
                    workbook.SaveAs(filePath);
                    MessageBox.Show("Reporte generado correctamente: " + filePath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar el reporte: " + e.Message);
            }

            return Task.CompletedTask;
        }
        public void EnviarStockWhatsApp(string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                DataTable datos = clsStockRepository.ListarPino(sede, filtro);
                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine($"Stock de Pino - {sede}");
                mensaje.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                mensaje.AppendLine();

                foreach (DataRow row in datos.Rows)
                {
                    mensaje.AppendLine($"- {FormatearEnumSecado(row["Secado"].ToString())} | Medida: {row["Medida"]} | Paquetes: {row["Cantidad"]} | Tablas x paquete: {row["CantidadPorUnidad"]} | Total tablas: {row["Total"]}");
                }

                clsWhatsApp.EnviarTexto(mensaje.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al preparar el mensaje de WhatsApp: " + e.Message);
            }
        }
        private void LlenarReporte(IXLWorksheet worksheet, DataTable datos, string sede)
        {
            worksheet.Cell("A1").Value = $"Listado de Pino - {sede}";
            worksheet.Range("A1:E1").Merge();
            worksheet.Cell("A2").Value = "Secado";
            worksheet.Cell("B2").Value = "Medida";
            worksheet.Cell("C2").Value = "Cant. Paquetes";
            worksheet.Cell("D2").Value = "Cant. Tablas x Paquete";
            worksheet.Cell("E2").Value = "Cant. Tablas Totales";

            int rowNum = 3;
            foreach (DataRow row in datos.Rows)
            {
                worksheet.Cell(rowNum, 1).Value = FormatearEnumSecado(row["Secado"].ToString());
                worksheet.Cell(rowNum, 2).Value = row["Medida"].ToString();
                worksheet.Cell(rowNum, 3).Value = Convert.ToInt32(row["Cantidad"]);
                worksheet.Cell(rowNum, 4).Value = Convert.ToInt32(row["CantidadPorUnidad"]);
                worksheet.Cell(rowNum, 5).Value = Convert.ToInt32(row["Total"]);
                rowNum++;
            }
            clsReportes.AplicarEstilosListado(worksheet);
        }
    }
}