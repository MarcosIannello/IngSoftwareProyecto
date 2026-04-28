using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.SessionManager
{
    public sealed class SessionManager90DI
    {
        private static SessionManager90DI? _instancia = null;

        private static readonly object _lock = new();

        public string UserName { get; private set; } = string.Empty;

        public string Password { get; private set; }

        public string IdUsuario { get; private set; }

        public string Rol { get; private set; }

        public bool SesionActiva { get; private set; }

        private readonly UsuariosBll200MI _usuariosBLL = new UsuariosBll200MI();


        private SessionManager90DI() { }

        public static SessionManager90DI Instancia
        {
            get
            {
                if(_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new SessionManager90DI();
                        }
                    }
                }
                return _instancia;
            }
        }

        public bool IniciarSesion(string userName="", string password="")
        {
            try
            {
                if (SesionActiva)
                    throw new InvalidOperationException("Ya hay una sesión activa. Cerrala antes de iniciar otra.");

                Usuario200MI? user = _usuariosBLL.Login(userName, password);

                if (user == null)
                    throw new InvalidOperationException("El usuario no existe.");


                this.UserName = user.NombreUsuario_200MI;
                this.IdUsuario = user.IdUsuario_200MI.ToString();
                this.Rol = user.IdPerfil_200MI.ToString();
                this.SesionActiva = true;

                return true;

            }
            catch (Exception ex) {
                return false;

            }
        }

        public void CerrarSesion()
        {
            _instancia = null;
        }

    }
}
