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

        public Area Delete(Area area)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Areas.Attach(area);

                //delete tables in this area
                model.Tables.RemoveRange(area.Tables);
                model.Areas.Remove(area);
                model.SaveChanges();
                return area;
            }
        }
    }
}
