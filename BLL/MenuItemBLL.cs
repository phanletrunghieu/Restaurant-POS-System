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
            return Connection.DBContext.MenuItems.Where(m => m.MenuID == menu.ID).ToList();
        }

        public MenuItem CreateMenuItem(MenuItem menuItem)
        {
            if (menuItem.MenuID != null)
            {
                if (menuItem.Name != null && menuItem.Name !="")
                {
                    if (menuItem.Price != null && menuItem.Price != 0)
                    {
                        if (menuItem.PriceAfter != null && menuItem.PriceAfter != 0)
                        {
                            if (menuItem.Image != null)
                            {
                                Connection.DBContext.MenuItems.Add(menuItem);
                                Connection.DBContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            return menuItem;
        }

        public void Update(MenuItem oldMenuItem, MenuItem newMenuItem)
        {
            Connection.DBContext.MenuItems.Attach(oldMenuItem);
            newMenuItem.ID = oldMenuItem.ID;
            Connection.DBContext.MenuItems.AddOrUpdate(newMenuItem);
            Connection.DBContext.SaveChanges();
        }

        public void Delete(MenuItem menuItem)
        {
            Connection.DBContext.MenuItems.Attach(menuItem);
            Connection.DBContext.MenuItems.Remove(menuItem);
            Connection.DBContext.SaveChanges();
        }
    }
}
