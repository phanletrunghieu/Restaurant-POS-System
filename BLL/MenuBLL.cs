using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MenuBLL
    {
        public List<Menu> ListMenu()
        {
            return Connection.DBContext.Menus.ToList();
        }

        public Menu CreateMenu(string name)
        {
            Menu menu = new Menu { Name = "" };
            if (name != "" && name != null)
            {
                menu = new Menu { Name = name };
                Connection.DBContext.Menus.Add(menu);
                Connection.DBContext.SaveChanges();
            }
            return menu;
        }

        public Menu Delete(Menu menu)
        {
            Connection.DBContext.Menus.Attach(menu);

            //delete tables in this area
            Connection.DBContext.MenuItems.RemoveRange(menu.MenuItems);
            Connection.DBContext.Menus.Remove(menu);
            Connection.DBContext.SaveChanges();
            return menu;
        }

        public void Update(Menu menu, string name)
        {
            Connection.DBContext.Menus.Attach(menu);
            menu.Name = name;
            Connection.DBContext.Menus.AddOrUpdate(menu);
            Connection.DBContext.SaveChanges();
        }
    }
}
