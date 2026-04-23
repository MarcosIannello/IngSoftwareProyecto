using System;
using System.Collections.Generic;
using System.Text;

namespace Services.SessionManager
{
    public sealed class SessionManager200MI
    {
        private static SessionManager200MI? _instancia = null;

        private static readonly object _lock = new();

        public string UserName { get; private set; } = string.Empty;

        public string IdUsuario { get; private set; }

        public string Rol { get; private set; }

        public bool SesionActiva { get; private set; }


        private SessionManager200MI() { }

        public static SessionManager200MI Instancia
        {
            get
            {
                if(_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new SessionManager200MI();
                        }
                    }
                }
                return _instancia;
            }
        }

        public void IniciarSesion(string userName, string idUsuario, string rol)
        {
            if (SesionActiva)
                throw new InvalidOperationException("Ya hay una sesión activa. Cerrala antes de iniciar otra.");

            UserName = userName;
            IdUsuario = idUsuario;
            Rol = rol;
            SesionActiva = true;
        }

        public void CerrarSesion()
        {
            _instancia = null;
        }

    }
}
