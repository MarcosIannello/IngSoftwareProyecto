using Services_90DI.BLL;
using Services_90DI.DAL;
using Services_90DI.Entidades;

namespace Services_90DI
{
    public class UsersService90DI
    {
        private readonly UsuariosDAL200MI _dal = new UsuariosDAL200MI();

        private const int MAX_INTENTOS = 3;

        private int _intentos = 0;

        public UsersService90DI() { }

        public User90DI? getUserByUsername(string Username)
        {
            return _dal.GetUser90DI(Username);
        }

        public User90DI? Login(string username, string password)
        {
            var user = getUserByUsername(username);

            if (user != null)
            {
                if (PasswordHasher90DI.Verify90DI(password, user.Clave_90DI))
                {
                    return user;
                }
                else
                {
                    _intentos++;

                    if (_intentos >= MAX_INTENTOS)
                    {
                        _dal.blockUser90DI(user.IdUsuario_90DI);
                        return new User90DI { Bloqueo_90DI = true };
                    }
                    return null;
                }
            }

            return null;
        }

        public bool updatePassword(int idUsuario, string password)
        {
            var hashPassword = getHashPassword(password);

            return _dal.UpdatePassword90DI(idUsuario, hashPassword);
        }

        public string getHashPassword(string password)
        {
            return PasswordHasher90DI.HashPassword90DI(password);
        }
    }
}
