namespace ControlStock
{
    partial class frmAgregarNuevaMaderaDura
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
            this.txtEspecie = new System.Windows.Forms.TextBox();
            this.lblEspecie = new System.Windows.Forms.Label();
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
            this.grpCargadeDatos.Controls.Add(this.txtEspecie);
            this.grpCargadeDatos.Controls.Add(this.lblEspecie);
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
            this.grpCargadeDatos.Size = new System.Drawing.Size(407, 214);
            this.grpCargadeDatos.TabIndex = 3;
            this.grpCargadeDatos.TabStop = false;
            this.grpCargadeDatos.Text = "Carga de datos";
            // 
            // txtEspecie
            // 
            this.txtEspecie.Location = new System.Drawing.Point(250, 33);
            this.txtEspecie.Name = "txtEspecie";
            this.txtEspecie.Size = new System.Drawing.Size(142, 24);
            this.txtEspecie.TabIndex = 0;
            this.txtEspecie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblEspecie
            // 
            this.lblEspecie.AutoSize = true;
            this.lblEspecie.Location = new System.Drawing.Point(14, 33);
            this.lblEspecie.Name = "lblEspecie";
            this.lblEspecie.Size = new System.Drawing.Size(65, 18);
            this.lblEspecie.TabIndex = 21;
            this.lblEspecie.Text = "Especie:";
            // 
            // txtCantidadTablas
            // 
            this.txtCantidadTablas.Location = new System.Drawing.Point(250, 138);
            this.txtCantidadTablas.Name = "txtCantidadTablas";
            this.txtCantidadTablas.Size = new System.Drawing.Size(142, 24);
            this.txtCantidadTablas.TabIndex = 4;
            this.txtCantidadTablas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCantidadTablas
            // 
            this.lblCantidadTablas.AutoSize = true;
            this.lblCantidadTablas.Location = new System.Drawing.Point(14, 138);
            this.lblCantidadTablas.Name = "lblCantidadTablas";
            this.lblCantidadTablas.Size = new System.Drawing.Size(222, 18);
            this.lblCantidadTablas.TabIndex = 19;
            this.lblCantidadTablas.Text = "Cantidad de Tablas por Paquete:";
            // 
            // txtMedida
            // 
            this.txtMedida.Location = new System.Drawing.Point(250, 104);
            this.txtMedida.Name = "txtMedida";
            this.txtMedida.Size = new System.Drawing.Size(142, 24);
            this.txtMedida.TabIndex = 3;
            this.txtMedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAgregarNuevo
            // 
            this.btnAgregarNuevo.Location = new System.Drawing.Point(250, 177);
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
            this.lblMedida.Location = new System.Drawing.Point(14, 104);
            this.lblMedida.Name = "lblMedida";
            this.lblMedida.Size = new System.Drawing.Size(60, 18);
            this.lblMedida.TabIndex = 9;
            this.lblMedida.Text = "Medida:";
            // 
            // txtCantidadPaquetes
            // 
            this.txtCantidadPaquetes.Location = new System.Drawing.Point(250, 70);
            this.txtCantidadPaquetes.Name = "txtCantidadPaquetes";
            this.txtCantidadPaquetes.Size = new System.Drawing.Size(142, 24);
            this.txtCantidadPaquetes.TabIndex = 2;
            this.txtCantidadPaquetes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCantidadPaquetes
            // 
            this.lblCantidadPaquetes.AutoSize = true;
            this.lblCantidadPaquetes.Location = new System.Drawing.Point(14, 70);
            this.lblCantidadPaquetes.Name = "lblCantidadPaquetes";
            this.lblCantidadPaquetes.Size = new System.Drawing.Size(136, 18);
            this.lblCantidadPaquetes.TabIndex = 2;
            this.lblCantidadPaquetes.Text = "Cantidad Paquetes:";
            // 
            // frmAgregarNuevaMaderaDura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 230);
            this.Controls.Add(this.grpCargadeDatos);
            this.Name = "frmAgregarNuevaMaderaDura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Nueva Madera Dura";
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
        private System.Windows.Forms.TextBox txtEspecie;
        private System.Windows.Forms.Label lblEspecie;
    }
}