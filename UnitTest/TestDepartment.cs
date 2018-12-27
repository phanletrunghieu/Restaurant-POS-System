using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;

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
            Assert.AreEqual(nameExpected, department.Name);
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
    }
}
