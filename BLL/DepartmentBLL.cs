using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity.Migrations;

namespace BLL
{
    public class DepartmentBLL
    {
        public List<Department> ListDepartment()
        {
            return Connection.DBContext.Departments.ToList();
        }
        public Department CreateDepartment(string name)
        {
            Department department = new Department { Name = "" };
            if (name != "" && name != null)
            {
                department = new Department { Name = name };
                Connection.DBContext.Departments.Add(department);
                Connection.DBContext.SaveChanges();
            }
            return department;
        }

        public void DeleteDepartment(Department department)
        {
            Connection.DBContext.Departments.Attach(department);
            Connection.DBContext.Departments.Remove(department);
            Connection.DBContext.SaveChanges();
        }

        public void Update(Department department)
        {
            Connection.DBContext.Departments.AddOrUpdate(department);
            Connection.DBContext.SaveChanges();
        }
    }
}
