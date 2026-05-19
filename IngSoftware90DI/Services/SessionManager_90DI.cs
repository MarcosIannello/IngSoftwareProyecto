using DAL;
using Entities_90DI;

namespace Service_90DI
{
    public sealed class SessionManager_90DI
    {
        private static SessionManager_90DI? _instancia = null;

        private static readonly object _lock = new();

        public string UserName { get; private set; } = string.Empty;

        public string Password { get; private set; } = string.Empty;

        public string IdUsuario { get; private set; } = string.Empty;

        public string Rol { get; private set; } = string.Empty;

        public bool SesionActiva { get; private set; }

        private UserDAL_90DI _usuariosBLL = new UserDAL_90DI();

        private SessionManager_90DI() { }

        public static SessionManager_90DI Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new SessionManager_90DI();
                        }
                    }
                }
                return _instancia;
            }
        }

        public bool Login_90DI(User_90DI? user = null)
        {
           
                if (SesionActiva)
                    throw new InvalidOperationException("Ya hay una sesion activa. Cerrala antes de iniciar otra.");

                if (user == null)
                    throw new InvalidOperationException("El usuario no existe , o sus credenciales son incorrectas");

                if (user.Bloqueo_90DI == true)
                    throw new InvalidOperationException("El usuario esta bloqueado, Comuniquese con el Admin.");

                if (user.Activo_90DI == false)
                    throw new InvalidOperationException("El usuario no esta activo, Comuniquese con el Admin.");

                this.UserName = user.NombreUsuario_90DI;
                this.IdUsuario = user.IdUsuario_90DI.ToString();
                this.Rol = user.Rol_90DI.ToString();
                this.SesionActiva = true;

                return true;
        }

        public void CerrarSesion()
        {
            _instancia = null;
        }
    }
}
