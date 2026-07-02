using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ControlStock
{
    internal class clsFenolicos
    {
        public String Calidad;
        public String Espesor;
        public Int32 CantidadHojasPaquete;
        public Int32 CantidadHojasTotales;
        public String SedeInicial = clsDatabase.SedeCordoba;
        public DataTable ObtenerCalidadesFenolicos()
        {
            return clsStockRepository.ObtenerValores("Fenolicos", "Calidad");
        }
        public DataTable ObtenerEspesorPorCalidad(string calidad)
        {
            return clsStockRepository.ObtenerValoresFiltrados("Fenolicos", "Espesor", "Calidad", calidad);
        }
        public DataTable ObtenerEspesor()
        {
            return clsStockRepository.ObtenerValores("Fenolicos", "Espesor");
        }
        public void AgregarNuevoFenolicos()
        {
            clsStockRepository.AgregarProducto("Fenolicos", null, null, null, Calidad, Espesor, CantidadHojasPaquete, "Hojas", CantidadHojasTotales, SedeInicial);
        }
        public void ListarFenolicos(DataGridView Grilla, string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                DataTable datos = clsStockRepository.ListarFenolicos(sede, filtro);
                Grilla.Rows.Clear();
                foreach (DataRow row in datos.Rows)
                {
                    Grilla.Rows.Add(
                        row["Calidad"].ToString(),
                        row["Espesor"].ToString(),
                        Convert.ToInt32(row["CantidadPorUnidad"]),
                        Convert.ToInt32(row["Cantidad"])
                    );
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al listar fenolicos: " + e.Message);
            }
        }
        public void SumarCantidadHojas(int cantidad, string calidad, string espesor, string sede)
        {
            clsStockRepository.SumarStock("Fenolicos", null, null, null, calidad, espesor, cantidad, sede);
        }
        public void RestarCantidadHojas(int cantidad, string calidad, string espesor, string sede, string detalle, long? clienteId)
        {
            clsStockRepository.RestarStock("Fenolicos", null, null, null, calidad, espesor, cantidad, sede, detalle, clienteId);
        }
        public void EliminarFenolicos(string calidad, string espesor)
        {
            clsStockRepository.EliminarProducto("Fenolicos", null, null, null, calidad, espesor);
        }
        public DataTable ObtenerDatosParaGrafico()
        {
            return clsStockRepository.ObtenerStockFenolicosParaGrafico();
        }
        public Task GenerarReporteFenolicosAsync(string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Fenolicos");
                    LlenarReporte(worksheet, clsStockRepository.ListarFenolicos(sede, filtro), sede);
                    string filePath = clsReportes.CrearArchivoReporte("StockFenolicos");
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
                DataTable datos = clsStockRepository.ListarFenolicos(sede, filtro);
                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine($"Stock de Fenolicos - {sede}");
                mensaje.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                mensaje.AppendLine();

                foreach (DataRow row in datos.Rows)
                {
                    mensaje.AppendLine($"- Calidad: {row["Calidad"]} | Espesor: {row["Espesor"]} | Hojas x paquete: {row["CantidadPorUnidad"]} | Hojas totales: {row["Cantidad"]}");
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
            worksheet.Cell("A1").Value = $"Listado de Fenolicos - {sede}";
            worksheet.Range("A1:D1").Merge();
            worksheet.Cell("A2").Value = "Calidad";
            worksheet.Cell("B2").Value = "Espesor";
            worksheet.Cell("C2").Value = "Cant. Hojas x Paquete";
            worksheet.Cell("D2").Value = "Cant. Hojas Totales";

            int rowNum = 3;
            foreach (DataRow row in datos.Rows)
            {
                worksheet.Cell(rowNum, 1).Value = row["Calidad"].ToString();
                worksheet.Cell(rowNum, 2).Value = row["Espesor"].ToString();
                worksheet.Cell(rowNum, 3).Value = Convert.ToInt32(row["CantidadPorUnidad"]);
                worksheet.Cell(rowNum, 4).Value = Convert.ToInt32(row["Cantidad"]);
                rowNum++;
            }
            clsReportes.AplicarEstilosListado(worksheet);
        }
    }
}