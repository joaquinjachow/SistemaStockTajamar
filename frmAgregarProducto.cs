using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    public class frmAgregarProducto : Form
    {
        private ComboBox cmbRubro;
        private ComboBox cmbSede;
        private ComboBox cmbUnidad;
        private TextBox txtMedida;
        private TextBox txtSecado;
        private TextBox txtEspecie;
        private TextBox txtCalidad;
        private TextBox txtEspesor;
        private TextBox txtCantidadPorUnidad;
        private TextBox txtCantidadInicial;
        private Label lblMedida;
        private Label lblSecado;
        private Label lblEspecie;
        private Label lblCalidad;
        private Label lblEspesor;
        private Label lblCantidadPorUnidad;
        private Label lblCantidadInicial;
        private Label lblUnidad;

        public frmAgregarProducto()
        {
            InitializeComponent();
            clsUi.Aplicar(this);
        }

        private void InitializeComponent()
        {
            Text = "Agregar Producto";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(560, 430);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            GroupBox grupo = new GroupBox();
            grupo.Text = "Datos del producto";
            grupo.Location = new Point(12, 12);
            grupo.Size = new Size(536, 360);
            Controls.Add(grupo);

            cmbRubro = CrearCombo(grupo, "Rubro:", 24, true, out _);
            cmbRubro.SelectedIndexChanged += (sender, args) => AplicarConfiguracionRubro();
            cmbRubro.TextChanged += (sender, args) => AplicarConfiguracionRubro();
            txtMedida = CrearTexto(grupo, "Medida:", 62, out lblMedida);
            txtSecado = CrearTexto(grupo, "Secado:", 100, out lblSecado);
            txtEspecie = CrearTexto(grupo, "Especie:", 138, out lblEspecie);
            txtCalidad = CrearTexto(grupo, "Calidad:", 176, out lblCalidad);
            txtEspesor = CrearTexto(grupo, "Espesor:", 214, out lblEspesor);
            txtCantidadPorUnidad = CrearTexto(grupo, "Cantidad por paquete:", 252, out lblCantidadPorUnidad);
            cmbUnidad = CrearCombo(grupo, "Unidad:", 290, true, out lblUnidad);
            txtCantidadInicial = CrearTexto(grupo, "Cantidad inicial:", 328, out lblCantidadInicial);

            cmbSede = new ComboBox();
            Label lblSede = new Label();
            lblSede.AutoSize = true;
            lblSede.Location = new Point(22, 390);
            lblSede.Text = "Sede inicial:";
            cmbSede.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSede.Location = new Point(130, 386);
            cmbSede.Size = new Size(160, 21);
            Controls.Add(lblSede);
            Controls.Add(cmbSede);

            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar";
            btnAgregar.Location = new Point(350, 382);
            btnAgregar.Size = new Size(90, 32);
            btnAgregar.Click += btnAgregar_Click;
            Controls.Add(btnAgregar);

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(452, 382);
            btnCerrar.Size = new Size(90, 32);
            btnCerrar.Click += (sender, args) => Close();
            Controls.Add(btnCerrar);

            Load += frmAgregarProducto_Load;
        }

        private ComboBox CrearCombo(Control contenedor, string texto, int y, bool editable, out Label label)
        {
            label = CrearLabel(texto, y);
            ComboBox combo = new ComboBox();
            combo.DropDownStyle = editable ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
            combo.Location = new Point(180, y - 3);
            combo.Size = new Size(320, 21);
            contenedor.Controls.Add(label);
            contenedor.Controls.Add(combo);
            return combo;
        }

        private TextBox CrearTexto(Control contenedor, string texto, int y, out Label label)
        {
            label = CrearLabel(texto, y);
            TextBox textBox = new TextBox();
            textBox.Location = new Point(180, y - 3);
            textBox.Size = new Size(320, 20);
            contenedor.Controls.Add(label);
            contenedor.Controls.Add(textBox);
            return textBox;
        }

        private Label CrearLabel(string texto, int y)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Location = new Point(18, y);
            label.Text = texto;
            return label;
        }

        private void frmAgregarProducto_Load(object sender, EventArgs e)
        {
            CargarRubros();
            cmbUnidad.Items.AddRange(new object[] { "Paquetes", "Tablas", "Hojas", "Unidades" });
            cmbUnidad.Text = "Unidades";
            cmbSede.DataSource = clsStockRepository.ObtenerSedes();
            cmbSede.DisplayMember = "Nombre";
            cmbSede.SelectedIndex = cmbSede.FindStringExact(clsDatabase.SedeCordoba);
            txtCantidadPorUnidad.Text = "1";
            txtCantidadInicial.Text = "0";
            AplicarConfiguracionRubro();
        }

        private void CargarRubros()
        {
            HashSet<string> rubros = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            cmbRubro.Items.Clear();
            foreach (string rubro in new[] { "Pino", "MaderaDura", "Machimbre", "Fenolicos" })
            {
                rubros.Add(rubro);
                cmbRubro.Items.Add(rubro);
            }
            DataTable rubrosDb = clsStockRepository.ObtenerRubros();
            foreach (DataRow row in rubrosDb.Rows)
            {
                string rubro = Convert.ToString(row["Rubro"]);
                if (!string.IsNullOrWhiteSpace(rubro) && rubros.Add(rubro))
                {
                    cmbRubro.Items.Add(rubro);
                }
            }
            if (cmbRubro.Items.Count > 0)
            {
                cmbRubro.SelectedIndex = 0;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CamposObligatoriosValidos())
                {
                    return;
                }

                int cantidadPorUnidad = 1;
                if (txtCantidadPorUnidad.Visible && (!int.TryParse(txtCantidadPorUnidad.Text, out cantidadPorUnidad) || cantidadPorUnidad <= 0))
                {
                    MessageBox.Show("Ingrese una cantidad por unidad valida.");
                    txtCantidadPorUnidad.Focus();
                    return;
                }
                if (!int.TryParse(txtCantidadInicial.Text, out int cantidadInicial) || cantidadInicial < 0)
                {
                    MessageBox.Show("Ingrese una cantidad inicial valida.");
                    txtCantidadInicial.Focus();
                    return;
                }

                clsStockRepository.AgregarProducto(
                    cmbRubro.Text,
                    ValorVisible(txtMedida),
                    ValorVisible(txtSecado),
                    ValorVisible(txtEspecie),
                    ValorVisible(txtCalidad),
                    ValorVisible(txtEspesor),
                    cantidadPorUnidad,
                    cmbUnidad.Text,
                    cantidadInicial,
                    cmbSede.Text);

                MessageBox.Show("Producto agregado correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtMedida.Clear();
            txtSecado.Clear();
            txtEspecie.Clear();
            txtCalidad.Clear();
            txtEspesor.Clear();
            txtCantidadPorUnidad.Text = "1";
            txtCantidadInicial.Text = "0";
            txtMedida.Focus();
        }

        private void AplicarConfiguracionRubro()
        {
            string rubro = RubroNormalizado();
            bool esPino = rubro == "pino";
            bool esMaderaDura = rubro == "maderadura";
            bool esMachimbre = rubro == "machimbre";
            bool esFenolicos = rubro == "fenolicos";
            bool rubroConocido = esPino || esMaderaDura || esMachimbre || esFenolicos;

            MostrarCampo(lblMedida, txtMedida, esPino || esMaderaDura || esMachimbre || !rubroConocido);
            MostrarCampo(lblSecado, txtSecado, esPino || !rubroConocido);
            MostrarCampo(lblEspecie, txtEspecie, esMaderaDura || !rubroConocido);
            MostrarCampo(lblCalidad, txtCalidad, esMachimbre || esFenolicos || !rubroConocido);
            MostrarCampo(lblEspesor, txtEspesor, esFenolicos || !rubroConocido);

            bool usaCantidadPorPaquete = esPino || esMaderaDura || !rubroConocido;
            MostrarCampo(lblCantidadPorUnidad, txtCantidadPorUnidad, usaCantidadPorPaquete);
            lblCantidadPorUnidad.Text = "Cantidad por paquete:";

            if (esPino || esMaderaDura)
            {
                cmbUnidad.Text = "Paquetes";
                cmbUnidad.Enabled = false;
                lblCantidadInicial.Text = "Paquetes iniciales:";
            }
            else if (esMachimbre)
            {
                cmbUnidad.Text = "Tablas";
                cmbUnidad.Enabled = false;
                txtCantidadPorUnidad.Text = "1";
                lblCantidadInicial.Text = "Tablas iniciales:";
            }
            else if (esFenolicos)
            {
                cmbUnidad.Text = "Hojas";
                cmbUnidad.Enabled = false;
                txtCantidadPorUnidad.Text = "1";
                lblCantidadInicial.Text = "Hojas iniciales:";
            }
            else
            {
                cmbUnidad.Enabled = true;
                lblCantidadInicial.Text = "Cantidad inicial:";
            }
            ReacomodarCampos();
        }

        private string RubroNormalizado()
        {
            if (cmbRubro == null || cmbRubro.Text == null)
            {
                return string.Empty;
            }
            return cmbRubro.Text.Trim().ToLowerInvariant().Replace(" ", "").Replace("\u00f3", "o");
        }

        private void MostrarCampo(Label label, Control control, bool visible)
        {
            label.Visible = visible;
            control.Visible = visible;
            if (!visible)
            {
                control.Text = string.Empty;
            }
        }

        private void ReacomodarCampos()
        {
            int y = 62;
            UbicarCampo(lblMedida, txtMedida, ref y);
            UbicarCampo(lblSecado, txtSecado, ref y);
            UbicarCampo(lblEspecie, txtEspecie, ref y);
            UbicarCampo(lblCalidad, txtCalidad, ref y);
            UbicarCampo(lblEspesor, txtEspesor, ref y);
            UbicarCampo(lblCantidadPorUnidad, txtCantidadPorUnidad, ref y);
            UbicarCampo(lblUnidad, cmbUnidad, ref y);
            UbicarCampo(lblCantidadInicial, txtCantidadInicial, ref y);
        }

        private void UbicarCampo(Label label, Control control, ref int y)
        {
            if (!label.Visible || !control.Visible)
            {
                return;
            }
            label.Location = new Point(18, y);
            control.Location = new Point(180, y - 3);
            y += 38;
        }

        private string ValorVisible(TextBox textBox)
        {
            return textBox.Visible ? textBox.Text : string.Empty;
        }

        private bool CamposObligatoriosValidos()
        {
            string rubro = RubroNormalizado();
            if (string.IsNullOrWhiteSpace(cmbRubro.Text))
            {
                MessageBox.Show("Seleccione o ingrese un rubro.");
                cmbRubro.Focus();
                return false;
            }
            if (rubro == "pino")
            {
                return Requerir(txtSecado, "Secado") && Requerir(txtMedida, "Medida");
            }
            if (rubro == "maderadura")
            {
                return Requerir(txtEspecie, "Especie") && Requerir(txtMedida, "Medida");
            }
            if (rubro == "machimbre")
            {
                return Requerir(txtCalidad, "Calidad") && Requerir(txtMedida, "Medida");
            }
            if (rubro == "fenolicos")
            {
                return Requerir(txtCalidad, "Calidad") && Requerir(txtEspesor, "Espesor");
            }
            return true;
        }

        private bool Requerir(TextBox textBox, string campo)
        {
            if (!textBox.Visible || !string.IsNullOrWhiteSpace(textBox.Text))
            {
                return true;
            }
            MessageBox.Show("Complete el campo " + campo + ".");
            textBox.Focus();
            return false;
        }
    }
}
