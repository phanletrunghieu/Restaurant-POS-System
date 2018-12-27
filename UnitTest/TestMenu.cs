using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;

namespace UnitTest
{
    [TestClass]
    public class TestMenu
    {
        private MenuBLL menuBLL = new MenuBLL();

        [TestMethod]
        public void TestCRUDMenu()
        {
            string nameExpected = "Ăn rạng sáng";
            Menu menu = menuBLL.CreateMenu(nameExpected);
            Assert.AreEqual(nameExpected, menu.Name);
            TestUpdateMenu(menu);
        }
        public void TestUpdateMenu(Menu menu)
        {
            string nameTest = "Ăn nữa đêm";
            menu.Name = nameTest;
            menuBLL.Update(menu, nameTest);
            List<Menu> menus = menuBLL.ListMenu();
            string nameExpected = "";
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].ID == menu.ID) nameExpected = menus[i].Name;
            }
            Assert.AreEqual(nameTest, nameExpected);
            TestDeleteMenu(menu);
        }
        public void TestDeleteMenu(Menu menu)
        {
            bool isDelete = true;
            menuBLL.Delete(menu);
            List<Menu> menus = menuBLL.ListMenu();
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].ID == menu.ID) isDelete = false;
            }
            Assert.AreEqual(isDelete, true);
        }
    }
}
