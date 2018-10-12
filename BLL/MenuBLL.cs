using DAL;
using System;
using System.Collections.Generic;
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
    }
}
