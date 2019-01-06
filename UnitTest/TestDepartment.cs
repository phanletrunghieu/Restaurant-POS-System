using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System;

namespace UnitTest
{
    [TestClass]
    public class TestDepartment
    {
        private DepartmentBLL departmentBLL = new DepartmentBLL();

        [TestMethod]
        public void TestCRUDDepartment()
        {
            string nameExpected = "CEO";
            Department department= departmentBLL.CreateDepartment(nameExpected);
            bool isCreated = false;
            List<Department> departments = departmentBLL.ListDepartment();
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].Name == nameExpected) isCreated=true;
            }
            Assert.AreEqual(true, isCreated);
            TestUpdateDepartment(department);
        }
        public void TestUpdateDepartment(Department department)
        {
            string nameTest = "CTO";
            department.Name = nameTest;
            departmentBLL.Update(department);
            List<Department> departments = departmentBLL.ListDepartment();
            string nameExpected = "";
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].ID == department.ID) nameExpected = departments[i].Name;
            }
            Assert.AreEqual(nameTest, nameExpected);
            TestDeleteDepartment(department);
        }
        public void TestDeleteDepartment(Department department)
        {
            bool isDelete = true;
            departmentBLL.DeleteDepartment(department);
            List<Department> departments = departmentBLL.ListDepartment();
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].ID == department.ID) isDelete = false;
            }
            Assert.AreEqual(isDelete, true);
        }
        [TestMethod]
        public void TestInputEmptyDepartment()
        {
            try
            {
                bool isCreated = false;
                Department department = departmentBLL.CreateDepartment("");
                List<Department> departments = departmentBLL.ListDepartment();
                for (int i = 0; i < departments.Count; i++)
                {
                    if (departments[i].Name == "") isCreated = true;
                }
                Assert.AreEqual(false, isCreated);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("", e.ToString());
            }
        }
    }
}
