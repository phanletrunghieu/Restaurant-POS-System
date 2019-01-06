using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace BLL
{
    public class EmployeeBLL
    {
        public List<Employee> ListEmployee()
        {
            return Connection.DBContext.Employees.ToList();
        }

        public List<Employee> ListEmployeeByDepartment(Department department)
        {
            List<Employee> listEmployee = new List<Employee>();
            List<EmployeeDepartment> employeeDepartment = Connection.DBContext.EmployeeDepartments.Where(t => t.DepartmentID == department.ID).ToList();
            for (int i= 0; i< employeeDepartment.Count; i++)
            {
                listEmployee.Add(employeeDepartment[i].Employee);
            }
            return listEmployee;
        }

        public Employee CreateEmployee(string name , string userName , string passWord)
        {
            Employee employee = new Employee { Name = "", Username = "", Password = "" };
            if (name != "" && name != null)
            {
                if (userName != "" && userName != null)
                {
                    if (passWord != "" && passWord != null)
                    {
                        employee = new Employee { Name = name, Username = userName, Password = passWord };
                        Connection.DBContext.Employees.Add(employee);
                        Connection.DBContext.SaveChanges();
                    }
                }
            }
            return employee;
        }

        public void DeleteByDepartment(List<EmployeeDepartment> employeeDepartment)
        {
            for(int i = 0; i < employeeDepartment.Count; i++)
            {
                Connection.DBContext.EmployeeDepartments.Attach(employeeDepartment[i]);
                Connection.DBContext.Employees.Remove(employeeDepartment[i].Employee);
            }
            Connection.DBContext.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            Connection.DBContext.Employees.Attach(employee);
            Connection.DBContext.EmployeeDepartments.RemoveRange(employee.EmployeeDepartments);
            Connection.DBContext.Employees.Remove(employee);
            Connection.DBContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            Connection.DBContext.Employees.AddOrUpdate(employee);
            Connection.DBContext.SaveChanges();
        }
    }
}
