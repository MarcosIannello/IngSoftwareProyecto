

using BLL_90DI;
using Entities_90DI;
using Service_90DI;

namespace UI_90DI
{
    public partial class FrmCambiarPassword_90DI : Form
    {

        private UsersBLL_90DI _usuarioService = new UsersBLL_90DI();
        private User_90DI usuario = new();
        Menu_90DI menu = new Menu_90DI();

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
            var nuevaPassword = textBox2.Text;

            if (nuevaPassword == "")
            {
                MessageBox.Show("La contraseña no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultado = _usuarioService.updatePassword_90DI(usuario.IdUsuario_90DI, nuevaPassword);

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
    }
}
