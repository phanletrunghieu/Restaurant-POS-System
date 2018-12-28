using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System;

namespace UnitTest
{
    [TestClass]
    public class TestEmployee
    {
        private EmployeeBLL employeeBLL = new EmployeeBLL();
        private EmloyeeDepartmentBLL emloyeeDepartmentBLL = new EmloyeeDepartmentBLL();
        private DepartmentBLL departmentBLL = new DepartmentBLL();

        [TestMethod]
        public void TestCRUDEmployee()
        {
            string userNameExpected = "AnLe";
            string nameExpected = "AnLe";
            string passWord = "AnLe";

            Employee employee = employeeBLL.CreateEmployee(nameExpected, userNameExpected, passWord);
            EmployeeDepartment employeeDepartment = new EmployeeDepartment();
            List<Department> departments = departmentBLL.ListDepartment();
            employeeDepartment.DepartmentID = departments[0].ID;
            employeeDepartment.EmployeeID = employee.ID;
            emloyeeDepartmentBLL.CreateEmployeeDepartment(employeeDepartment);

            bool isCreated = false;
            List<Employee> employees = employeeBLL.ListEmployeeByDepartment(departments[0]);
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Name == nameExpected&& employees[i].Username == userNameExpected) isCreated = true;
            }
            Assert.AreEqual(true, isCreated);
            TestUpdateInformationEmployee(employee, employeeDepartment);
        }
        public void TestUpdateInformationEmployee(Employee employee, EmployeeDepartment employeeDepartment)
        {
            string userNameExpected = "DanielLe";
            string nameExpected = "DanielLe";
            employee.Name = nameExpected;
            employee.Username = userNameExpected;
            employeeBLL.Update(employee);

            bool isUpdated = false;
            List<Department> departments = departmentBLL.ListDepartment();
            List<Employee> employees = employeeBLL.ListEmployeeByDepartment(departments[0]);
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Name == nameExpected && employees[i].Username == userNameExpected) isUpdated = true;
            }
            Assert.AreEqual(true, isUpdated);

            TestUpdatePassWordEmployee(employee, employeeDepartment);
        }
        public void TestUpdatePassWordEmployee(Employee employee, EmployeeDepartment employeeDepartment)
        {
            string passWord = "DanielLe";
            employee.Password = passWord;
            employeeBLL.Update(employee);

            bool isUpdated = false;
            List<Department> departments = departmentBLL.ListDepartment();
            List<Employee> employees = employeeBLL.ListEmployeeByDepartment(departments[0]);
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Password == passWord) isUpdated = true;
            }
            Assert.AreEqual(true, isUpdated);

            TestDeleteEmployee(employee, employeeDepartment);
        }
        public void TestDeleteEmployee(Employee employee, EmployeeDepartment employeeDepartment)
        {
            employeeBLL.Delete(employee);
            bool isDeleted = true;
            List<Department> departments = departmentBLL.ListDepartment();
            List<Employee> employees = employeeBLL.ListEmployeeByDepartment(departments[0]);
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Name == employee.Name && employees[i].Username == employee.Username) isDeleted = false;
            }
            Assert.AreEqual(true, isDeleted);
        }
        [TestMethod]
        public void TestInputEmptyEmployee()
        {
            try
            {
                string userNameExpected = "";
                string nameExpected = "";
                string passWord = "";

                Employee employee = employeeBLL.CreateEmployee(nameExpected, userNameExpected, passWord);
                EmployeeDepartment employeeDepartment = new EmployeeDepartment();
                List<Department> departments = departmentBLL.ListDepartment();

                bool isCreated = false;
                List<Employee> employees = employeeBLL.ListEmployeeByDepartment(departments[0]);
                for (int i = 0; i < employees.Count; i++)
                {
                    if (employees[i].Name == nameExpected && employees[i].Username == userNameExpected) isCreated = true;
                }
                Assert.AreEqual(false, isCreated);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("", e.ToString());
            }
        }
        [TestMethod]
        public void TestDeleteAllEmployeeAndDepartment()
        {
            string userNameExpected = "AnLe";
            string nameExpected = "AnLe";
            string passWord = "AnLe";

            string userNameExpected1 = "AnLe1";
            string nameExpected1 = "AnLe1";
            string passWord1 = "AnLe1";

            Employee employee = employeeBLL.CreateEmployee(nameExpected, userNameExpected, passWord);
            Employee employee1 = employeeBLL.CreateEmployee(nameExpected1, userNameExpected1, passWord1);
            EmployeeDepartment employeeDepartment = new EmployeeDepartment();
            EmployeeDepartment employeeDepartment1 = new EmployeeDepartment();

            Department department = departmentBLL.CreateDepartment("ANLE");

            employeeDepartment.DepartmentID = department.ID;
            employeeDepartment.EmployeeID = employee.ID;
            employeeDepartment1.DepartmentID = department.ID;
            employeeDepartment1.EmployeeID = employee1.ID;
            emloyeeDepartmentBLL.CreateEmployeeDepartment(employeeDepartment);
            emloyeeDepartmentBLL.CreateEmployeeDepartment(employeeDepartment1);

            employeeBLL.DeleteByDepartment(emloyeeDepartmentBLL.ListEmployeeDepartmentByDepartment(department));

            bool isDelete = true;
            departmentBLL.DeleteDepartment(department);
            List<Department> departments = departmentBLL.ListDepartment();
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].ID == department.ID) isDelete = false;
            }
            Assert.AreEqual(isDelete, true);
        }
    }
}
