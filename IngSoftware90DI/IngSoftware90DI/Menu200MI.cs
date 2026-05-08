using Services_90DI;
using System.Reflection;
using BLL_90DI;

namespace UI_90DI
{
    public partial class Menu200MI : Form
    {
      
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
    }
}
