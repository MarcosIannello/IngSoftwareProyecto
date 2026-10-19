using Services_90DI.entities;

namespace Service_90DI
{
    public sealed class SessionManager_90DI
    {
        private static SessionManager_90DI? _instancia_90DI = null;

        private static readonly object _lock_90DI = new();

        public User_90DI userActual_90DI = new User_90DI();

        public bool SesionActiva_90DI { get; private set; }

        // Nombres de las patentes efectivas del usuario logueado. Se cargan en el login
        // y se usan para mostrar u ocultar opciones del menú (sin distinguir mayúsculas).
        public HashSet<string> PatentesActivas_90DI { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        // true si el usuario de la sesión tiene esa patente.
        public bool TienePatente_90DI(string nombrePatente) =>
            PatentesActivas_90DI.Contains(nombrePatente);

        // Idioma de la sesión. Se elige en el login (antes de autenticar) y se mantiene
        // en el resto de las pantallas. Por defecto, español.
        public Idioma_90DI IdiomaActual_90DI { get; set; } = new Idioma_90DI
        {
            IdIdioma_90DI = 1,
            NombreIdioma_90DI = "Español",
            CodigoIdioma_90DI = "es"
        };

        private SessionManager_90DI() { }

        public static SessionManager_90DI Instancia_90DI
        {
            get
            {
                if (_instancia_90DI == null)
                {
                    lock (_lock_90DI)
                    {
                        if (_instancia_90DI == null)
                        {
                            _instancia_90DI = new SessionManager_90DI();
                        }
                    }
                }
                return _instancia_90DI;
            }
        }

        public bool Login_90DI(User_90DI? user = null)
        {

                if (SesionActiva_90DI)
                    throw new InvalidOperationException("Ya hay una sesion activa. Cerrala antes de iniciar otra.");

                if (user == null)
                    throw new InvalidOperationException("El usuario no existe , o sus credenciales son incorrectas");

                if (user.Bloqueo_90DI == true)
                    throw new InvalidOperationException("El usuario esta bloqueado, Comuniquese con el Admin.");

                if (user.Activo_90DI == false)
                    throw new InvalidOperationException("El usuario no esta activo, Comuniquese con el Admin.");

                this.userActual_90DI = user;
                this.SesionActiva_90DI = true;

                return true;
        }

        public void CerrarSesion_90DI()
        {
            _instancia_90DI = null;
        }
    }
}
