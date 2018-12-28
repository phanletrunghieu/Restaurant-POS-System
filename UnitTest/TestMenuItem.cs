using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.IO;
using System;

namespace UnitTest
{
    [TestClass]
    public class TestMenuItem
    {
        private MenuBLL menuBLL = new MenuBLL();
        private MenuItemBLL menuItemBLL = new MenuItemBLL();

        [TestMethod]
        public void TestCRUDMenuItem()
        {
            List<Menu> menus = menuBLL.ListMenu();

            int menuIDTest = menus[0].ID;
            string nameTest = "Phá lấu bò gà heo";
            int priceTest = 30000;
            int priceAfterTest = 25000;
            string rootPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            byte[] imageTest = File.ReadAllBytes(rootPath + "/data/images/com-chien-ca-man.jpg");

            MenuItem menuItemTest = new MenuItem();
            menuItemTest.MenuID = menuIDTest;
            menuItemTest.Name = nameTest;
            menuItemTest.Price = priceTest;
            menuItemTest.PriceAfter = priceAfterTest;
            menuItemTest.Image = imageTest;

            MenuItem menuItemExpected = menuItemBLL.CreateMenuItem(menuItemTest);
            Assert.AreEqual(menuItemTest, menuItemExpected);

            TestUpdateMenuItem(menuItemExpected);
        }
        public void TestUpdateMenuItem(MenuItem menuItemOld)
        {
            List<Menu> menus = menuBLL.ListMenu();

            int menuIDTest = menus[0].ID;
            string nameTest = "Phá lấu bò gà heo chuột dê mèo";
            int priceTest = 50000;
            int priceAfterTest = 35000;
            string rootPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            byte[] imageTest = File.ReadAllBytes(rootPath + "/data/images/com-chien-ca-man.jpg");

            MenuItem menuItemNew = new MenuItem();
            menuItemNew = menuItemOld;
            menuItemNew.MenuID = menuIDTest;
            menuItemNew.Name = nameTest;
            menuItemNew.Price = priceTest;
            menuItemNew.PriceAfter = priceAfterTest;
            menuItemNew.Image = imageTest;

            menuItemBLL.Update(menuItemOld, menuItemNew);

            List<MenuItem> menuItems = menuItemBLL.FindByMenuID(menus[0]);
            MenuItem menuItemExpected = new MenuItem();
            for (int i=0;i< menuItems.Count; i++)
            {
                if (menuItems[i].ID == menuItemNew.ID)
                {
                    menuItemExpected.ID = menuItems[i].ID;
                    menuItemExpected.MenuID = menuItems[i].MenuID;
                    menuItemExpected.Name = menuItems[i].Name;
                    menuItemExpected.Price = menuItems[i].Price;
                    menuItemExpected.PriceAfter = menuItems[i].PriceAfter;
                    menuItemExpected.Image = imageTest;
                }
            }
            Assert.AreEqual(menuItemNew.ID, menuItemExpected.ID);
            Assert.AreEqual(menuItemNew.MenuID, menuItemExpected.MenuID);
            Assert.AreEqual(menuItemNew.Name, menuItemExpected.Name);
            Assert.AreEqual(menuItemNew.Price, menuItemExpected.Price);
            Assert.AreEqual(menuItemNew.PriceAfter, menuItemExpected.PriceAfter);
            Assert.AreEqual(menuItemNew.Image, imageTest);

            TestDeleteMenuItem(menuItemNew);
        }
        public void TestDeleteMenuItem(MenuItem menuItemNew)
        {
            bool isDelete = true;
            menuItemBLL.Delete(menuItemNew);
            List<Menu> menus = menuBLL.ListMenu();
            List<MenuItem> menuItems = menuItemBLL.FindByMenuID(menus[0]);
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].ID == menuItemNew.ID) isDelete = false;
            }
            Assert.AreEqual(isDelete, true);
        }
        [TestMethod]
        public void TestInputEmptyMenuItem()
        {
            try
            {
                bool isCreate = false;

                List<Menu> menus = menuBLL.ListMenu();

                MenuItem menuItemTest = new MenuItem();
                menuItemTest.MenuID = menus[0].ID;
                menuItemTest.Name = "";
                menuItemTest.Price = 0;
                menuItemTest.PriceAfter = 0;
                menuItemTest.Image = null;
                MenuItem menuItemExpected = menuItemBLL.CreateMenuItem(menuItemTest);

                List<MenuItem> menuItems = menuItemBLL.FindByMenuID(menus[0]);
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (menuItems[i] == menuItemTest) isCreate = true;
                }
                Assert.AreEqual(isCreate, false);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("", e.ToString());
            }
        }
    }
}
