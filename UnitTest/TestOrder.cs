using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System;

namespace UnitTest
{
    [TestClass]
    public class TestOrder
    {
        private OrderBLL orderBLL = new OrderBLL();
        private EmployeeBLL employeeBLL = new EmployeeBLL();
        private EmloyeeDepartmentBLL emloyeeDepartmentBLL = new EmloyeeDepartmentBLL();
        private DepartmentBLL departmentBLL = new DepartmentBLL();
        private MenuBLL menuBLL = new MenuBLL();
        private MenuItemBLL menuItemBLL = new MenuItemBLL();
        private TableBLL tableBLL = new TableBLL();
        private AreaBLL areaBLL = new AreaBLL();

        [TestMethod]
        public void TestCRUDOrder()
        {
            string customerName = "AnLe";

            List<Department> departments = departmentBLL.ListDepartment();
            List<Employee> employees = employeeBLL.ListEmployeeByDepartment(departments[0]);

            List<Menu> menus = menuBLL.ListMenu();
            List<MenuItem> menuItems = menuItemBLL.FindByMenuID(menus[0]);

            List<OrderDetail> listOrderDetail = new List<OrderDetail>();

            listOrderDetail.Add(new OrderDetail
            {
                OrderID = 0,
                MenuItemID = menuItems[0].ID,
                Price = menuItems[0].Price,
                Quantity = 10
            });
            listOrderDetail.Add(new OrderDetail
            {
                OrderID = 0,
                MenuItemID = menuItems[1].ID,
                Price = menuItems[1].Price,
                Quantity = 20
            });
            listOrderDetail.Add(new OrderDetail
            {
                OrderID = 0,
                MenuItemID = menuItems[2].ID,
                Price = menuItems[2].Price,
                Quantity = 30
            });

            List<Area> areas = areaBLL.ListArea();
            List<Table> allTables = tableBLL.ListTablesByArea(areas[0]);

            List<Table> tables = new List<Table>();
            tables.Add(allTables[0]);

            Order orderExpected = orderBLL.CreateOrder(employees[0], tables, customerName, listOrderDetail);

            Order currentOrder = orderBLL.GetCurrentOrderByTable(allTables[0]);

            Assert.AreEqual(currentOrder.CustomerName, customerName);
            Assert.AreEqual(currentOrder.EmployeeID, employees[0].ID);

            allTables = tableBLL.ListTablesByArea(areas[0]);

            Assert.AreEqual(1, allTables[0].Status);
            Assert.AreEqual(currentOrder.ID, orderExpected.ID);

            TestAddFood(currentOrder);
        }
        public void TestAddFood(Order order)
        {
            List<Menu> menus = menuBLL.ListMenu();
            List<MenuItem> menuItems = menuItemBLL.FindByMenuID(menus[1]);

            List<OrderDetail> listOrderDetail = new List<OrderDetail>();

            listOrderDetail.Add(new OrderDetail
            {
                OrderID = order.ID,
                MenuItemID = menuItems[0].ID,
                Price = menuItems[0].Price,
                Quantity = 10
            });
            listOrderDetail.Add(new OrderDetail
            {
                OrderID = order.ID,
                MenuItemID = menuItems[1].ID,
                Price = menuItems[1].Price,
                Quantity = 20
            });
            listOrderDetail.Add(new OrderDetail
            {
                OrderID = order.ID,
                MenuItemID = menuItems[2].ID,
                Price = menuItems[2].Price,
                Quantity = 30
            });
            orderBLL.AddFood(order, listOrderDetail); 

            TestAddVAT(order);
        }
        public void TestAddVAT(Order order)
        {
            decimal vat = 20;
            orderBLL.AddVAT(order, vat);

            List<Area> areas = areaBLL.ListArea();
            List<Table> allTables = tableBLL.ListTablesByArea(areas[0]);
            Order currentOrder = orderBLL.GetCurrentOrderByTable(allTables[0]);

            Assert.AreEqual(currentOrder.ID, order.ID);
            Assert.AreEqual(currentOrder.VAT, vat);

            TestAddExtra(currentOrder);
        }
        public void TestAddExtra(Order order)
        {
            int extra = 100000;
            string content = "Break table";

            orderBLL.AddExtra(order, extra, content);

            List<Area> areas = areaBLL.ListArea();
            List<Table> allTables = tableBLL.ListTablesByArea(areas[0]);
            Order currentOrder = orderBLL.GetCurrentOrderByTable(allTables[0]);

            Assert.AreEqual(currentOrder.ID, order.ID);
            Assert.AreEqual(currentOrder.Extra, extra);
            Assert.AreEqual(currentOrder.ExtraContent, content);

            TestAddDiscount(currentOrder);
        }
        public void TestAddDiscount(Order order)
        {
            int discount = 100000;
            byte discountType = 1;

            orderBLL.AddDiscount(order, discount, discountType);

            List<Area> areas = areaBLL.ListArea();
            List<Table> allTables = tableBLL.ListTablesByArea(areas[0]);
            Order currentOrder = orderBLL.GetCurrentOrderByTable(allTables[0]);

            Assert.AreEqual(currentOrder.ID, order.ID);
            Assert.AreEqual(currentOrder.Discount, discount);
            Assert.AreEqual(currentOrder.DiscountType, discountType);

            TestChangeTableForOrder(currentOrder);
        }
        public void TestChangeTableForOrder(Order order)
        {
            List<Area> areas = areaBLL.ListArea();
            List<Table> allTables = tableBLL.ListTablesByArea(areas[1]);

            List<Table> tables = new List<Table>();
            tables.Add(allTables[0]);

            Order currentOrder= orderBLL.ChangeTable(order, tables);

            Order newOrder= orderBLL.GetCurrentOrderByTable(tables[0]);

            List<Table> tables1 = tableBLL.ListTablesByArea(areas[0]);
            List<Table> tables2 = tableBLL.ListTablesByArea(areas[1]);

            Assert.AreNotEqual(tables1[0].Status, tables2[0].Status);
            Assert.AreEqual(newOrder.MoneyReceive, currentOrder.MoneyReceive);

            TestPay(currentOrder);
        }
        public void TestPay(Order order)
        {
            decimal moneyReceive = 199000;
            decimal moneyBalance = 0;

            order = orderBLL.Pay(order, moneyReceive, moneyBalance);

            Assert.AreEqual(order.MoneyCharge, moneyBalance);
            Assert.AreEqual(order.MoneyReceive, moneyReceive);
            foreach (OrderTable ot in order.OrderTables)
            {
                Assert.AreEqual(0, ot.Table.Status);
            }
        }
        [TestMethod]
        public void TestInputEmptyOrder()
        {
            try
            {
                List<Area> areas = areaBLL.ListArea();
                List<Table> allTables = tableBLL.ListTablesByArea(areas[0]);

                List<Table> tables = new List<Table>();
                tables.Add(allTables[0]);

                Order orderExpected = orderBLL.CreateOrder(null, tables, "", null);

                Order currentOrder = orderBLL.GetCurrentOrderByTable(allTables[0]);

                Assert.AreNotEqual(orderExpected, currentOrder);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("", e.ToString());
            }
        }
    }
}
