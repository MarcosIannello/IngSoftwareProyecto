

using BLL_90DI;
using Entities_90DI;
using Service_90DI;

namespace UI_90DI
{
    public partial class FrmCambiarPassword_200MI : Form
    {

        private UsersService90DI _usuarioService = new UsersService90DI();
        private User90DI usuario = new();
        Menu200MI menu = new Menu200MI();

        public FrmCambiarPassword_200MI()
        {
            InitializeComponent();

            usuario = _usuarioService.getUserByUsername(SessionManager90DI.Instancia.UserName);


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
            var nuevaPassword = textBox2.Text;

            if (nuevaPassword == "")
            {
                MessageBox.Show("La contraseña no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultado = _usuarioService.updatePassword(usuario.IdUsuario_90DI, nuevaPassword);

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

        private void cargarDatos(User90DI usuario)
        {
            textBox1.Text = usuario.NombreUsuario_90DI;
            textBox1.Enabled = false;
        }

        private void FrmCambiarPassword_200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }
    }
}
