using Services_90DI;
using Services_90DI.entities;

namespace UI_90DI
{
    public class FrmCambiarIdioma_90DI : Form, IObserver_90DI
    {
        private Label lblSeleccionar;
        private ComboBox cmbIdioma;
        private Button btnAplicar;
        private Button btnCancelar;

        public FrmCambiarIdioma_90DI()
        {
            BuildUI();
            CargarIdiomas();

            LanguageManager_90DI.AddObserver_90DI(this);
            var t = LanguageManager_90DI.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        private void BuildUI()
        {
            Text = LanguageManager_90DI.T("idioma_form_titulo");
            ClientSize = new Size(320, 160);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            lblSeleccionar = new Label
            {
                Text = LanguageManager_90DI.T("idioma_lbl_seleccionar"),
                Location = new Point(20, 20),
                AutoSize = true
            };

            cmbIdioma = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(20, 50),
                Size = new Size(275, 30)
            };

            btnAplicar = new Button
            {
                Text = LanguageManager_90DI.T("idioma_btn_aplicar"),
                Location = new Point(20, 100),
                Size = new Size(130, 36),
                BackColor = Color.GreenYellow
            };
            btnAplicar.Click += BtnAplicar_Click;

            btnCancelar = new Button
            {
                Text = LanguageManager_90DI.T("idioma_btn_cancelar"),
                Location = new Point(165, 100),
                Size = new Size(130, 36),
                BackColor = Color.LightCoral
            };
            btnCancelar.Click += (_, _) => Close();

            Controls.AddRange(new Control[] { lblSeleccionar, cmbIdioma, btnAplicar, btnCancelar });
        }

        private void CargarIdiomas()
        {
            cmbIdioma.Items.Add(new Idioma_90DI { IdIdioma_90DI = 1, NombreIdioma_90DI = "Español", CodigoIdioma_90DI = "es" });
            cmbIdioma.Items.Add(new Idioma_90DI { IdIdioma_90DI = 2, NombreIdioma_90DI = "English",  CodigoIdioma_90DI = "en" });

            // Pre-seleccionar el idioma activo
            var codigoActual = Service_90DI.SessionManager_90DI.Instancia.IdiomaActual.CodigoIdioma_90DI;
            cmbIdioma.SelectedIndex = codigoActual == "en" ? 1 : 0;
        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            if (cmbIdioma.SelectedItem is Idioma_90DI idioma)
                LanguageManager_90DI.NotifyAllObservers_90DI(idioma);
            Close();
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("idioma_form_titulo",    out var v)) Text                = v;
            if (traducciones.TryGetValue("idioma_lbl_seleccionar",out v))     lblSeleccionar.Text = v;
            if (traducciones.TryGetValue("idioma_btn_aplicar",    out v))     btnAplicar.Text     = v;
            if (traducciones.TryGetValue("idioma_btn_cancelar",   out v))     btnCancelar.Text    = v;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }
    }
}
