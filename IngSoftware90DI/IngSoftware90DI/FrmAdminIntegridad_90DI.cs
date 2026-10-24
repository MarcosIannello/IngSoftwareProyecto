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
        private readonly BackupBLL_90DI _backup = new BackupBLL_90DI();

        public FrmAdminIntegridad_90DI(List<ResultadoIntegridad_90DI> resultados)
        {
            InitializeComponent();
            _resultados = resultados;

            // Nos suscribimos al cambio de idioma y aplicamos el actual.
            LanguageManager_90DI.Current_90DI.AddObserver_90DI(this);
            AplicarTraducciones();
        }

        private void FrmAdminIntegridad_90DI_Load(object sender, EventArgs e)
        {
            MostrarResultados(_resultados);
        }

        // Textos fijos (título y botones) en el idioma actual.
        private void AplicarTraducciones()
        {
            Text                          = LanguageManager_90DI.T_90DI("integridad_form_titulo");
            btnForzarIntegridadAdmin.Text = LanguageManager_90DI.T_90DI("integridad_btn_reparar");
            btnBackup.Text                = LanguageManager_90DI.T_90DI("integridad_btn_backup");
            btnSalir.Text                 = LanguageManager_90DI.T_90DI("integridad_btn_cancelar");
        }

        // Cuando cambia el idioma se reaplican los textos y se vuelve a armar el detalle.
        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            AplicarTraducciones();
            MostrarResultados(_resultados);
        }

        // Detalle: solo mostramos lo que está corrupto
        private void MostrarResultados(List<ResultadoIntegridad_90DI> resultados)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Consolas", 9.5f);

            // Nos quedamos con lo que no está íntegro (corruptos y errores)
            var problemas = resultados.Where(r => !r.EsIntegro).ToList();

            // Si no hay nada, está todo bien y no hay que mostrar nada
            if (!problemas.Any())
                return;

            richTextBox1.SelectionColor = Color.OrangeRed;
            richTextBox1.AppendText(string.Format(LanguageManager_90DI.T_90DI("integridad_msg_detectados"), problemas.Count) + "\n");
            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.AppendText(new string('═', 65) + "\n\n");

            // Una línea por cada tabla con problemas
            foreach (var r in problemas)
            {
                if (r.EsError)
                {
                    richTextBox1.SelectionColor = Color.Magenta;
                    richTextBox1.AppendText($"  ?  {r.NombreTabla_90DI,-30} {LanguageManager_90DI.T_90DI("integridad_lbl_error")}\n");
                    continue;
                }

                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.AppendText($"  ✘  {r.NombreTabla_90DI,-30} {LanguageManager_90DI.T_90DI("integridad_lbl_corrupto")}");

                if (r.FilaAfectada_90DI != null)
                {
                    richTextBox1.SelectionColor = Color.Orange;
                    richTextBox1.AppendText($"\n       ↳ {LanguageManager_90DI.T_90DI("integridad_lbl_fila")}  : {r.FilaAfectada_90DI}");
                }
                if (r.ColumnaAfectada_90DI != null)
                {
                    richTextBox1.SelectionColor = Color.Orange;
                    richTextBox1.AppendText($"\n       ↳ {LanguageManager_90DI.T_90DI("integridad_lbl_celda")} : {r.ColumnaAfectada_90DI}");
                }
                richTextBox1.AppendText("\n");
            }

            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.AppendText("\n" + new string('─', 65) + "\n");
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.AppendText(LanguageManager_90DI.T_90DI("integridad_msg_acciones"));
        }

        // Restaura el último backup y vuelve a verificar. Si queda todo bien cierra con OK
        // y el login sigue al menú.
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                LanguageManager_90DI.T_90DI("integridad_msg_backup_confirm"),
                LanguageManager_90DI.T_90DI("integridad_msg_backup_title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            btnBackup.Enabled = false;
            var cursorAnterior = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                bool ok = _backup.RestaurarBackup_90DI();

                if (!ok)
                {
                    MessageBox.Show(
                        LanguageManager_90DI.T_90DI("integridad_msg_restore_error"),
                        LanguageManager_90DI.T_90DI("integridad_msg_error_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Verificamos de nuevo sobre la base ya restaurada.
                var nuevos          = _integridad.Verificar_90DI();
                var nuevosCorruptos = nuevos.Where(r => !r.EsIntegro).ToList();
                MostrarResultados(nuevos);

                if (!nuevosCorruptos.Any())
                {
                    MessageBox.Show(
                        LanguageManager_90DI.T_90DI("integridad_msg_restore_ok"),
                        LanguageManager_90DI.T_90DI("integridad_msg_recalc_ok_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T_90DI("integridad_msg_recalc_pendiente"), nuevosCorruptos.Count),
                        LanguageManager_90DI.T_90DI("integridad_msg_advertencia_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(LanguageManager_90DI.T_90DI("integridad_msg_error_inesperado"), ex.Message),
                    LanguageManager_90DI.T_90DI("integridad_msg_error_title"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = cursorAnterior;
                btnBackup.Enabled = true;
            }
        }

        // Forzar recálculo de todo. Si después queda íntegro cierra con OK y FrmLogin abre
        // el menú; si siguen habiendo corruptos, actualiza el detalle y se queda abierto.
        private void BtnForzarIntegridadAdmin_Click(object sender, EventArgs e)
        {
            btnForzarIntegridadAdmin.Enabled = false;
            btnForzarIntegridadAdmin.Text = LanguageManager_90DI.T_90DI("integridad_btn_recalculando");

            try
            {
                bool ok = _integridad.RecalcularTodo_90DI();

                if (!ok)
                {
                    MessageBox.Show(
                        LanguageManager_90DI.T_90DI("integridad_msg_recalc_error"),
                        LanguageManager_90DI.T_90DI("integridad_msg_error_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var nuevos         = _integridad.Verificar_90DI();
                var nuevosCorruptos = nuevos.Where(r => !r.EsIntegro).ToList();
                MostrarResultados(nuevos);

                if (!nuevosCorruptos.Any())
                {
                    // Quedó todo bien, seguimos al menú
                    MessageBox.Show(
                        LanguageManager_90DI.T_90DI("integridad_msg_recalc_ok"),
                        LanguageManager_90DI.T_90DI("integridad_msg_recalc_ok_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        string.Format(LanguageManager_90DI.T_90DI("integridad_msg_recalc_pendiente"), nuevosCorruptos.Count),
                        LanguageManager_90DI.T_90DI("integridad_msg_advertencia_title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(LanguageManager_90DI.T_90DI("integridad_msg_error_inesperado"), ex.Message),
                    LanguageManager_90DI.T_90DI("integridad_msg_error_title"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnForzarIntegridadAdmin.Enabled = true;
                btnForzarIntegridadAdmin.Text = LanguageManager_90DI.T_90DI("integridad_btn_reparar");
            }
        }

        // Salir: cierra la sesión y vuelve al login
        private void button2_Click(object sender, EventArgs e)
        {
            SessionManager_90DI.Instancia_90DI.CerrarSesion_90DI();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current_90DI.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }
    }
}
