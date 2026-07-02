using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ControlStock
{
    public partial class frmGráficoMaderasDuras : Form
    {
        private clsMaderaDura maderaDura;
        public frmGráficoMaderasDuras()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
            maderaDura = new clsMaderaDura();
            CargarDatosGrafico();
        }
        private void CargarDatosGrafico()
        {
            DataTable stockPorMedida = maderaDura.ObtenerStockPorMedida();

            if (stockPorMedida == null || stockPorMedida.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para el gráfico.");
                return;
            }

            chartMaderaDura.Series.Clear();
            chartMaderaDura.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea();
            chartMaderaDura.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                Name = "Stock",
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Pie
            };
            series.Palette = ChartColorPalette.SeaGreen;
            chartMaderaDura.Series.Add(series);

            foreach (DataRow row in stockPorMedida.Rows)
            {
                series.Points.AddXY(row["Medida"], row["StockTotal"]);
            }
        }
    }
}
