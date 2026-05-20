

using BLL_90DI;
using Services_90DI;
using Service_90DI;

namespace UI_90DI
{
    public partial class FrmCambiarPassword_90DI : Form
    {

        private UsersBLL_90DI _usuarioService = new UsersBLL_90DI();
        private User_90DI usuario = new();
        FrmMenu_90DI menu = new FrmMenu_90DI();

        public FrmCambiarPassword_90DI()
        {
            InitializeComponent();

            usuario = _usuarioService.getUserByUsername(SessionManager_90DI.Instancia.UserName);


            if (usuario != null)
            {
                cargarDatos(usuario);
            }
            else
            {
                MessageBox.Show("Error al cargar el usuario. Por favor, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassActual.Text) ||
                string.IsNullOrWhiteSpace(txtNewPass.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmNewPass.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_usuarioService.VerifyPassword_90DI(txtPassActual.Text, usuario.Password_90DI))
            {
                MessageBox.Show("La contraseña actual es incorrecta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassActual.Focus();
                return;
            }

            if (txtNewPass.Text.Length < 8)
            {
                MessageBox.Show("La nueva contraseña debe tener al menos 8 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            if (txtNewPass.Text != txtConfirmNewPass.Text)
            {
                MessageBox.Show("La nueva contraseña y su confirmación no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmNewPass.Focus();
                return;
            }

            var resultado = _usuarioService.updatePassword_90DI(usuario.IdUsuario_90DI, txtNewPass.Text);

            if (resultado)
            {
                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña. Por favor, intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarDatos(User_90DI usuario)
        {
            textBox1.Text = usuario.NombreUsuario_90DI;
            textBox1.Enabled = false;
        }

        private void FrmCambiarPassword_200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }

        private void FrmCambiarPassword_90DI_Load(object sender, EventArgs e)
        {

        }
    }
}
