using BLL_90DI;

namespace UI_90DI
{
    // Helper de navegación de sesión a nivel UI. Centraliza el logout para que
    // pueda invocarse desde cualquier form, no solo desde el menú.
    public static class SesionUI_90DI
    {
        // True mientras se hace logout. El FrmMenu lo consulta en su FormClosed
        // para NO disparar Application.Exit() cuando lo cerramos a propósito.
        public static bool CerrandoPorLogout { get; private set; }

        // Cierra la sesión y TODO lo que esté abierto, dejando SOLO el login.
        // No cierra la aplicación: reutiliza el login original (el main form de
        // Application.Run, que quedó oculto al loguearse) en vez de crear uno nuevo.
        // Si cerráramos ese main form, WinForms terminaría el message loop y la app.
        public static void Logout_90DI(UsersBLL_90DI userBll)
        {
            // 1) Cerrar la sesión (bitácora best-effort + limpia el Singleton).
            userBll.Logout_90DI();

            // 2) Recuperar el login original (sigue vivo, oculto) y el menú.
            var login = Application.OpenForms.OfType<FrmLogin_90DI>().FirstOrDefault();
            var menu  = Application.OpenForms.OfType<FrmMenu_90DI>().FirstOrDefault();

            // Fallback defensivo: si no estuviera el login original, creamos uno.
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

            // 4) Volver a mostrar el login limpio.
            login.ResetCampos_90DI();
            login.Show();
            login.BringToFront();
        }
    }
}
