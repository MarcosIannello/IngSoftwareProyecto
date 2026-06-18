using BLL_90DI;
using Service_90DI;
using Services_90DI;
using Services_90DI.entities;

namespace Capital_
{
    public partial class FrmAdminIntegridad_90DI : Form, IObserver_90DI
    {
        private readonly List<ResultadoIntegridad_90DI> _resultados;
        private readonly IntegridadBLL_90DI _integridad = new IntegridadBLL_90DI();

        public FrmAdminIntegridad_90DI(List<ResultadoIntegridad_90DI> resultados)
        {
            InitializeComponent();
            _resultados = resultados;

            // Suscribirse a los cambios de idioma y aplicar el idioma actual.
            LanguageManager_90DI.AddObserver_90DI(this);
            AplicarTraducciones();
        }

        private void FrmAdminIntegridad_90DI_Load(object sender, EventArgs e)
        {
            MostrarResultados(_resultados);
        }

        // Aplica los textos estáticos (título y botones) en el idioma activo.
        private void AplicarTraducciones()
        {
            Text                          = LanguageManager_90DI.T("integridad_form_titulo");
            btnForzarIntegridadAdmin.Text = LanguageManager_90DI.T("integridad_btn_reparar");
            btnBackup.Text                = LanguageManager_90DI.T("integridad_btn_backup");
            btnSalir.Text                 = LanguageManager_90DI.T("integridad_btn_cancelar");
        }

        // Observer: al cambiar el idioma reaplica textos y vuelve a renderizar el detalle.
        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            AplicarTraducciones();
            MostrarResultados(_resultados);
        }

        // ─── Mostrar SOLO la data corrupta en richTextBox1 ─────────────────────
        private void MostrarResultados(List<ResultadoIntegridad_90DI> resultados)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Consolas", 9.5f);

            // Solo lo que NO está íntegro: corruptos + errores
            var problemas = resultados.Where(r => !r.EsIntegro).ToList();

            // Sin problemas → sistema íntegro → no mostrar nada
            if (!problemas.Any())
                return;

            richTextBox1.SelectionColor = Color.OrangeRed;
            richTextBox1.AppendText(string.Format(LanguageManager_90DI.T("integridad_msg_detectados"), problemas.Count) + "\n");
            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.AppendText(new string('═', 65) + "\n\n");

            // Una entrada por tabla corrupta (no se listan las OK)
            foreach (var r in problemas)
            {
                if (r.EsError)
                {
                    richTextBox1.SelectionColor = Color.Magenta;
                    richTextBox1.AppendText($"  ?  {r.NombreTabla_90DI,-30} {LanguageManager_90DI.T("integridad_lbl_error")}\n");
                    continue;
                }

                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.AppendText($"  ✘  {r.NombreTabla_90DI,-30} {LanguageManager_90DI.T("integridad_lbl_corrupto")}");

                if (r.FilaAfectada_90DI != null)
                {
                    richTextBox1.SelectionColor = Color.Orange;
                    richTextBox1.AppendText($"\n       ↳ {LanguageManager_90DI.T("integridad_lbl_fila")}  : {r.FilaAfectada_90DI}");
                }
                if (r.ColumnaAfectada_90DI != null)
                {
                    richTextBox1.SelectionColor = Color.Orange;
                    richTextBox1.AppendText($"\n       ↳ {LanguageManager_90DI.T("integridad_lbl_celda")} : {r.ColumnaAfectada_90DI}");
                }
                richTextBox1.AppendText("\n");
            }

            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.AppendText("\n" + new string('─', 65) + "\n");
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.AppendText(LanguageManager_90DI.T("integridad_msg_acciones"));
        }

        // ─── Recuperar último backup (NO implementado todavía) ─────────────────
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                LanguageManager_90DI.T("integridad_msg_backup_confirm"),
                LanguageManager_90DI.T("integridad_msg_backup_title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            // TODO: implementar restauración real desde backup + recálculo + re-verificación.
            MessageBox.Show(
                LanguageManager_90DI.T("integridad_msg_backup_no_impl"),
                LanguageManager_90DI.T("integridad_msg_backup_no_impl_title"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ─── Forzar recálculo total ────────────────────────────────────────────
        // Si el sistema queda íntegro → cierra con OK y FrmLogin abre el menú.
        // Si siguen habiendo corruptos → actualiza la vista y permanece abierto.
        private void BtnForzarIntegridadAdmin_Click(object sender, EventArgs e)
        {
            btnForzarIntegridadAdmin.Enabled = false;
            btnForzarIntegridadAdmin.Text = LanguageManager_90DI.T("integridad_btn_recalculando");

            try
            {
                bool ok = _integridad.RecalcularTodo_90DI();

                if (!ok)
                {
                    MessageBox.Show(
                        LanguageManager_90DI.T("integridad_msg_recalc_error"),
                        LanguageManager_90DI.T("integridad_msg_error_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var nuevos         = _integridad.Verificar_90DI();
                var nuevosCorruptos = nuevos.Where(r => r.EsCorrupto).ToList();
                MostrarResultados(nuevos);

                if (!nuevosCorruptos.Any())
                {
                    // Sistema limpio → continuar al menú
                    MessageBox.Show(
                        LanguageManager_90DI.T("integridad_msg_recalc_ok"),
                        LanguageManager_90DI.T("integridad_msg_recalc_ok_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T("integridad_msg_recalc_pendiente"), nuevosCorruptos.Count),
                        LanguageManager_90DI.T("integridad_msg_advertencia_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(LanguageManager_90DI.T("integridad_msg_error_inesperado"), ex.Message),
                    LanguageManager_90DI.T("integridad_msg_error_title"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnForzarIntegridadAdmin.Enabled = true;
                btnForzarIntegridadAdmin.Text = LanguageManager_90DI.T("integridad_btn_reparar");
            }
        }

        // ─── Salir / cerrar sesión (siempre vuelve al login) ──────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            SessionManager_90DI.Instancia.CerrarSesion();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }
    }
}
