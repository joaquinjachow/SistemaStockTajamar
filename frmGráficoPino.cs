using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ControlStock
{
    public partial class frmGráficoPino : Form
    {
        private clsPino pino;
        public frmGráficoPino()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
            pino = new clsPino();
            CargarDatosGrafico();
        }

        private void CargarDatosGrafico()
        {
            DataTable stockPorMedida = pino.ObtenerStockPorMedida();

            if (stockPorMedida == null || stockPorMedida.Rows.Count == 0)
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

            foreach (DataRow row in stockPorMedida.Rows)
            {
                series.Points.AddXY(row["Medida"], row["StockTotal"]);
            }
        }
    }
}
