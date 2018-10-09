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
            using (MyDBContext model = new MyDBContext())
            {
                return model.Departments.ToList();
            }
        }
        public Department CreateDepartment(string name)
        {
            using (MyDBContext model = new MyDBContext())
            {
                Department department = new Department { Name = name };
                model.Departments.Add(department);
                model.SaveChanges();
                return department;
            }
        }

        public void DeleteDepartment(Department department)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Departments.Attach(department);
                model.Departments.Remove(department);
                model.SaveChanges();
            }
        }

        public void Update(Department department)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Departments.AddOrUpdate(department);
                model.SaveChanges();
            }
        }
    }
}
