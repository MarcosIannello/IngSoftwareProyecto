using BLL_90DI;
using Services_90DI;

namespace UI_90DI
{
    // Diálogo de gestión de backup para el Admin. Ofrece dos acciones sobre el único
    // archivo de backup: generar uno nuevo (pisa al anterior) o restaurar el último.
    // Muestra la fecha/hora del último backup para que el Admin sepa a qué estado
    // volvería la base si decide restaurar.
    public partial class FrmBackup_90DI : Form, IObserver_90DI
    {
        private readonly BackupBLL_90DI _backup = new BackupBLL_90DI();

        // Formato mostrado al usuario para la fecha del último backup.
        private const string FormatoFecha = "dd/MM/yyyy HH:mm:ss";

        public FrmBackup_90DI()
        {
            InitializeComponent();

            AplicarTraducciones();
            RefrescarUltimoBackup();

            LanguageManager_90DI.Current.AddObserver_90DI(this);
            var t = LanguageManager_90DI.Current.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        private void AplicarTraducciones()
        {
            Text = LanguageManager_90DI.T("backup_form_titulo");
            lblUltimoTitulo.Text = LanguageManager_90DI.T("backup_lbl_ultimo");
            lblInfo.Text = LanguageManager_90DI.T("backup_lbl_info");
            btnGenerar.Text = LanguageManager_90DI.T("backup_btn_generar");
            btnAplicar.Text = LanguageManager_90DI.T("backup_btn_aplicar");
            btnCerrar.Text = LanguageManager_90DI.T("backup_btn_cerrar");
        }

        // Consulta la fecha del último backup y actualiza el label. Si no hay ninguno,
        // muestra el texto correspondiente y deshabilita la opción de restaurar.
        private void RefrescarUltimoBackup()
        {
            DateTime? fecha = _backup.ObtenerFechaUltimoBackup_90DI();

            if (fecha.HasValue)
            {
                lblUltimoFecha.Text = fecha.Value.ToString(FormatoFecha);
                btnAplicar.Enabled = true;
            }
            else
            {
                lblUltimoFecha.Text = LanguageManager_90DI.T("backup_lbl_sin_backup");
                btnAplicar.Enabled = false;
            }
        }

        // Opción 1: generar un nuevo backup del estado actual.
        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                LanguageManager_90DI.T("menu_backup_confirm"),
                LanguageManager_90DI.T("backup_form_titulo"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            EjecutarConEspera(() =>
            {
                bool ok = _backup.GenerarBackup_90DI();

                MessageBox.Show(
                    LanguageManager_90DI.T(ok ? "menu_backup_ok" : "menu_backup_error"),
                    LanguageManager_90DI.T("backup_form_titulo"),
                    MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (ok) RefrescarUltimoBackup();
            });
        }

        // Opción 2: restaurar la base desde el último backup. Se confirma incluyendo la
        // fecha del backup para dejar claro a qué estado volvería la base.
        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            DateTime? fecha = _backup.ObtenerFechaUltimoBackup_90DI();
            if (!fecha.HasValue)
            {
                MessageBox.Show(
                    LanguageManager_90DI.T("backup_restore_sin_backup"),
                    LanguageManager_90DI.T("backup_form_titulo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefrescarUltimoBackup();
                return;
            }

            var mensaje = string.Format(
                LanguageManager_90DI.T("backup_restore_confirm"),
                fecha.Value.ToString(FormatoFecha));

            var confirmar = MessageBox.Show(
                mensaje,
                LanguageManager_90DI.T("backup_form_titulo"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmar != DialogResult.Yes)
                return;

            EjecutarConEspera(() =>
            {
                bool ok = _backup.RestaurarBackup_90DI();

                MessageBox.Show(
                    LanguageManager_90DI.T(ok ? "backup_restore_ok" : "backup_restore_error"),
                    LanguageManager_90DI.T("backup_form_titulo"),
                    MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                RefrescarUltimoBackup();
            });
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Corre una acción (backup/restore) con cursor de espera y los botones deshabilitados
        // para evitar disparos en paralelo mientras SQL trabaja.
        private void EjecutarConEspera(Action accion)
        {
            btnGenerar.Enabled = false;
            btnAplicar.Enabled = false;
            var cursorAnterior = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager_90DI.T("backup_form_titulo"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = cursorAnterior;
                btnGenerar.Enabled = true;
                // btnAplicar lo re-habilita RefrescarUltimoBackup según haya backup o no.
                RefrescarUltimoBackup();
            }
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("backup_form_titulo", out var v)) Text = v;
            if (traducciones.TryGetValue("backup_lbl_ultimo", out v)) lblUltimoTitulo.Text = v;
            if (traducciones.TryGetValue("backup_lbl_info", out v)) lblInfo.Text = v;
            if (traducciones.TryGetValue("backup_btn_generar", out v)) btnGenerar.Text = v;
            if (traducciones.TryGetValue("backup_btn_aplicar", out v)) btnAplicar.Text = v;
            if (traducciones.TryGetValue("backup_btn_cerrar", out v)) btnCerrar.Text = v;

            // El label de fecha puede estar mostrando "sin backup" traducido: refrescar.
            RefrescarUltimoBackup();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }
    }
}
