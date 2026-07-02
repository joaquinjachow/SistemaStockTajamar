using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ControlStock
{
    internal class clsMaderaDura
    {
        public Int32 CantidadPaquetes;
        public String Medida;
        public Int32 CantidadTablasPaquete;
        public String Especie;
        public String SedeInicial = clsDatabase.SedeCordoba;
        public DataTable ObtenerEspeciesMaderaDura()
        {
            return clsStockRepository.ObtenerValores("MaderaDura", "Especie");
        }
        public DataTable ObtenerMedidasPorEspecies(string especies)
        {
            return clsStockRepository.ObtenerValoresFiltrados("MaderaDura", "Medida", "Especie", especies);
        }
        public DataTable ObtenerMedidasMaderasDura()
        {
            return clsStockRepository.ObtenerValores("MaderaDura", "Medida");
        }
        public void AgregarNuevaMaderaDura()
        {
            clsStockRepository.AgregarProducto("MaderaDura", Medida, null, Especie, null, null, CantidadTablasPaquete, "Paquetes", CantidadPaquetes, SedeInicial);
        }
        public void ListarMaderasDuras(DataGridView Grilla, string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                DataTable datos = clsStockRepository.ListarMaderaDura(sede, filtro);
                Grilla.Rows.Clear();

                foreach (DataRow row in datos.Rows)
                {
                    Grilla.Rows.Add(
                        row["Especie"].ToString(),
                        row["Medida"].ToString(),
                        Convert.ToInt32(row["Cantidad"]),
                        Convert.ToInt32(row["CantidadPorUnidad"]),
                        Convert.ToInt32(row["Total"])
                    );
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al listar maderas duras: " + e.Message);
            }
        }
        public void SumarCantidadPaquetes(string medida, string especie, int cantidad, string sede)
        {
            clsStockRepository.SumarStock("MaderaDura", medida, null, especie, null, null, cantidad, sede);
        }
        public void RestarCantidadPaquetes(string medida, string especie, int cantidad, string sede, string detalle, long? clienteId)
        {
            clsStockRepository.RestarStock("MaderaDura", medida, null, especie, null, null, cantidad, sede, detalle, clienteId);
        }
        public void EliminarMadera(string medida, string especie)
        {
            clsStockRepository.EliminarProducto("MaderaDura", medida, null, especie, null, null);
        }
        public DataTable ObtenerStockPorMedida()
        {
            return clsStockRepository.ObtenerStockPorMedida("MaderaDura");
        }
        public Task GenerarReporteMaderaDuraAsync(string sede = clsStockRepository.SedeTotal, string filtro = "")
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("MaderaDura");
                    LlenarReporte(worksheet, clsStockRepository.ListarMaderaDura(sede, filtro), sede);
                    string filePath = clsReportes.CrearArchivoReporte("StockMaderasDuras");
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
                DataTable datos = clsStockRepository.ListarMaderaDura(sede, filtro);
                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine($"Stock de Maderas Duras - {sede}");
                mensaje.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                mensaje.AppendLine();

                foreach (DataRow row in datos.Rows)
                {
                    mensaje.AppendLine($"- Especie: {row["Especie"]} | Medida: {row["Medida"]} | Paquetes: {row["Cantidad"]} | Tablas x paquete: {row["CantidadPorUnidad"]} | Total tablas: {row["Total"]}");
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
            worksheet.Cell("A1").Value = $"Listado de Maderas Duras - {sede}";
            worksheet.Range("A1:E1").Merge();
            worksheet.Cell("A2").Value = "Especie";
            worksheet.Cell("B2").Value = "Medida";
            worksheet.Cell("C2").Value = "Cant. Paquetes";
            worksheet.Cell("D2").Value = "Cant. Tablas x Paquete";
            worksheet.Cell("E2").Value = "Cant. Tablas Totales";

            int rowNum = 3;
            foreach (DataRow row in datos.Rows)
            {
                worksheet.Cell(rowNum, 1).Value = row["Especie"].ToString();
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