using BLL_90DI;
using DAL;
using Service_90DI;
using Services_90DI;
using Services_90DI.entities;

namespace BLL_90DI
{
    public class UsersBLL_90DI
    {
        private readonly UserDAL_90DI _dal = new UserDAL_90DI();
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();

        private const int MAX_INTENTOS = 3;

        private int _intentos = 0;

        public UsersBLL_90DI() { }

        // Si el email ya está en texto plano (usuarios migrados)
        private static string TryDecryptEmail(string email)
        {
            try { return SecurityService_90DI.ReversibleDecrypt_90DI(email); }
            catch { return email; }
        }

        public User_90DI? getUserByUsername(string Username)
        {
            var user = _dal.GetUserByUsername_90DI(Username);
            if (user != null && !string.IsNullOrEmpty(user.Email_90DI))
                user.Email_90DI = TryDecryptEmail(user.Email_90DI);
            return user;
        }

        public User_90DI? Login_90DI(string username, string password)
        {
            var user = getUserByUsername(username);

            if(user != null && user.Bloqueo_90DI)
            {
                return new User_90DI { Bloqueo_90DI = true };
            }

            if (user != null)
            {
                if (SecurityService_90DI.Verify90DI(password, user.Password_90DI))
                {
                    //LogEvent login exitoso
                    _bitacora.CreateLogEvent_90DI(new Event_90DI
                    {
                        Login_90DI = user.NombreUsuario_90DI,
                        Fecha_90DI = DateTime.Now,
                        Hora_90DI = DateTime.Now.TimeOfDay,
                        Modulo_90DI = "Login",
                        Evento_90DI = "Inicio de sesion exitoso",
                        Criticidad_90DI = 1
                    });

                    return user;
                }
                else
                {
                    //ValidateBlock
                    _intentos++;

                    if (_intentos >= MAX_INTENTOS)
                    {
                        _dal.blockUser90DI(user.IdUsuario_90DI);
                        return new User_90DI { Bloqueo_90DI = true };
                    }
                    return null;
                }
            }

            return null;
        }

        public void Logout_90DI()
        {
            try
            {
                var username = SessionManager_90DI.Instancia.userActual.NombreUsuario_90DI;

                SessionManager_90DI.Instancia.CerrarSesion();

                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI = username,
                    Fecha_90DI = DateTime.Now,
                    Hora_90DI = DateTime.Now.TimeOfDay,
                    Modulo_90DI = "Login",
                    Evento_90DI = "Cierre de sesion",
                    Criticidad_90DI = 1
                });

            }
            catch (Exception ex) {
                throw new Exception("Error al cerrar sesión: " + ex.Message);
            }
        }

        public bool updatePassword_90DI(int idUsuario, string password)
        {
            var hashPassword = getHashPassword_90DI(password);

            return _dal.UpdatePassword90DI(idUsuario, hashPassword);
        }

        public string getHashPassword_90DI(string password)
        {
            return SecurityService_90DI.HashPassword90DI(password);
        }

        public bool VerifyPassword_90DI(string password, string storedHash)
        {
            return SecurityService_90DI.Verify90DI(password, storedHash);
        }

        public List<User_90DI> GetAllUsers_90DI()
        {
            var users = _dal.GetAllUsers90DI();
            foreach (var u in users)
                if (!string.IsNullOrEmpty(u.Email_90DI))
                    u.Email_90DI = TryDecryptEmail(u.Email_90DI);
            return users;
        }

        public bool CreateUser_90DI(User_90DI user)
        {
            user.Password_90DI = getHashPassword_90DI(user.Password_90DI);
            if (!string.IsNullOrEmpty(user.Email_90DI))
                user.Email_90DI = SecurityService_90DI.ReversibleEncrypt_90DI(user.Email_90DI);

          
            var response = _dal.CreateUser90DI(user);
            if(response)
                CreateLogEvent_90DI("Usuario creado exitosamente: " + user.NombreUsuario_90DI, 2);
            return response;
        }

        public bool UnblockUser_90DI(int idUsuario)
        {
            var response = _dal.UnblockUser90DI(idUsuario);
            if(response)
                CreateLogEvent_90DI("Usuario desbloqueado: " + SessionManager_90DI.Instancia.userActual.NombreUsuario_90DI, 3);
            return response;
        }

        public bool UpdateUser_90DI(User_90DI user)
        {
            if (!string.IsNullOrEmpty(user.Email_90DI))
                user.Email_90DI = SecurityService_90DI.ReversibleEncrypt_90DI(user.Email_90DI);
            var response =  _dal.UpdateUser90DI(user);

            if(response)
                CreateLogEvent_90DI("Usuario actualizado exitosamente: " + user.NombreUsuario_90DI, 2);

            return response;
        }

        public bool ActivateUser_90DI(User_90DI user)
        {
            bool response;
            if (user.Activo_90DI)
            {
                response = _dal.DesactivateUser90DI(user.IdUsuario_90DI);
                CreateLogEvent_90DI("Usuario desactivado: " + user.NombreUsuario_90DI, 1);
            }
            else
            {
                response = _dal.ActivateUser90DI(user.IdUsuario_90DI);
                CreateLogEvent_90DI("Usuario activado: " + user.NombreUsuario_90DI, 1);
            }
            return response;
        }

        private void CreateLogEvent_90DI(string evento, byte criticidad = 3)
        {
            _bitacora.CreateLogEvent_90DI(new Event_90DI
            {
                Login_90DI = SessionManager_90DI.Instancia.userActual.NombreUsuario_90DI,
                Fecha_90DI = DateTime.Now,
                Hora_90DI = DateTime.Now.TimeOfDay,
                Modulo_90DI = "Usuarios",
                Evento_90DI = evento,
                Criticidad_90DI = criticidad
            });
        }

    }
}
