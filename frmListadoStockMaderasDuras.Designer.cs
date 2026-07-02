namespace ControlStock
{
    partial class frmListadoStockMaderasDuras
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
            this.cmbEspecie = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cmbMadera = new System.Windows.Forms.ComboBox();
            this.lblMedidaMadera = new System.Windows.Forms.Label();
            this.btnWhatsApp = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.GrillaMaderas = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.grpMaderas.SuspendLayout();
            this.grpCambios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaMaderas)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMaderas
            // 
            this.grpMaderas.Controls.Add(this.grpCambios);
            this.grpMaderas.Controls.Add(this.btnWhatsApp);
            this.grpMaderas.Controls.Add(this.btnExportar);
            this.grpMaderas.Controls.Add(this.btnListar);
            this.grpMaderas.Controls.Add(this.GrillaMaderas);
            this.grpMaderas.Location = new System.Drawing.Point(12, 12);
            this.grpMaderas.Name = "grpMaderas";
            this.grpMaderas.Size = new System.Drawing.Size(749, 812);
            this.grpMaderas.TabIndex = 1;
            this.grpMaderas.TabStop = false;
            this.grpMaderas.Text = "Maderas Duras";
            // 
            // grpCambios
            // 
            this.grpCambios.Controls.Add(this.btnEliminar);
            this.grpCambios.Controls.Add(this.cmbEspecie);
            this.grpCambios.Controls.Add(this.label1);
            this.grpCambios.Controls.Add(this.lblCantidad);
            this.grpCambios.Controls.Add(this.btnRestar);
            this.grpCambios.Controls.Add(this.btnAgregar);
            this.grpCambios.Controls.Add(this.txtCantidad);
            this.grpCambios.Controls.Add(this.cmbMadera);
            this.grpCambios.Controls.Add(this.lblMedidaMadera);
            this.grpCambios.Location = new System.Drawing.Point(16, 651);
            this.grpCambios.Name = "grpCambios";
            this.grpCambios.Size = new System.Drawing.Size(302, 155);
            this.grpCambios.TabIndex = 21;
            this.grpCambios.TabStop = false;
            this.grpCambios.Text = "Agregado de Maderas";
            // 
            // cmbEspecie
            // 
            this.cmbEspecie.FormattingEnabled = true;
            this.cmbEspecie.Location = new System.Drawing.Point(143, 26);
            this.cmbEspecie.Name = "cmbEspecie";
            this.cmbEspecie.Size = new System.Drawing.Size(147, 21);
            this.cmbEspecie.TabIndex = 2;
            this.cmbEspecie.SelectedIndexChanged += new System.EventHandler(this.cmbEspecie_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Especie:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCantidad.Location = new System.Drawing.Point(6, 82);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(131, 15);
            this.lblCantidad.TabIndex = 24;
            this.lblCantidad.Text = "Cantidad de Paquetes:";
            // 
            // btnRestar
            // 
            this.btnRestar.Location = new System.Drawing.Point(198, 108);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(92, 36);
            this.btnRestar.TabIndex = 6;
            this.btnRestar.Text = "Restar";
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(100, 108);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(92, 36);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(143, 82);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(147, 20);
            this.txtCantidad.TabIndex = 4;
            // 
            // cmbMadera
            // 
            this.cmbMadera.FormattingEnabled = true;
            this.cmbMadera.Location = new System.Drawing.Point(143, 53);
            this.cmbMadera.Name = "cmbMadera";
            this.cmbMadera.Size = new System.Drawing.Size(147, 21);
            this.cmbMadera.TabIndex = 3;
            // 
            // lblMedidaMadera
            // 
            this.lblMedidaMadera.AutoSize = true;
            this.lblMedidaMadera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblMedidaMadera.Location = new System.Drawing.Point(6, 56);
            this.lblMedidaMadera.Name = "lblMedidaMadera";
            this.lblMedidaMadera.Size = new System.Drawing.Size(128, 15);
            this.lblMedidaMadera.TabIndex = 19;
            this.lblMedidaMadera.Text = "Medida de la Madera:";
            // 
            // btnWhatsApp
            // 
            this.btnWhatsApp.Location = new System.Drawing.Point(592, 763);
            this.btnWhatsApp.Name = "btnWhatsApp";
            this.btnWhatsApp.Size = new System.Drawing.Size(150, 43);
            this.btnWhatsApp.TabIndex = 2;
            this.btnWhatsApp.Text = "WhatsApp";
            this.btnWhatsApp.UseVisualStyleBackColor = true;
            this.btnWhatsApp.Click += new System.EventHandler(this.btnWhatsApp_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(592, 707);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(150, 50);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(592, 651);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(150, 50);
            this.btnListar.TabIndex = 0;
            this.btnListar.Text = "Listar";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);
            // 
            // GrillaMaderas
            // 
            this.GrillaMaderas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaMaderas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6});
            this.GrillaMaderas.Location = new System.Drawing.Point(7, 20);
            this.GrillaMaderas.Name = "GrillaMaderas";
            this.GrillaMaderas.Size = new System.Drawing.Size(735, 625);
            this.GrillaMaderas.TabIndex = 7;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 130F;
            this.Column4.HeaderText = "Especie";
            this.Column4.Name = "Column4";
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
            this.btnEliminar.Location = new System.Drawing.Point(2, 108);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(92, 36);
            this.btnEliminar.TabIndex = 28;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // frmListadoStockMaderasDuras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 830);
            this.Controls.Add(this.grpMaderas);
            this.Name = "frmListadoStockMaderasDuras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Stock Maderas Duras";
            this.Load += new System.EventHandler(this.frmListadoStockMaderasDuras_Load);
            this.grpMaderas.ResumeLayout(false);
            this.grpCambios.ResumeLayout(false);
            this.grpCambios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaMaderas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMaderas;
        private System.Windows.Forms.GroupBox grpCambios;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.ComboBox cmbMadera;
        private System.Windows.Forms.Label lblMedidaMadera;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.DataGridView GrillaMaderas;
        private System.Windows.Forms.ComboBox cmbEspecie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnWhatsApp;
    }
}
