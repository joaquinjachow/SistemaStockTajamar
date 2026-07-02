namespace ControlStock
{
    partial class frmGráficoMaderasDuras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartMaderaDura = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartMaderaDura)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMaderaDura
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMaderaDura.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMaderaDura.Legends.Add(legend1);
            this.chartMaderaDura.Location = new System.Drawing.Point(13, 13);
            this.chartMaderaDura.Name = "chartMaderaDura";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartMaderaDura.Series.Add(series1);
            this.chartMaderaDura.Size = new System.Drawing.Size(432, 425);
            this.chartMaderaDura.TabIndex = 0;
            this.chartMaderaDura.Text = "chart1";
            // 
            // frmGráficoMaderasDuras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 445);
            this.Controls.Add(this.chartMaderaDura);
            this.Name = "frmGráficoMaderasDuras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gráfico Maderas Duras";
            ((System.ComponentModel.ISupportInitialize)(this.chartMaderaDura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMaderaDura;
    }
}