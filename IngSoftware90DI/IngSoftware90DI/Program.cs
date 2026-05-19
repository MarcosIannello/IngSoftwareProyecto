using Services_90DI;

namespace UI_90DI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Clave AES-256 (32 bytes en Base64) para encriptación reversible del campo Email
            SecurityService_90DI.Initialize_90DI("K2bD5gH8jL1nP4rT7vY0zA3cF6iM9qS+uWxEeGhJoQr=");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmLogin_90DI());
        }
    }
}