using DAL;
using Entities_90DI;
using BLL_90DI;
using Services_90DI;

namespace BLL_90DI
{
    public class UsersBLL_90DI
    {
        private readonly UsuariosDAL_90DI _dal = new UsuariosDAL_90DI();

        private const int MAX_INTENTOS = 3;

        private int _intentos = 0;

        public UsersBLL_90DI() { }

        public User_90DI? getUserByUsername(string Username)
        {
            return _dal.GetUser90DI(Username);
        }

        public User_90DI? Login_90DI(string username, string password)
        {
            var user = getUserByUsername(username);

            if (user != null)
            {
                if (SecurityService_90DI.Verify90DI(password, user.Clave_90DI))
                {
                    return user;
                }
                else
                {
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

        public bool updatePassword_90DI(int idUsuario, string password)
        {
            var hashPassword = getHashPassword_90DI(password);

            return _dal.UpdatePassword90DI(idUsuario, hashPassword);
        }

        public string getHashPassword_90DI(string password)
        {
            return SecurityService_90DI.HashPassword90DI(password);
        }

        public List<User_90DI> GetAllUsers_90DI()
        {
            return _dal.GetAllUsers90DI();
        }

        public bool CreateUser_90DI(User_90DI user)
        {
            user.Clave_90DI = getHashPassword_90DI(user.Clave_90DI);
            return _dal.CreateUser90DI(user);
        }

        public bool UnblockUser_90DI(int idUsuario)
        {
            return _dal.UnblockUser90DI(idUsuario);
        }

        public bool UpdateUser_90DI(User_90DI user)
        {
            return _dal.EditUser90DI(user);
        }

        public bool ActivateUser_90DI(User_90DI user)
        {
            if (user.Activo_90DI)
            {
                return _dal.DesactivateUser90DI(user.IdUsuario_90DI);
            }
            return _dal.ActivateUser90DI(user.IdUsuario_90DI);
        }


    }
}
