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
            using (MyDBContext model = new MyDBContext())
            {
                return model.Employees.ToList();
            }
        }

        public List<Employee> ListEmployeeByDepartment(Department department)
        {
            using (MyDBContext model = new MyDBContext())
            {
                List<Employee> listEmployee = new List<Employee>();
                List<EmployeeDepartment> employeeDepartment = model.EmployeeDepartments.Where(t => t.DepartmentID == department.ID).ToList();
                for (int i= 0; i< employeeDepartment.Count; i++)
                {
                    listEmployee.Add(employeeDepartment[i].Employee);
                }
                return listEmployee;
            }
        }

        public Employee CreateEmployee(string name , string userName , string passWord)
        {
            using (MyDBContext model = new MyDBContext())
            {
                Employee employee = new Employee { Name = name , Username = userName , Password = passWord };
                model.Employees.Add(employee);
                model.SaveChanges();
                return employee;
            }
        }

        public void DeleteByDepartment(List<EmployeeDepartment> employeeDepartment)
        {
            using (MyDBContext model = new MyDBContext())
            {
                for(int i = 0; i < employeeDepartment.Count; i++)
                {
                    model.EmployeeDepartments.Attach(employeeDepartment[i]);
                    model.Employees.Remove(employeeDepartment[i].Employee);
                }
                model.SaveChanges();
            }
        }

        public void Delete(Employee employee)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Employees.Attach(employee);
                model.EmployeeDepartments.RemoveRange(employee.EmployeeDepartments);
                model.Employees.Remove(employee);
                model.SaveChanges();
            }
        }

        public void Update(Employee employee)
        {
            using (MyDBContext model = new MyDBContext())
            {
                model.Employees.AddOrUpdate(employee);
                model.SaveChanges();
            }
        }
    }
}
