using DAL;
using Entities_90DI;
using BLL_90DI;
using Services_90DI;

namespace BLL_90DI
{
    public class UsersBLL_90DI
    {
        private readonly UserDAL_90DI _dal = new UserDAL_90DI();

        private const int MAX_INTENTOS = 3;

        private int _intentos = 0;

        public UsersBLL_90DI() { }

        // Si el email ya está en texto plano (usuarios migrados), lo deja como está
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
            return _dal.CreateUser90DI(user);
        }

        public bool UnblockUser_90DI(int idUsuario)
        {
            return _dal.UnblockUser90DI(idUsuario);
        }

        public bool UpdateUser_90DI(User_90DI user)
        {
            if (!string.IsNullOrEmpty(user.Email_90DI))
                user.Email_90DI = SecurityService_90DI.ReversibleEncrypt_90DI(user.Email_90DI);
            return _dal.UpdateUser90DI(user);
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
