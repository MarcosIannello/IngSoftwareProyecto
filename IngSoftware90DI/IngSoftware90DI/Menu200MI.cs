using Services_90DI;
using System.Reflection;
using BLL_90DI;
using Entities_90DI;
using Service_90DI;
using Capital_;

namespace UI_90DI
{
    public partial class Menu200MI : Form
    {
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();

        public Menu200MI()
        {
            InitializeComponent();
        }



        //public void AbrirForm(string? nombreForm)
        //{
        //    if (string.IsNullOrEmpty(nombreForm)) return;

        //    switch (nombreForm)
        //    {
        //        case "Login":
        //            var sesionActiva = SessionManager90DI.Instancia.IniciarSesion();
        //            if (!sesionActiva)
        //            {
        //                MessageBox.Show("Ya posee una sesion activa");
        //            }
        //            break;
        //        case "LogOut":
        //            SessionManager90DI.Instancia.CerrarSesion();
        //            var login = new Login_90DI();
        //            login.Show();
        //            this.Hide();
        //            break;
        //        default:
        //            //ompletar
        //            break;
        //    }
        //}


        private void Menu200MI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SessionManager90DI.Instancia.IniciarSesion();

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
                var userName = SessionManager90DI.Instancia.UserName;

                _bitacora.CreateLogEvent_90DI(new LogEvent_90DI
                {
                    Login_90DI      = userName,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = "Usuarios",
                    Evento_90DI     = "Logout",
                    Criticidad_90DI = 1
                });

                SessionManager90DI.Instancia.CerrarSesion();
                var login = new Login_90DI();
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
            var form = new FrmCambiarPassword_200MI();
            form.Show();
            this.Hide();
        }

        private void aBMUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var abmUsers = new CrudUsers();
            abmUsers.Show();
            this.Hide();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Bitacora = new Bitacora();
            this.Hide();
            Bitacora.Show();
        }
    }
}
