using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TableBLL
    {
        public List<Table> ListTablesByArea(Area area)
        {
            using (MyDBContext model = new MyDBContext())
            {
                return model.Tables.Where(t=>t.AreaID == area.ID).ToList();
            }
        }

        public Table CreateTable(Table table)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Tables.Add(table);
                model.SaveChanges();
                return table;
            }
        }

        public void CreateTables(List<Table> tables)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Tables.AddRange(tables);
                model.SaveChanges();
            }
        }

        public void Delete(Table table)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Tables.Attach(table);
                model.Tables.Remove(table);
                model.SaveChanges();
            }
        }

        public void Update(Table table)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Tables.AddOrUpdate(table);
                model.SaveChanges();
            }
        }
    }
}
