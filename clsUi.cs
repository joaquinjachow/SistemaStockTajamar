using System.Drawing;
using System.Windows.Forms;

namespace ControlStock
{
    internal static class clsUi
    {
        private static readonly Color Fondo = Color.FromArgb(245, 247, 250);
        private static readonly Color Panel = Color.White;
        private static readonly Color Borde = Color.FromArgb(218, 225, 232);
        private static readonly Font Fuente = new Font("Segoe UI", 9F);
        private static readonly Font FuenteTitulo = new Font("Segoe UI", 9F, FontStyle.Bold);

        public static void Aplicar(Form form)
        {
            form.Font = Fuente;
            form.BackColor = Fondo;
            form.StartPosition = FormStartPosition.CenterScreen;
            Recorrer(form.Controls);
        }

        public static void AplicarMenu(MenuStrip menu)
        {
            menu.Font = Fuente;
            menu.BackColor = SystemColors.Control;
            menu.ForeColor = SystemColors.ControlText;
            menu.Padding = new Padding(6, 2, 6, 2);
            menu.RenderMode = ToolStripRenderMode.Professional;
            menu.Renderer = new ToolStripProfessionalRenderer(new ColoresMenu());
        }

        private static void Recorrer(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                Estilizar(control);

                if (control.HasChildren)
                {
                    Recorrer(control.Controls);
                }
            }
        }

        private static void Estilizar(Control control)
        {
            if (control is GroupBox group)
            {
                group.Font = FuenteTitulo;
                group.BackColor = Panel;
                group.ForeColor = Color.FromArgb(35, 45, 55);
                group.Padding = new Padding(10);
                return;
            }

            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = SystemColors.ControlDark;
                button.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
                button.FlatAppearance.MouseDownBackColor = SystemColors.ControlDark;
                button.BackColor = SystemColors.Control;
                button.ForeColor = SystemColors.ControlText;
                button.Font = FuenteTitulo;
                button.MinimumSize = new Size(95, 30);
                return;
            }

            if (control is DataGridView grid)
            {
                grid.BackgroundColor = Panel;
                grid.BorderStyle = BorderStyle.FixedSingle;
                grid.GridColor = Borde;
                grid.EnableHeadersVisualStyles = false;
                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235);
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                grid.ColumnHeadersDefaultCellStyle.Font = FuenteTitulo;
                grid.DefaultCellStyle.Font = Fuente;
                grid.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
                grid.DefaultCellStyle.SelectionForeColor = Color.Black;
                grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 253);
                grid.RowHeadersVisible = false;
                return;
            }

            if (control is TextBox || control is ComboBox || control is DateTimePicker)
            {
                control.Font = Fuente;
                control.BackColor = Color.White;
                if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.Fixed3D;
                }

                if (control is ComboBox comboBox)
                {
                    comboBox.FlatStyle = FlatStyle.Standard;
                }
                return;
            }
            if (control is Label)
            {
                control.Font = Fuente;
                control.ForeColor = Color.FromArgb(45, 55, 65);
            }
        }

        private sealed class ColoresMenu : ProfessionalColorTable
        {
            private static readonly Color Hover = Color.FromArgb(225, 225, 225);
            private static readonly Color BordeHover = Color.FromArgb(170, 170, 170);
            public override Color MenuItemSelected
            {
                get { return Hover; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Hover; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Hover; }
            }
            public override Color MenuItemBorder
            {
                get { return BordeHover; }
            }
            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(210, 210, 210); }
            }
            public override Color MenuItemPressedGradientMiddle
            {
                get { return Color.FromArgb(210, 210, 210); }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(210, 210, 210); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return SystemColors.Control; }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return SystemColors.Control; }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return SystemColors.Control; }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return SystemColors.Control; }
            }
        }
    }
}