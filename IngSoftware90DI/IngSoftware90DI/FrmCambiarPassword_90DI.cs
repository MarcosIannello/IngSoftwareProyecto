

using BLL_90DI;
using Service_90DI;
using Services_90DI;
using Services_90DI.entities;

namespace UI_90DI
{
    public partial class FrmCambiarPassword_90DI : Form, IObserver_90DI
    {

        private UsersBLL_90DI _usuarioService = new UsersBLL_90DI();
        private User_90DI usuario = new();

        public FrmCambiarPassword_90DI()
        {
            InitializeComponent();

            usuario = _usuarioService.getUserByUsername(SessionManager_90DI.Instancia.userActual.NombreUsuario_90DI);

            if (usuario != null)
            {
                cargarDatos(usuario);
            }
            else
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_error_actualizar"), LanguageManager_90DI.T("pass_msg_error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            LanguageManager_90DI.Current.AddObserver_90DI(this);
            var t = LanguageManager_90DI.Current.TraduccionesActuales_90DI;
            if (t.Count > 0) UpdateLanguage_90DI(t);
        }

        public void UpdateLanguage_90DI(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue("pass_lbl_nombre_usuario", out var v)) label1.Text   = v;
            if (traducciones.TryGetValue("pass_lbl_pass_actual",    out v))     label2.Text   = v;
            if (traducciones.TryGetValue("pass_lbl_nueva_pass",     out v))     label3.Text   = v;
            if (traducciones.TryGetValue("pass_lbl_confirmar",      out v))     label4.Text   = v;
            if (traducciones.TryGetValue("pass_btn_cambiar",        out v))     button1.Text  = v;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            LanguageManager_90DI.Current.Unsubscribe_90DI(this);
            base.OnFormClosed(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassActual.Text) ||
                string.IsNullOrWhiteSpace(txtNewPass.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmNewPass.Text))
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_campos_obligatorios"), LanguageManager_90DI.T("pass_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_usuarioService.VerifyPassword_90DI(txtPassActual.Text, usuario.Password_90DI))
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_pass_incorrecta"), LanguageManager_90DI.T("pass_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassActual.Focus();
                return;
            }

            if (txtNewPass.Text.Length < 8)
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_pass_corta"), LanguageManager_90DI.T("pass_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            if (txtNewPass.Text == txtPassActual.Text)
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_pass_igual"), LanguageManager_90DI.T("pass_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            if (txtNewPass.Text != txtConfirmNewPass.Text)
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_pass_no_coincide"), LanguageManager_90DI.T("pass_msg_val_title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmNewPass.Focus();
                return;
            }

            var resultado = _usuarioService.updatePassword_90DI(usuario.IdUsuario_90DI, txtNewPass.Text);

            if (resultado)
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_actualizada"), LanguageManager_90DI.T("pass_msg_exito_title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // vuelve al menú que quedó abierto detrás
            }
            else
            {
                MessageBox.Show(LanguageManager_90DI.T("pass_msg_error_actualizar"), LanguageManager_90DI.T("pass_msg_error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarDatos(User_90DI usuario)
        {
            textBox1.Text = usuario.NombreUsuario_90DI;
            textBox1.Enabled = false;
        }

        private void FrmCambiarPassword_90DI_FormClosed(object sender, FormClosedEventArgs e)
        {
            // El menú quedó abierto detrás (ShowDialog), no hay que volver a mostrarlo.
        }

        private void FrmCambiarPassword_90DI_Load(object sender, EventArgs e)
        {

        }
    }
}
