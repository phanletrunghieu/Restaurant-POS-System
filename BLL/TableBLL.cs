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
            return Connection.DBContext.Tables.Where(t => t.AreaID == area.ID).ToList();
        }

        public List<Table> ListAvailableTables()
        {
            return Connection.DBContext.Tables.Where(t => t.Status == 0).ToList();
        }

        public List<Table> ListOrderedTables()
        {
            return Connection.DBContext.Tables.Where(t => t.Status == 1).ToList();
        }

        public Table CreateTable(Table table)
        {
            if (table.Name!=null && table.Name != "")
            {
                Connection.DBContext.Tables.Add(table);
                Connection.DBContext.SaveChanges();
            }
            return table;
        }

        public void CreateTables(List<Table> tables)
        {
            Connection.DBContext.Tables.AddRange(tables);
            Connection.DBContext.SaveChanges();
        }

        public void Delete(Table table)
        {
            Connection.DBContext.Tables.Attach(table);
            Connection.DBContext.Tables.Remove(table);
            Connection.DBContext.SaveChanges();
        }

        public void Update(Table table)
        {
            Connection.DBContext.Tables.AddOrUpdate(table);
            Connection.DBContext.SaveChanges();
        }
    }
}
