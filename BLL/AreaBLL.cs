using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AreaBLL
    {
        public List<Area> ListArea()
        {
            using (MyDBContext model = new MyDBContext())
            {
                return model.Areas.ToList();
            }
        }

        public Area CreateArea(string name)
        {
            using (MyDBContext model = new MyDBContext())
            {
                Area area = new Area { Name = name };
                model.Areas.Add(area);
                model.SaveChanges();
                return area;
            }
        }
    }
}
