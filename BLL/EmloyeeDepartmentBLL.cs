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
            Connection.DBContext.EmployeeDepartments.Add(employeeDepartment);
            Connection.DBContext.SaveChanges();
            return employeeDepartment;
        }

        public void Delete(EmployeeDepartment employeeDepartment)
        {
            Connection.DBContext.EmployeeDepartments.Attach(employeeDepartment);
            Connection.DBContext.EmployeeDepartments.Remove(employeeDepartment);
            Connection.DBContext.SaveChanges();
        }

        public void Update(EmployeeDepartment employeeDepartment)
        {
            Connection.DBContext.EmployeeDepartments.AddOrUpdate(employeeDepartment);
            Connection.DBContext.SaveChanges();
        }

        public List<EmployeeDepartment> ListEmployeeDepartmentByDepartment(Department department)
        {
            return Connection.DBContext.EmployeeDepartments.Where(t => t.DepartmentID == department.ID).ToList();
        }
    }
}
