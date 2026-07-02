namespace ControlStock
{
    partial class frmListaStockMachimbre
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
            this.grpMaderas = new System.Windows.Forms.GroupBox();
            this.grpCambios = new System.Windows.Forms.GroupBox();
            this.cmbCalidad = new System.Windows.Forms.ComboBox();
            this.lblCalidad = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cmbMachimbre = new System.Windows.Forms.ComboBox();
            this.lblMedidaMachimbre = new System.Windows.Forms.Label();
            this.btnWhatsApp = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.GrillaMachimbre = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.grpMaderas.SuspendLayout();
            this.grpCambios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaMachimbre)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMaderas
            // 
            this.grpMaderas.Controls.Add(this.grpCambios);
            this.grpMaderas.Controls.Add(this.btnWhatsApp);
            this.grpMaderas.Controls.Add(this.btnExportar);
            this.grpMaderas.Controls.Add(this.btnListar);
            this.grpMaderas.Controls.Add(this.GrillaMachimbre);
            this.grpMaderas.Location = new System.Drawing.Point(12, 12);
            this.grpMaderas.Name = "grpMaderas";
            this.grpMaderas.Size = new System.Drawing.Size(778, 812);
            this.grpMaderas.TabIndex = 1;
            this.grpMaderas.TabStop = false;
            this.grpMaderas.Text = "Machimbres";
            // 
            // grpCambios
            // 
            this.grpCambios.Controls.Add(this.btnEliminar);
            this.grpCambios.Controls.Add(this.cmbCalidad);
            this.grpCambios.Controls.Add(this.lblCalidad);
            this.grpCambios.Controls.Add(this.lblCantidad);
            this.grpCambios.Controls.Add(this.btnRestar);
            this.grpCambios.Controls.Add(this.btnAgregar);
            this.grpCambios.Controls.Add(this.txtCantidad);
            this.grpCambios.Controls.Add(this.cmbMachimbre);
            this.grpCambios.Controls.Add(this.lblMedidaMachimbre);
            this.grpCambios.Location = new System.Drawing.Point(7, 633);
            this.grpCambios.Name = "grpCambios";
            this.grpCambios.Size = new System.Drawing.Size(316, 173);
            this.grpCambios.TabIndex = 21;
            this.grpCambios.TabStop = false;
            this.grpCambios.Text = "Agregado de Machimbres";
            // 
            // cmbCalidad
            // 
            this.cmbCalidad.FormattingEnabled = true;
            this.cmbCalidad.Location = new System.Drawing.Point(162, 34);
            this.cmbCalidad.Name = "cmbCalidad";
            this.cmbCalidad.Size = new System.Drawing.Size(147, 21);
            this.cmbCalidad.TabIndex = 2;
            this.cmbCalidad.SelectedIndexChanged += new System.EventHandler(this.cmbCalidad_SelectedIndexChanged);
            // 
            // lblCalidad
            // 
            this.lblCalidad.AutoSize = true;
            this.lblCalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCalidad.Location = new System.Drawing.Point(6, 35);
            this.lblCalidad.Name = "lblCalidad";
            this.lblCalidad.Size = new System.Drawing.Size(52, 15);
            this.lblCalidad.TabIndex = 102;
            this.lblCalidad.Text = "Calidad:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCantidad.Location = new System.Drawing.Point(6, 95);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(131, 15);
            this.lblCantidad.TabIndex = 24;
            this.lblCantidad.Text = "Cantidad de Paquetes:";
            // 
            // btnRestar
            // 
            this.btnRestar.Location = new System.Drawing.Point(217, 130);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(92, 36);
            this.btnRestar.TabIndex = 6;
            this.btnRestar.Text = "Restar";
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(119, 130);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(92, 36);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(162, 95);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(147, 20);
            this.txtCantidad.TabIndex = 4;
            // 
            // cmbMachimbre
            // 
            this.cmbMachimbre.FormattingEnabled = true;
            this.cmbMachimbre.Location = new System.Drawing.Point(162, 66);
            this.cmbMachimbre.Name = "cmbMachimbre";
            this.cmbMachimbre.Size = new System.Drawing.Size(147, 21);
            this.cmbMachimbre.TabIndex = 3;
            // 
            // lblMedidaMachimbre
            // 
            this.lblMedidaMachimbre.AutoSize = true;
            this.lblMedidaMachimbre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblMedidaMachimbre.Location = new System.Drawing.Point(6, 66);
            this.lblMedidaMachimbre.Name = "lblMedidaMachimbre";
            this.lblMedidaMachimbre.Size = new System.Drawing.Size(138, 15);
            this.lblMedidaMachimbre.TabIndex = 19;
            this.lblMedidaMachimbre.Text = "Medida del Machimbre:";
            // 
            // btnWhatsApp
            // 
            this.btnWhatsApp.Location = new System.Drawing.Point(620, 747);
            this.btnWhatsApp.Name = "btnWhatsApp";
            this.btnWhatsApp.Size = new System.Drawing.Size(150, 50);
            this.btnWhatsApp.TabIndex = 2;
            this.btnWhatsApp.Text = "WhatsApp";
            this.btnWhatsApp.UseVisualStyleBackColor = true;
            this.btnWhatsApp.Click += new System.EventHandler(this.btnWhatsApp_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(620, 691);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(150, 50);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(620, 633);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(150, 50);
            this.btnListar.TabIndex = 0;
            this.btnListar.Text = "Listar";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);
            // 
            // GrillaMachimbre
            // 
            this.GrillaMachimbre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaMachimbre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6});
            this.GrillaMachimbre.Location = new System.Drawing.Point(7, 20);
            this.GrillaMachimbre.Name = "GrillaMachimbre";
            this.GrillaMachimbre.Size = new System.Drawing.Size(763, 607);
            this.GrillaMachimbre.TabIndex = 7;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Calidad";
            this.Column4.Name = "Column4";
            this.Column4.Width = 130;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Medida";
            this.Column2.Name = "Column2";
            this.Column2.Width = 130;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Cantidad Paquetes";
            this.Column3.Name = "Column3";
            this.Column3.Width = 130;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Cantidad de Tablas por Paquete";
            this.Column5.Name = "Column5";
            this.Column5.Width = 200;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Volúmen";
            this.Column6.Name = "Column6";
            this.Column6.Width = 130;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(21, 131);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(92, 36);
            this.btnEliminar.TabIndex = 103;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // frmListaStockMachimbre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 837);
            this.Controls.Add(this.grpMaderas);
            this.Name = "frmListaStockMachimbre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Stock Machimbres";
            this.Load += new System.EventHandler(this.frmListaStockMachimbre_Load);
            this.grpMaderas.ResumeLayout(false);
            this.grpCambios.ResumeLayout(false);
            this.grpCambios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaMachimbre)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMaderas;
        private System.Windows.Forms.GroupBox grpCambios;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.ComboBox cmbMachimbre;
        private System.Windows.Forms.Label lblMedidaMachimbre;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.DataGridView GrillaMachimbre;
        private System.Windows.Forms.ComboBox cmbCalidad;
        private System.Windows.Forms.Label lblCalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnWhatsApp;
    }
}
