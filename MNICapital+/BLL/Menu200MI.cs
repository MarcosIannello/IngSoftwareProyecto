using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class MenuBLL200MI
    {
        private readonly MenuDal200MI _dal = new MenuDal200MI();

        public List<Pantalla200MI> GetMenu()
        {
            var menus = _dal.GetMenuPadres();

            foreach(var menu in menus)
            {
                menu.Hijos = menus.FindAll(m => m.IdPadre_200MI == menu.IdPantalla_200MI);
            }

            return menus;
        }

    }
}
