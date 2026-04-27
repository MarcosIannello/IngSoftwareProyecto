using BE;
using DAL;
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

        private readonly UsuariosDAL200MI _usuariosDal = new UsuariosDAL200MI();


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

        public bool IniciarSesion(string userName="")
        {
            try
            {
                if (SesionActiva)
                    throw new InvalidOperationException("Ya hay una sesión activa. Cerrala antes de iniciar otra.");

                Usuario200MI? user = _usuariosDal.GetUser200MI(userName);

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
