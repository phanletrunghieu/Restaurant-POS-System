using DAL;
using System;
using System.Collections.Generic;
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
    }
}
