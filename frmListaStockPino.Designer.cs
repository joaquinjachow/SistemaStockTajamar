namespace ControlStock
{
    partial class frmListaStockPino
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
            this.grpPino = new System.Windows.Forms.GroupBox();
            this.grpCambios = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.cmbSecado = new System.Windows.Forms.ComboBox();
            this.lblSecado = new System.Windows.Forms.Label();
            this.lblCantidadPaquetes = new System.Windows.Forms.Label();
            this.btnRestar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cmbPino = new System.Windows.Forms.ComboBox();
            this.lblMedidaPino = new System.Windows.Forms.Label();
            this.btnWhatsApp = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.GrillaMaderas = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpPino.SuspendLayout();
            this.grpCambios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaMaderas)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPino
            // 
            this.grpPino.Controls.Add(this.grpCambios);
            this.grpPino.Controls.Add(this.btnWhatsApp);
            this.grpPino.Controls.Add(this.btnExportar);
            this.grpPino.Controls.Add(this.btnListar);
            this.grpPino.Controls.Add(this.GrillaMaderas);
            this.grpPino.Location = new System.Drawing.Point(12, 12);
            this.grpPino.Name = "grpPino";
            this.grpPino.Size = new System.Drawing.Size(779, 812);
            this.grpPino.TabIndex = 0;
            this.grpPino.TabStop = false;
            this.grpPino.Text = "Pino";
            // 
            // grpCambios
            // 
            this.grpCambios.Controls.Add(this.btnEliminar);
            this.grpCambios.Controls.Add(this.cmbSecado);
            this.grpCambios.Controls.Add(this.lblSecado);
            this.grpCambios.Controls.Add(this.lblCantidadPaquetes);
            this.grpCambios.Controls.Add(this.btnRestar);
            this.grpCambios.Controls.Add(this.btnAgregar);
            this.grpCambios.Controls.Add(this.txtCantidad);
            this.grpCambios.Controls.Add(this.cmbPino);
            this.grpCambios.Controls.Add(this.lblMedidaPino);
            this.grpCambios.Location = new System.Drawing.Point(7, 619);
            this.grpCambios.Name = "grpCambios";
            this.grpCambios.Size = new System.Drawing.Size(302, 187);
            this.grpCambios.TabIndex = 21;
            this.grpCambios.TabStop = false;
            this.grpCambios.Text = "Agregado de Pino";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(5, 137);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(92, 36);
            this.btnEliminar.TabIndex = 27;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // cmbSecado
            // 
            this.cmbSecado.FormattingEnabled = true;
            this.cmbSecado.Location = new System.Drawing.Point(146, 33);
            this.cmbSecado.Name = "cmbSecado";
            this.cmbSecado.Size = new System.Drawing.Size(147, 21);
            this.cmbSecado.TabIndex = 2;
            this.cmbSecado.SelectedIndexChanged += new System.EventHandler(this.cmbSecado_SelectedIndexChanged);
            // 
            // lblSecado
            // 
            this.lblSecado.AutoSize = true;
            this.lblSecado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSecado.Location = new System.Drawing.Point(9, 33);
            this.lblSecado.Name = "lblSecado";
            this.lblSecado.Size = new System.Drawing.Size(52, 15);
            this.lblSecado.TabIndex = 26;
            this.lblSecado.Text = "Secado:";
            // 
            // lblCantidadPaquetes
            // 
            this.lblCantidadPaquetes.AutoSize = true;
            this.lblCantidadPaquetes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCantidadPaquetes.Location = new System.Drawing.Point(9, 102);
            this.lblCantidadPaquetes.Name = "lblCantidadPaquetes";
            this.lblCantidadPaquetes.Size = new System.Drawing.Size(131, 15);
            this.lblCantidadPaquetes.TabIndex = 24;
            this.lblCantidadPaquetes.Text = "Cantidad de Paquetes:";
            // 
            // btnRestar
            // 
            this.btnRestar.Location = new System.Drawing.Point(201, 137);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(92, 36);
            this.btnRestar.TabIndex = 6;
            this.btnRestar.Text = "Restar";
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(103, 137);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(92, 36);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(146, 102);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(147, 20);
            this.txtCantidad.TabIndex = 4;
            // 
            // cmbPino
            // 
            this.cmbPino.FormattingEnabled = true;
            this.cmbPino.Location = new System.Drawing.Point(146, 67);
            this.cmbPino.Name = "cmbPino";
            this.cmbPino.Size = new System.Drawing.Size(147, 21);
            this.cmbPino.TabIndex = 3;
            // 
            // lblMedidaPino
            // 
            this.lblMedidaPino.AutoSize = true;
            this.lblMedidaPino.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblMedidaPino.Location = new System.Drawing.Point(9, 68);
            this.lblMedidaPino.Name = "lblMedidaPino";
            this.lblMedidaPino.Size = new System.Drawing.Size(99, 15);
            this.lblMedidaPino.TabIndex = 19;
            this.lblMedidaPino.Text = "Medida del pino:";
            // 
            // btnWhatsApp
            // 
            this.btnWhatsApp.Location = new System.Drawing.Point(624, 733);
            this.btnWhatsApp.Name = "btnWhatsApp";
            this.btnWhatsApp.Size = new System.Drawing.Size(150, 50);
            this.btnWhatsApp.TabIndex = 2;
            this.btnWhatsApp.Text = "WhatsApp";
            this.btnWhatsApp.UseVisualStyleBackColor = true;
            this.btnWhatsApp.Click += new System.EventHandler(this.btnWhatsApp_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(624, 676);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(150, 50);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(624, 619);
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
            this.GrillaMaderas.Size = new System.Drawing.Size(767, 591);
            this.GrillaMaderas.TabIndex = 7;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Secado";
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
            // frmListaStockPino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 828);
            this.Controls.Add(this.grpPino);
            this.Name = "frmListaStockPino";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista Stock Pino";
            this.Load += new System.EventHandler(this.frmListaStockMaderas_Load);
            this.grpPino.ResumeLayout(false);
            this.grpCambios.ResumeLayout(false);
            this.grpCambios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaMaderas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPino;
        private System.Windows.Forms.DataGridView GrillaMaderas;
        private System.Windows.Forms.GroupBox grpCambios;
        private System.Windows.Forms.ComboBox cmbPino;
        private System.Windows.Forms.Label lblMedidaPino;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblCantidadPaquetes;
        private System.Windows.Forms.ComboBox cmbSecado;
        private System.Windows.Forms.Label lblSecado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnWhatsApp;
    }
}
