using BE;
using BLL;
using Entidades;
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

        public string IdUsuario { get; private set; }

        public string Rol { get; private set; }

        public bool SesionActiva { get; private set; }

        private readonly UsuariosService90DI _usuariosBLL = new UsuariosService90DI();


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

                Usuario90DI? user = _usuariosBLL.Login(userName, password);

                if (user == null)
                    throw new InvalidOperationException("El usuario no existe.");


                this.UserName = user.NombreUsuario_90DI;
                this.IdUsuario = user.IdUsuario_90DI.ToString();
                this.Rol = user.IdPerfil_90DI.ToString();
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
