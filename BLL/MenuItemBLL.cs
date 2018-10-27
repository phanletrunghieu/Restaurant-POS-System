using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MenuItemBLL
    {
        public List<MenuItem> FindByMenuID(Menu menu)
        {
            using (MyDBContext model = new MyDBContext())
            {
                return model.MenuItems.Where(m => m.MenuID == menu.ID).ToList();
            }
        }

        public MenuItem CreateMenuItem(MenuItem menuItem)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.MenuItems.Add(menuItem);
                model.SaveChanges();
                return menuItem;
            }
        }

        public void Update(MenuItem oldMenuItem, MenuItem newMenuItem)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.MenuItems.Attach(oldMenuItem);
                newMenuItem.ID = oldMenuItem.ID;
                model.MenuItems.AddOrUpdate(newMenuItem);
                model.SaveChanges();
            }
        }

        public void Delete(MenuItem menuItem)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.MenuItems.Attach(menuItem);
                model.MenuItems.Remove(menuItem);
                model.SaveChanges();
            }
        }
    }
}
