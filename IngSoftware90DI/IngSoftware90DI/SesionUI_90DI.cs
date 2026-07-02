using BLL_90DI;

namespace UI_90DI
{
    public static class SesionUI_90DI
    {
       
        public static bool CerrandoPorLogout { get; private set; }

        // Cierra la sesión y TODO lo que esté abierto, dejando SOLO el login.
      
        public static void Logout_90DI(UsersBLL_90DI userBll)
        {
            
            userBll.Logout_90DI();

           
            var login = Application.OpenForms.OfType<FrmLogin_90DI>().FirstOrDefault();
            var menu  = Application.OpenForms.OfType<FrmMenu_90DI>().FirstOrDefault();

            if (login == null)
            {
                login = new FrmLogin_90DI();
                login.Show();
            }

           
            CerrandoPorLogout = true;
            try
            {
                foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
                    if (f != login && f != menu)
                        f.Close();

                menu?.Close();
            }
            finally
            {
                CerrandoPorLogout = false;
            }

       
            login.ResetCampos_90DI();
            login.Show();
            login.BringToFront();
        }
    }
}
