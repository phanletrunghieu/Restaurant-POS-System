using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AreaBLL
    {
        public List<Area> ListArea()
        {
            return Connection.DBContext.Areas.ToList();
        }

        public Area CreateArea(string name)
        {
            Area area= new Area { Name = "" };
            if (name != "" && name != null)
            {
                area = new Area { Name = name };
                Connection.DBContext.Areas.Add(area);
                Connection.DBContext.SaveChanges();
            }
            return area;
        }

        public Area Delete(Area area)
        {
            Connection.DBContext.Areas.Attach(area);

            //delete tables in this area
            Connection.DBContext.Tables.RemoveRange(area.Tables);
            Connection.DBContext.Areas.Remove(area);
            Connection.DBContext.SaveChanges();
            return area;
        }

        public void Update(Area area)
        {
            Connection.DBContext.Areas.AddOrUpdate(area);
            Connection.DBContext.SaveChanges();
        }
    }
}
