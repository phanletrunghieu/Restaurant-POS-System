using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class EmloyeeDepartmentBLL
    {
      
        public EmployeeDepartment CreateEmployeeDepartment(EmployeeDepartment employeeDepartment)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.EmployeeDepartments.Add(employeeDepartment);
                model.SaveChanges();
                return employeeDepartment;
            }
        }

        public void Delete(EmployeeDepartment employeeDepartment)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.EmployeeDepartments.Attach(employeeDepartment);
                model.EmployeeDepartments.Remove(employeeDepartment);
                model.SaveChanges();
            }
        }

        public void Update(EmployeeDepartment employeeDepartment)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.EmployeeDepartments.AddOrUpdate(employeeDepartment);
                model.SaveChanges();
            }
        }

        public List<EmployeeDepartment> ListEmployeeDepartmentByDepartment(Department department)
        {
            using (MyDBContext model = new MyDBContext())
            {
                return model.EmployeeDepartments.Where(t => t.DepartmentID == department.ID).ToList();
            }
        }
    }
}
