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
            menuItemNew.ID = menuItemOld.ID;
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
                if (menuItems[i].ID == menuItemNew.ID) menuItemExpected = menuItems[i];
            }

            Assert.AreEqual(menuItemNew, menuItemExpected);
        }
    }
}
