using BLL_90DI;
using Service_90DI;
using Services_90DI;
using Services_90DI.entities;

namespace UI_90DI
{
    public partial class FrmCambiarIdioma_90DI : Form, IObserver_90DI
    {
        private readonly UsersBLL_90DI _usuarioService = new UsersBLL_90DI();

        public FrmCambiarIdioma_90DI()
        {
            // Layout estatico definido en el Designer (vista previa).
            InitializeComponent();

            // Aplicar traducciones e items dinamicamente en runtime.
            AplicarTraducciones();
            CargarIdiomas();

            LanguageManager_90DI.AddObserver_90DI(this);
            var t = LanguageManager_90DI.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        private void AplicarTraducciones()
        {
            Text                = LanguageManager_90DI.T("idioma_form_titulo");
            lblSeleccionar.Text = LanguageManager_90DI.T("idioma_lbl_seleccionar");
            btnAplicar.Text     = LanguageManager_90DI.T("idioma_btn_aplicar");
            btnCancelar.Text    = LanguageManager_90DI.T("idioma_btn_cancelar");
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
            {
                // Notifica a los observers y actualiza el idioma de la sesión.
                LanguageManager_90DI.NotifyAllObservers_90DI(idioma);

                // Persiste el idioma elegido en la BD para el usuario logueado.
                var session = SessionManager_90DI.Instancia;
                if (session.SesionActiva && session.userActual.IdUsuario_90DI > 0)
                {
                    _usuarioService.UpdateIdioma_90DI(session.userActual.IdUsuario_90DI, idioma.CodigoIdioma_90DI);
                    session.userActual.Idioma_90DI = idioma.CodigoIdioma_90DI;
                }
            }
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
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
