using System;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace ControlStock
{
    internal static class clsReportes
    {
        public static string GenerarReporteGeneral(string sede)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                AgregarHoja(workbook, "Stock", clsStockRepository.ListarStock("Todos", sede));

                string archivo = CrearArchivoReporte($"StockGeneral_{sede}");
                workbook.SaveAs(archivo);
                return archivo;
            }
        }
        public static string GenerarReporteHistorial(string filtro, string fechaDesde, string fechaHasta, string tipo, string sede, string rubro)
        {
            return GenerarReporteHistorial(filtro, fechaDesde, fechaHasta, tipo, sede, rubro, string.Empty);
        }
        public static string GenerarReporteHistorial(string filtro, string fechaDesde, string fechaHasta, string tipo, string sede, string rubro, string cliente)
        {
            DataTable datos = clsStockRepository.ListarMovimientos(filtro, fechaDesde, fechaHasta, tipo, sede, rubro, cliente);
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet hoja = workbook.Worksheets.Add("Historial");
                hoja.Cell(1, 1).Value = "Historial de movimientos";
                hoja.Cell(1, 1).Style.Font.Bold = true;
                hoja.Cell(1, 1).Style.Font.FontSize = 14;
                hoja.Cell(2, 1).Value = "Filtros: " + ResumenFiltros(fechaDesde, fechaHasta, tipo, sede, rubro, filtro, cliente);
                InsertarTablaSimple(hoja, 4, datos);
                AplicarFormatoSimple(hoja);

                string archivo = CrearArchivoReporte("HistorialMovimientos");
                workbook.SaveAs(archivo);
                return archivo;
            }
        }
        public static string GenerarReporteClientes(string filtro)
        {
            DataTable datos = new clsCliente().BuscarClientes(filtro);
            PrepararColumnasClientes(datos);
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet hoja = workbook.Worksheets.Add("Clientes");
                hoja.Cell(1, 1).Value = "Listado de clientes";
                hoja.Cell(1, 1).Style.Font.Bold = true;
                hoja.Cell(1, 1).Style.Font.FontSize = 14;
                hoja.Cell(2, 1).Value = "Busqueda: " + (string.IsNullOrWhiteSpace(filtro) ? "sin filtro" : filtro.Trim());
                InsertarTablaSimple(hoja, 4, datos);
                AplicarFormatoSimple(hoja);

                string archivo = CrearArchivoReporte("Clientes");
                workbook.SaveAs(archivo);
                return archivo;
            }
        }
        public static string GenerarReporteStock(DataTable datos, string nombreReporte)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet hoja = workbook.Worksheets.Add("Stock");
                hoja.Cell(1, 1).Value = "Listado de stock";
                hoja.Cell(1, 1).Style.Font.Bold = true;
                hoja.Cell(1, 1).Style.Font.FontSize = 14;
                InsertarTablaSimple(hoja, 3, datos);
                AplicarFormatoSimple(hoja);

                string archivo = CrearArchivoReporte(nombreReporte);
                workbook.SaveAs(archivo);
                return archivo;
            }
        }
        public static string CrearArchivoReporte(string nombre)
        {
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string carpeta = Path.Combine(escritorio, "Reportes");
            Directory.CreateDirectory(carpeta);
            string nombreSeguro = string.IsNullOrWhiteSpace(nombre) ? "Reporte" : nombre.Trim();
            foreach (char caracter in Path.GetInvalidFileNameChars())
            {
                nombreSeguro = nombreSeguro.Replace(caracter, '_');
            }
            string archivo = Path.Combine(carpeta, nombreSeguro + ".xlsx");
            if (File.Exists(archivo))
            {
                File.Delete(archivo);
            }
            return archivo;
        }
        public static void AplicarEstilosListado(IXLWorksheet worksheet)
        {
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
            worksheet.CellsUsed().Style.Font.FontSize = 12;
            worksheet.Cell("A1").Style.Font.FontSize = 22;
            worksheet.Cells().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(worksheet.FirstCellUsed(), worksheet.LastCellUsed()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.FirstCellUsed(), worksheet.LastCellUsed()).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.Columns().AdjustToContents();
        }
        private static void AgregarHoja(XLWorkbook workbook, string nombre, DataTable datos)
        {
            IXLWorksheet hoja = workbook.Worksheets.Add(nombre);
            hoja.Cell(1, 1).Value = nombre;
            hoja.Cell(1, 1).Style.Font.Bold = true;
            hoja.Cell(1, 1).Style.Font.FontSize = 14;
            InsertarTablaSimple(hoja, 3, datos);
            AplicarFormatoSimple(hoja);
        }
        private static void InsertarTablaSimple(IXLWorksheet hoja, int fila, DataTable datos)
        {
            IXLTable tabla = hoja.Cell(fila, 1).InsertTable(datos, true);
            tabla.Theme = XLTableTheme.None;
        }
        private static void AplicarFormatoSimple(IXLWorksheet hoja)
        {
            hoja.CellsUsed().Style.Font.FontSize = 11;
            hoja.CellsUsed().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            hoja.Cell(1, 1).Style.Font.FontSize = 14;
            hoja.Cell(1, 1).Style.Font.Bold = true;
            hoja.Range(hoja.FirstCellUsed(), hoja.LastCellUsed()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            hoja.Range(hoja.FirstCellUsed(), hoja.LastCellUsed()).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            hoja.Columns().AdjustToContents();
        }
        private static string ResumenFiltros(string fechaDesde, string fechaHasta, string tipo, string sede, string rubro, string filtro, string cliente)
        {
            string desde = string.IsNullOrWhiteSpace(fechaDesde) ? "sin desde" : fechaDesde;
            string hasta = string.IsNullOrWhiteSpace(fechaHasta) ? "sin hasta" : fechaHasta;
            string tipoTexto = string.IsNullOrWhiteSpace(tipo) ? "todos los tipos" : tipo;
            string sedeTexto = string.IsNullOrWhiteSpace(sede) ? "todas las sedes" : sede;
            string rubroTexto = string.IsNullOrWhiteSpace(rubro) ? "todos los rubros" : rubro;
            string busqueda = string.IsNullOrWhiteSpace(filtro) ? "sin busqueda" : filtro;
            string clienteTexto = string.IsNullOrWhiteSpace(cliente) ? "todos los clientes" : cliente;
            return desde + " / " + hasta + " / " + tipoTexto + " / " + sedeTexto + " / " + rubroTexto + " / " + busqueda + " / " + clienteTexto;
        }
        private static void PrepararColumnasClientes(DataTable datos)
        {
            RenombrarColumna(datos, "IdCliente", "ID");
            RenombrarColumna(datos, "DireccionComercial", "Direccion comercial");
            RenombrarColumna(datos, "Cuit", "CUIT");
            RenombrarColumna(datos, "CondicionIva", "Condicion frente al IVA");
            RenombrarColumna(datos, "DireccionLegal", "Direccion legal");
        }
        private static void RenombrarColumna(DataTable datos, string actual, string nuevo)
        {
            if (datos.Columns.Contains(actual) && !datos.Columns.Contains(nuevo))
            {
                datos.Columns[actual].ColumnName = nuevo;
            }
        }
    }
}
