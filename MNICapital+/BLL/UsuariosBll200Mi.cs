using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UsuariosBll200MI
    {
        private readonly UsuariosDAL200MI _dal = new UsuariosDAL200MI();
        
        public UsuariosBll200MI() { }

        public Usuario200MI? getUserByUsername(string Username)
        {
            return _dal.GetUser200MI(Username);
        }

        public Usuario200MI? Login(string username,string password)
        {
            var hashPassword = getHashPassword(password);

            var user = getUserByUsername(username);

            

            if (user != null) { 
                
                if(PasswordHasher200MI.Verify(password, user.Clave_200MI)) return user;

            }

            return null;
        }

        public bool updatePassword(int idUsuario200MI, string password)
        {
            var hashPassword = getHashPassword(password);

            return _dal.UpdatePassword200MI(idUsuario200MI, hashPassword);
        }

        public string getHashPassword(string password)
        {
            return PasswordHasher200MI.HashPassword(password);
        }

    }
}
