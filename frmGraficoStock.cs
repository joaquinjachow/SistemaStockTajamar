using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ControlStock
{
    public class frmGraficoStock : Form
    {
        private ComboBox cmbRubro;
        private ComboBox cmbSede;
        private ComboBox cmbTipoGrafico;
        private Chart chartStock;

        public frmGraficoStock()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Text = "Grafico de Stock";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(820, 540);

            Label lblRubro = new Label();
            lblRubro.AutoSize = true;
            lblRubro.Location = new Point(15, 19);
            lblRubro.Text = "Rubro:";
            cmbRubro = new ComboBox();
            cmbRubro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRubro.Location = new Point(65, 16);
            cmbRubro.Size = new Size(160, 21);

            Label lblSede = new Label();
            lblSede.AutoSize = true;
            lblSede.Location = new Point(245, 19);
            lblSede.Text = "Sede:";
            cmbSede = new ComboBox();
            cmbSede.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSede.Location = new Point(290, 16);
            cmbSede.Size = new Size(140, 21);

            Label lblTipo = new Label();
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(450, 19);
            lblTipo.Text = "Tipo:";
            cmbTipoGrafico = new ComboBox();
            cmbTipoGrafico.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoGrafico.Location = new Point(490, 16);
            cmbTipoGrafico.Size = new Size(115, 21);
            cmbTipoGrafico.Items.AddRange(new object[] { "Barras", "Torta" });
            cmbTipoGrafico.SelectedIndex = 0;

            Button btnActualizar = new Button();
            btnActualizar.Text = "Actualizar";
            btnActualizar.Location = new Point(625, 12);
            btnActualizar.Size = new Size(95, 30);
            btnActualizar.Click += (sender, args) => CargarGrafico();

            chartStock = new Chart();
            chartStock.Location = new Point(12, 55);
            chartStock.Size = new Size(790, 465);
            chartStock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Controls.Add(lblRubro);
            Controls.Add(cmbRubro);
            Controls.Add(lblSede);
            Controls.Add(cmbSede);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipoGrafico);
            Controls.Add(btnActualizar);
            Controls.Add(chartStock);

            Load += frmGraficoStock_Load;
            cmbRubro.SelectedIndexChanged += (sender, args) => CargarGrafico();
            cmbSede.SelectedIndexChanged += (sender, args) => CargarGrafico();
            cmbTipoGrafico.SelectedIndexChanged += (sender, args) => CargarGrafico();
        }

        private void frmGraficoStock_Load(object sender, EventArgs e)
        {
            cmbRubro.DataSource = clsStockRepository.ObtenerRubrosConTodos();
            cmbRubro.DisplayMember = "Rubro";
            cmbSede.DataSource = clsStockRepository.ObtenerSedesConTotal();
            cmbSede.DisplayMember = "Nombre";
            cmbSede.SelectedIndex = cmbSede.FindStringExact(clsStockRepository.SedeTotal);
            CargarGrafico();
        }

        private void CargarGrafico()
        {
            if (cmbRubro == null || cmbSede == null || cmbTipoGrafico == null || chartStock == null)
            {
                return;
            }
            DataTable datos = clsStockRepository.ObtenerDatosGraficoStock(cmbRubro.Text, cmbSede.Text);
            chartStock.Series.Clear();
            chartStock.ChartAreas.Clear();
            chartStock.Titles.Clear();
            chartStock.Legends.Clear();

            ChartArea area = new ChartArea();
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -35;
            chartStock.ChartAreas.Add(area);
            chartStock.Titles.Add("Stock por " + (cmbRubro.Text == "Todos" ? "rubro" : "producto"));

            Series serie = new Series();
            serie.Name = "Stock";
            serie.ChartType = cmbTipoGrafico.Text == "Torta" ? SeriesChartType.Pie : SeriesChartType.Column;
            serie.IsValueShownAsLabel = true;
            chartStock.Series.Add(serie);
            if (cmbTipoGrafico.Text == "Torta")
            {
                chartStock.Legends.Add(new Legend("Referencias"));
                serie.Legend = "Referencias";
                serie.Label = "#PERCENT{P0}";
                serie.LegendText = "#VALX";
            }

            foreach (DataRow row in datos.Rows)
            {
                serie.Points.AddXY(Convert.ToString(row["Etiqueta"]), Convert.ToInt32(row["StockTotal"]));
            }
        }
    }
}
