using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ControlStock
{
    internal class clsMachimbre
    {
        public Int32 CantidadPaquetes;
        public String Medida;
        public Int32 CantidadTablasPaquete;
        public String Calidad;
        public String SedeInicial = clsDatabase.SedeCordoba;
        public DataTable ObtenerCalidadMachimbre()
        {
            return clsStockRepository.ObtenerValores("Machimbre", "Calidad");
        }
        public DataTable ObtenerMedidasPorCalidad(string calidades)
        {
            return clsStockRepository.ObtenerValoresFiltrados("Machimbre", "Medida", "Calidad", calidades);
        }
        public DataTable ObtenerMedidasMachimbre()
        {
            return clsStockRepository.ObtenerValores("Machimbre", "Medida");
        }
        public void AgregarNuevoMachimbre()
        {
            clsStockRepository.AgregarProducto("Machimbre", Medida, null, null, Calidad, null, CantidadTablasPaquete, "Paquetes", CantidadPaquetes, SedeInicial);
        }
        public void ListarMachimbre(DataGridView Grilla, string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                DataTable datos = clsStockRepository.ListarMachimbre(sede, filtro);
                Grilla.Rows.Clear();

                foreach (DataRow row in datos.Rows)
                {
                    Grilla.Rows.Add(
                        row["Calidad"].ToString(),
                        row["Medida"].ToString(),
                        Convert.ToInt32(row["Cantidad"]),
                        Convert.ToInt32(row["CantidadPorUnidad"]),
                        Convert.ToInt32(row["Total"])
                    );
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al listar machimbres: " + e.Message);
            }
        }
        public void SumarCantidadPaquetes(string medida, string calidad, int cantidad, string sede)
        {
            clsStockRepository.SumarStock("Machimbre", medida, null, null, calidad, null, cantidad, sede);
        }
        public void RestarCantidadPaquetes(string medida, string calidad, int cantidad, string sede, string detalle, long? clienteId)
        {
            clsStockRepository.RestarStock("Machimbre", medida, null, null, calidad, null, cantidad, sede, detalle, clienteId);
        }
        public void EliminarMachimbre(string medida, string calidad)
        {
            clsStockRepository.EliminarProducto("Machimbre", medida, null, null, calidad, null);
        }
        public DataTable ObtenerStockPorMedida()
        {
            return clsStockRepository.ObtenerStockPorMedida("Machimbre");
        }
        public Task GenerarReporteMachimbreAsync(string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Machimbre");
                    LlenarReporte(worksheet, clsStockRepository.ListarMachimbre(sede, filtro), sede);
                    string filePath = clsReportes.CrearArchivoReporte("StockMachimbre");
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
                DataTable datos = clsStockRepository.ListarMachimbre(sede, filtro);
                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine($"Stock de Machimbres - {sede}");
                mensaje.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                mensaje.AppendLine();

                foreach (DataRow row in datos.Rows)
                {
                    mensaje.AppendLine($"- Calidad: {row["Calidad"]} | Medida: {row["Medida"]} | Paquetes: {row["Cantidad"]} | Tablas x paquete: {row["CantidadPorUnidad"]} | Total tablas: {row["Total"]}");
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
            worksheet.Cell("A1").Value = $"Listado de Machimbres - {sede}";
            worksheet.Range("A1:E1").Merge();
            worksheet.Cell("A2").Value = "Calidad";
            worksheet.Cell("B2").Value = "Medida";
            worksheet.Cell("C2").Value = "Cant. Paquetes";
            worksheet.Cell("D2").Value = "Cant. Tablas x Paquete";
            worksheet.Cell("E2").Value = "Cant. Tablas Totales";

            int rowNum = 3;
            foreach (DataRow row in datos.Rows)
            {
                worksheet.Cell(rowNum, 1).Value = row["Calidad"].ToString();
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