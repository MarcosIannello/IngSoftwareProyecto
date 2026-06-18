using BLL_90DI;
using Service_90DI;
using Services_90DI.entities;

namespace Capital_
{
    public partial class FrmAdminIntegridad_90DI : Form
    {
        private readonly List<ResultadoIntegridad_90DI> _resultados;
        private readonly IntegridadBLL_90DI _integridad = new IntegridadBLL_90DI();

        public FrmAdminIntegridad_90DI(List<ResultadoIntegridad_90DI> resultados)
        {
            InitializeComponent();
            _resultados = resultados;
        }

        private void FrmAdminIntegridad_90DI_Load(object sender, EventArgs e)
        {
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

            int totalCorruptos = problemas.Count(r => r.EsCorrupto);

            richTextBox1.SelectionColor = Color.OrangeRed;
            richTextBox1.AppendText($"⚠  Se detectaron {problemas.Count} tabla(s) con problemas de integridad:\n");
            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.AppendText(new string('═', 65) + "\n\n");

            // Una entrada por tabla corrupta (no se listan las OK)
            foreach (var r in problemas)
            {
                if (r.EsError)
                {
                    richTextBox1.SelectionColor = Color.Magenta;
                    richTextBox1.AppendText($"  ?  {r.NombreTabla_90DI,-30} ERROR (espejo o tabla faltante)\n");
                    continue;
                }

                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.AppendText($"  ✘  {r.NombreTabla_90DI,-30} CORRUPTO");

                if (r.FilaAfectada_90DI != null)
                {
                    richTextBox1.SelectionColor = Color.Orange;
                    richTextBox1.AppendText($"\n       ↳ FILA  : {r.FilaAfectada_90DI}");
                }
                if (r.ColumnaAfectada_90DI != null)
                {
                    richTextBox1.SelectionColor = Color.Orange;
                    richTextBox1.AppendText($"\n       ↳ CELDA : {r.ColumnaAfectada_90DI}");
                }
                richTextBox1.AppendText("\n");
            }

            richTextBox1.SelectionColor = Color.Silver;
            richTextBox1.AppendText("\n" + new string('─', 65) + "\n");
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.AppendText("Presioná \"Reparar Integridad\" para recalcular, \"Recuperar Backup\" para restaurar, o \"Cancelar\" para salir.");
        }

        // ─── Recuperar último backup (NO implementado todavía) ─────────────────
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                "¿Desea restaurar la base de datos desde el último backup disponible?",
                "Recuperar Backup",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            // TODO: implementar restauración real desde backup + recálculo + re-verificación.
            MessageBox.Show(
                "La recuperación desde backup todavía no está implementada.",
                "Función no disponible",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ─── Forzar recálculo total ────────────────────────────────────────────
        // Si el sistema queda íntegro → cierra con OK y FrmLogin abre el menú.
        // Si siguen habiendo corruptos → actualiza la vista y permanece abierto.
        private void BtnForzarIntegridadAdmin_Click(object sender, EventArgs e)
        {
            btnForzarIntegridadAdmin.Enabled = false;
            btnForzarIntegridadAdmin.Text = "Recalculando...";

            try
            {
                bool ok = _integridad.RecalcularTodo_90DI();

                if (!ok)
                {
                    MessageBox.Show(
                        "Ocurrió un error al recalcular la integridad. Revisá los logs.",
                        "Error",
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
                        "Integridad recalculada correctamente. El sistema está íntegro.",
                        "Integridad OK",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        $"Se recalculó pero aún quedan {nuevosCorruptos.Count} problema(s).\nRevisá los detalles.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnForzarIntegridadAdmin.Enabled = true;
                btnForzarIntegridadAdmin.Text = "Reparar Integridad";
            }
        }

        // ─── Salir / cerrar sesión (siempre vuelve al login) ──────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            SessionManager_90DI.Instancia.CerrarSesion();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
