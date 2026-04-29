using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UsuariosService90DI
    {
        private readonly UsuariosDAL90DI _dal = new UsuariosDAL90DI();
        
        public UsuariosService90DI() { }

        public Usuario90DI? getUserByUsername(string Username)
        {
            return _dal.getUser90DI(Username);
        }

        public Usuario90DI? Login(string username,string password)
        {
            var hashPassword = getHashPassword(password);

            var user = getUserByUsername(username);

            

            if (user != null) {

                if (PasswordHasher90DI.Verify90DI(password, user.Clave_90DI)) {
                    return user;
                
                }
                

            }

            return null;
        }

        public bool updatePassword(int idUsuario200MI, string password)
        {
            var hashPassword = getHashPassword(password);

            return _dal.updatePassword90DI(idUsuario200MI, hashPassword);
        }

        public string getHashPassword(string password)
        {
            return PasswordHasher90DI.HashPassword90DI(password);
        }

    }
}
