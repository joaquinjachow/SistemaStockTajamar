namespace ControlStock
{
    partial class frmListaStockFenólicos
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
            this.lblCalidadFenolico = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cmbEspesor = new System.Windows.Forms.ComboBox();
            this.lblEspesor = new System.Windows.Forms.Label();
            this.btnWhatsApp = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.GrillaFenolicos = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.grpMaderas.SuspendLayout();
            this.grpCambios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaFenolicos)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMaderas
            // 
            this.grpMaderas.Controls.Add(this.grpCambios);
            this.grpMaderas.Controls.Add(this.btnWhatsApp);
            this.grpMaderas.Controls.Add(this.btnExportar);
            this.grpMaderas.Controls.Add(this.btnListar);
            this.grpMaderas.Controls.Add(this.GrillaFenolicos);
            this.grpMaderas.Location = new System.Drawing.Point(12, 12);
            this.grpMaderas.Name = "grpMaderas";
            this.grpMaderas.Size = new System.Drawing.Size(712, 812);
            this.grpMaderas.TabIndex = 2;
            this.grpMaderas.TabStop = false;
            this.grpMaderas.Text = "Fenólicos";
            // 
            // grpCambios
            // 
            this.grpCambios.Controls.Add(this.btnEliminar);
            this.grpCambios.Controls.Add(this.cmbCalidad);
            this.grpCambios.Controls.Add(this.lblCalidadFenolico);
            this.grpCambios.Controls.Add(this.lblCantidad);
            this.grpCambios.Controls.Add(this.btnRestar);
            this.grpCambios.Controls.Add(this.btnAgregar);
            this.grpCambios.Controls.Add(this.txtCantidad);
            this.grpCambios.Controls.Add(this.cmbEspesor);
            this.grpCambios.Controls.Add(this.lblEspesor);
            this.grpCambios.Location = new System.Drawing.Point(7, 642);
            this.grpCambios.Name = "grpCambios";
            this.grpCambios.Size = new System.Drawing.Size(302, 164);
            this.grpCambios.TabIndex = 21;
            this.grpCambios.TabStop = false;
            this.grpCambios.Text = "Agregado de Fenólicos";
            // 
            // cmbCalidad
            // 
            this.cmbCalidad.FormattingEnabled = true;
            this.cmbCalidad.Location = new System.Drawing.Point(143, 27);
            this.cmbCalidad.Name = "cmbCalidad";
            this.cmbCalidad.Size = new System.Drawing.Size(147, 21);
            this.cmbCalidad.TabIndex = 2;
            this.cmbCalidad.SelectedIndexChanged += new System.EventHandler(this.cmbCalidad_SelectedIndexChanged);
            // 
            // lblCalidadFenolico
            // 
            this.lblCalidadFenolico.AutoSize = true;
            this.lblCalidadFenolico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCalidadFenolico.Location = new System.Drawing.Point(6, 27);
            this.lblCalidadFenolico.Name = "lblCalidadFenolico";
            this.lblCalidadFenolico.Size = new System.Drawing.Size(122, 15);
            this.lblCalidadFenolico.TabIndex = 26;
            this.lblCalidadFenolico.Text = "Calidad del Fenólico:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCantidad.Location = new System.Drawing.Point(6, 82);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(111, 15);
            this.lblCantidad.TabIndex = 24;
            this.lblCantidad.Text = "Cantidad de Hojas:";
            // 
            // btnRestar
            // 
            this.btnRestar.Location = new System.Drawing.Point(198, 116);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(92, 36);
            this.btnRestar.TabIndex = 6;
            this.btnRestar.Text = "Restar";
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(100, 116);
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
            // cmbEspesor
            // 
            this.cmbEspesor.FormattingEnabled = true;
            this.cmbEspesor.Location = new System.Drawing.Point(143, 54);
            this.cmbEspesor.Name = "cmbEspesor";
            this.cmbEspesor.Size = new System.Drawing.Size(147, 21);
            this.cmbEspesor.TabIndex = 3;
            // 
            // lblEspesor
            // 
            this.lblEspesor.AutoSize = true;
            this.lblEspesor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblEspesor.Location = new System.Drawing.Point(6, 54);
            this.lblEspesor.Name = "lblEspesor";
            this.lblEspesor.Size = new System.Drawing.Size(55, 15);
            this.lblEspesor.TabIndex = 19;
            this.lblEspesor.Text = "Espesor:";
            // 
            // btnWhatsApp
            // 
            this.btnWhatsApp.Location = new System.Drawing.Point(553, 755);
            this.btnWhatsApp.Name = "btnWhatsApp";
            this.btnWhatsApp.Size = new System.Drawing.Size(150, 50);
            this.btnWhatsApp.TabIndex = 2;
            this.btnWhatsApp.Text = "WhatsApp";
            this.btnWhatsApp.UseVisualStyleBackColor = true;
            this.btnWhatsApp.Click += new System.EventHandler(this.btnWhatsApp_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(553, 699);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(150, 50);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(553, 642);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(150, 50);
            this.btnListar.TabIndex = 0;
            this.btnListar.Text = "Listar";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);
            // 
            // GrillaFenolicos
            // 
            this.GrillaFenolicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaFenolicos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column2,
            this.Column5,
            this.Column6});
            this.GrillaFenolicos.Location = new System.Drawing.Point(7, 20);
            this.GrillaFenolicos.Name = "GrillaFenolicos";
            this.GrillaFenolicos.Size = new System.Drawing.Size(696, 616);
            this.GrillaFenolicos.TabIndex = 7;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Calidad";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Espesor";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Cantidad de Hojas por Paquete";
            this.Column5.Name = "Column5";
            this.Column5.Width = 200;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Cantidad de Hojas Totales";
            this.Column6.Name = "Column6";
            this.Column6.Width = 150;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(2, 116);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(92, 36);
            this.btnEliminar.TabIndex = 28;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // frmListaStockFenólicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 832);
            this.Controls.Add(this.grpMaderas);
            this.Name = "frmListaStockFenólicos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Stock Fenólicos";
            this.Load += new System.EventHandler(this.frmListaStockFenólicos_Load);
            this.grpMaderas.ResumeLayout(false);
            this.grpCambios.ResumeLayout(false);
            this.grpCambios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaFenolicos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMaderas;
        private System.Windows.Forms.GroupBox grpCambios;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.ComboBox cmbEspesor;
        private System.Windows.Forms.Label lblEspesor;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.DataGridView GrillaFenolicos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.ComboBox cmbCalidad;
        private System.Windows.Forms.Label lblCalidadFenolico;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnWhatsApp;
    }
}
