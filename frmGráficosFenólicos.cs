using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ControlStock
{
    public partial class frmGráficosFenólicos : Form
    {
        private clsFenolicos fenolicos;
        public frmGráficosFenólicos()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
            fenolicos = new clsFenolicos();
            CargarDatosGrafico();
        }
        private void CargarDatosGrafico()
        {
            DataTable datos = fenolicos.ObtenerDatosParaGrafico();

            if (datos == null || datos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para el gráfico.");
                return;
            }

            chartStock.Series.Clear();
            chartStock.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea();
            chartStock.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                Name = "Stock",
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Pie
            };
            series.Palette = ChartColorPalette.SeaGreen;
            chartStock.Series.Add(series);

            foreach (DataRow row in datos.Rows)
            {
                series.Points.AddXY(row["Calidad"], row["StockTotal"]);
            }
        }
    }
}
