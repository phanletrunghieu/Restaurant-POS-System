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
            using (MyDBContext model = new MyDBContext())
            {
                return model.Menus.ToList();
            }
        }

        public Menu CreateMenu(string name)
        {
            using (MyDBContext model = new MyDBContext())
            {
                Menu menu = new Menu { Name = name };
                model.Menus.Add(menu);
                model.SaveChanges();
                return menu;
            }
        }

        public Menu Delete(Menu menu)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Menus.Attach(menu);

                //delete tables in this area
                model.MenuItems.RemoveRange(menu.MenuItems);
                model.Menus.Remove(menu);
                model.SaveChanges();
                return menu;
            }
        }

        public void Update(Menu menu, string name)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Menus.Attach(menu);
                menu.Name = name;
                model.Menus.AddOrUpdate(menu);
                model.SaveChanges();
            }
        }
    }
}
