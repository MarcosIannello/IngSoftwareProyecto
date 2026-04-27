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

    }
}
