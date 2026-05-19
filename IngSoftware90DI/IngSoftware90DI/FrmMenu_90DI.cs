using Services_90DI;
using System.Reflection;
using BLL_90DI;
using Entities_90DI;
using Service_90DI;
using Capital_;

namespace UI_90DI
{
    public partial class FrmMenu_90DI : Form
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();

        public FrmMenu_90DI()
        {
            InitializeComponent();
        }


        private void Menu200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SessionManager_90DI.Instancia.Login_90DI(null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var confirm = MessageBox.Show(
                    "¿Desea cerrar sesión?",
                    "Cerrar sesión",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                var userName = SessionManager_90DI.Instancia.UserName;

                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = userName,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = "Usuarios",
                    Evento_90DI     = "Logout",
                    Criticidad_90DI = 1
                });

                SessionManager_90DI.Instancia.CerrarSesion();
                var login = new FrmLogin_90DI();
                this.Hide();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmCambiarPassword_90DI();
            form.Show();
            this.Hide();
        }

        private void aBMUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var abmUsers = new FrmGestionUsuarios();
            abmUsers.Show();
            this.Hide();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Bitacora = new FrmBitacora_90DI();
            this.Hide();
            Bitacora.Show();
        }
    }
}
