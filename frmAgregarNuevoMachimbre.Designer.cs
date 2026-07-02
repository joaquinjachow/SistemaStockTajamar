namespace ControlStock
{
    partial class frmAgregarNuevoMachimbre
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
            this.grpCargadeDatos = new System.Windows.Forms.GroupBox();
            this.txtCalidad = new System.Windows.Forms.TextBox();
            this.lblCalidad = new System.Windows.Forms.Label();
            this.txtCantidadTablas = new System.Windows.Forms.TextBox();
            this.lblCantidadTablas = new System.Windows.Forms.Label();
            this.txtMedida = new System.Windows.Forms.TextBox();
            this.btnAgregarNuevo = new System.Windows.Forms.Button();
            this.lblMedida = new System.Windows.Forms.Label();
            this.txtCantidadPaquetes = new System.Windows.Forms.TextBox();
            this.lblCantidadPaquetes = new System.Windows.Forms.Label();
            this.grpCargadeDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCargadeDatos
            // 
            this.grpCargadeDatos.Controls.Add(this.txtCalidad);
            this.grpCargadeDatos.Controls.Add(this.lblCalidad);
            this.grpCargadeDatos.Controls.Add(this.txtCantidadTablas);
            this.grpCargadeDatos.Controls.Add(this.lblCantidadTablas);
            this.grpCargadeDatos.Controls.Add(this.txtMedida);
            this.grpCargadeDatos.Controls.Add(this.btnAgregarNuevo);
            this.grpCargadeDatos.Controls.Add(this.lblMedida);
            this.grpCargadeDatos.Controls.Add(this.txtCantidadPaquetes);
            this.grpCargadeDatos.Controls.Add(this.lblCantidadPaquetes);
            this.grpCargadeDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.grpCargadeDatos.Location = new System.Drawing.Point(12, 12);
            this.grpCargadeDatos.Name = "grpCargadeDatos";
            this.grpCargadeDatos.Size = new System.Drawing.Size(422, 228);
            this.grpCargadeDatos.TabIndex = 3;
            this.grpCargadeDatos.TabStop = false;
            this.grpCargadeDatos.Text = "Carga de datos";
            // 
            // txtCalidad
            // 
            this.txtCalidad.Location = new System.Drawing.Point(253, 30);
            this.txtCalidad.Name = "txtCalidad";
            this.txtCalidad.Size = new System.Drawing.Size(142, 24);
            this.txtCalidad.TabIndex = 0;
            this.txtCalidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCalidad
            // 
            this.lblCalidad.AutoSize = true;
            this.lblCalidad.Location = new System.Drawing.Point(17, 30);
            this.lblCalidad.Name = "lblCalidad";
            this.lblCalidad.Size = new System.Drawing.Size(61, 18);
            this.lblCalidad.TabIndex = 23;
            this.lblCalidad.Text = "Calidad:";
            // 
            // txtCantidadTablas
            // 
            this.txtCantidadTablas.Location = new System.Drawing.Point(253, 146);
            this.txtCantidadTablas.Name = "txtCantidadTablas";
            this.txtCantidadTablas.Size = new System.Drawing.Size(142, 24);
            this.txtCantidadTablas.TabIndex = 4;
            this.txtCantidadTablas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCantidadTablas
            // 
            this.lblCantidadTablas.AutoSize = true;
            this.lblCantidadTablas.Location = new System.Drawing.Point(17, 146);
            this.lblCantidadTablas.Name = "lblCantidadTablas";
            this.lblCantidadTablas.Size = new System.Drawing.Size(222, 18);
            this.lblCantidadTablas.TabIndex = 19;
            this.lblCantidadTablas.Text = "Cantidad de Tablas por Paquete:";
            // 
            // txtMedida
            // 
            this.txtMedida.Location = new System.Drawing.Point(253, 108);
            this.txtMedida.Name = "txtMedida";
            this.txtMedida.Size = new System.Drawing.Size(142, 24);
            this.txtMedida.TabIndex = 3;
            this.txtMedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAgregarNuevo
            // 
            this.btnAgregarNuevo.Location = new System.Drawing.Point(253, 190);
            this.btnAgregarNuevo.Name = "btnAgregarNuevo";
            this.btnAgregarNuevo.Size = new System.Drawing.Size(142, 29);
            this.btnAgregarNuevo.TabIndex = 5;
            this.btnAgregarNuevo.Text = "Agregar Nuevo";
            this.btnAgregarNuevo.UseVisualStyleBackColor = true;
            this.btnAgregarNuevo.Click += new System.EventHandler(this.btnAgregarNuevo_Click);
            // 
            // lblMedida
            // 
            this.lblMedida.AutoSize = true;
            this.lblMedida.Location = new System.Drawing.Point(17, 108);
            this.lblMedida.Name = "lblMedida";
            this.lblMedida.Size = new System.Drawing.Size(60, 18);
            this.lblMedida.TabIndex = 9;
            this.lblMedida.Text = "Medida:";
            // 
            // txtCantidadPaquetes
            // 
            this.txtCantidadPaquetes.Location = new System.Drawing.Point(253, 68);
            this.txtCantidadPaquetes.Name = "txtCantidadPaquetes";
            this.txtCantidadPaquetes.Size = new System.Drawing.Size(142, 24);
            this.txtCantidadPaquetes.TabIndex = 2;
            this.txtCantidadPaquetes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCantidadPaquetes
            // 
            this.lblCantidadPaquetes.AutoSize = true;
            this.lblCantidadPaquetes.Location = new System.Drawing.Point(17, 68);
            this.lblCantidadPaquetes.Name = "lblCantidadPaquetes";
            this.lblCantidadPaquetes.Size = new System.Drawing.Size(136, 18);
            this.lblCantidadPaquetes.TabIndex = 2;
            this.lblCantidadPaquetes.Text = "Cantidad Paquetes:";
            // 
            // frmAgregarNuevoMachimbre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 243);
            this.Controls.Add(this.grpCargadeDatos);
            this.Name = "frmAgregarNuevoMachimbre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Nuevo Machimbre";
            this.grpCargadeDatos.ResumeLayout(false);
            this.grpCargadeDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCargadeDatos;
        private System.Windows.Forms.TextBox txtCantidadTablas;
        private System.Windows.Forms.Label lblCantidadTablas;
        private System.Windows.Forms.TextBox txtMedida;
        private System.Windows.Forms.Button btnAgregarNuevo;
        private System.Windows.Forms.Label lblMedida;
        private System.Windows.Forms.TextBox txtCantidadPaquetes;
        private System.Windows.Forms.Label lblCantidadPaquetes;
        private System.Windows.Forms.TextBox txtCalidad;
        private System.Windows.Forms.Label lblCalidad;
    }
}