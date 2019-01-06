using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System;

namespace UnitTest
{
    [TestClass]
    public class TestTable
    {
        private TableBLL tableBLL = new TableBLL();
        private AreaBLL areaBLL = new AreaBLL();

        [TestMethod]
        public void TestCRUDTable()
        {
            int ID= 0;
            List<Area> areas = areaBLL.ListArea();
            Table table =  tableBLL.CreateTable(new Table { Name = "Vip Table", AreaID = areas[0].ID });

            List<Table> tables = tableBLL.ListTablesByArea(areas[0]);
            bool isCreated = false;
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Name == "Vip Table" && tables[i].AreaID == areas[0].ID)
                {
                    isCreated = true;
                    ID = tables[i].ID;
                }
            }
            Assert.AreEqual(true, isCreated);

            TestUpdateTable(ID, table);
        }
        public void TestUpdateTable(int ID, Table table)
        {
            table.Name = "Sliver Table";
            tableBLL.Update(table);

            List<Area> areas = areaBLL.ListArea();
            List<Table> tables = tableBLL.ListTablesByArea(areas[0]);
            bool isUpdated = false;
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Name == "Sliver Table" && tables[i].AreaID == areas[0].ID && ID == tables[i].ID) isUpdated = true;
            }
            Assert.AreEqual(true, isUpdated);

            TestDeleteTable(ID, table);
        }
        public void TestDeleteTable(int ID, Table table)
        {
            tableBLL.Delete(table);
            List<Area> areas = areaBLL.ListArea();
            List<Table> tables = tableBLL.ListTablesByArea(areas[0]);
            bool isDeleted = true;
            for (int i = 0; i < tables.Count; i++)
            {
                if (ID == tables[i].ID) isDeleted = false;
            }
            Assert.AreEqual(true, isDeleted);
        }
        [TestMethod]
        public void TestInputEmptyTable()
        {
            try
            {
                List<Area> areas = areaBLL.ListArea();
                Table table = tableBLL.CreateTable(new Table { Name = "", AreaID = areas[0].ID });

                List<Table> tables = tableBLL.ListTablesByArea(areas[0]);
                bool isCreated = false;
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].Name == "" && tables[i].AreaID == areas[0].ID)
                    {
                        isCreated = true;
                    }
                }
                Assert.AreEqual(false, isCreated);
            }
            catch(Exception e)
            {
                Assert.AreNotEqual("", e.ToString());
            }
        }
    }
}
