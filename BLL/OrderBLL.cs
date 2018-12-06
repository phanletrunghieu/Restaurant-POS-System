using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBLL
    {
        public Order CreateOrder(Employee employee, List<Table> tables, string customerName, List<OrderDetail> listOrderDetail)
        {
            // create list ordertable from list table
            var orderTables = new List<OrderTable>();
            foreach(Table table in tables)
            {
                orderTables.Add(new OrderTable { TableID = table.ID });
            }

            Order order = new Order
            {
                CustomerName = customerName,
                EmployeeID = employee.ID,
                OrderTables = orderTables,
                OrderDetails = listOrderDetail,
                DateCreated = DateTime.Now,
            };
            Connection.DBContext.Orders.Add(order);
            Connection.DBContext.SaveChanges();
            order.InvoiceNo = order.ID.ToString("D5");
            Connection.DBContext.SaveChanges();

            //update table
            foreach (Table table in tables)
            {
                table.Status = 1;
                Connection.DBContext.Tables.AddOrUpdate(table);
            }
            Connection.DBContext.SaveChanges();

            return order;
        }

        public Order AddDiscount(Order order , int discount , byte discountType)
        {
            order.Discount = discount;
            order.DiscountType = discountType; // 1 : Cash  2 : Percent
            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.SaveChanges();
            return order;
        }

        public DAL.Order AddExtra(Order order, int extra, String content)
        {
            order.Extra = extra;
            order.ExtraContent = content;
            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.SaveChanges();
            return order;
        }

        public Order GetCurrentOrderByTable(Table table)
        {
            var orders = Connection.DBContext.Orders.Where(o => o.OrderTables.Where(ot => ot.TableID == table.ID).Count() > 0).ToList();
            return orders.Count > 0 ? orders[orders.Count-1] : null;
        }

        public void AddFood(List<OrderDetail> orderDetails)
        {
            Connection.DBContext.OrderDetails.AddRange(orderDetails);
            Connection.DBContext.SaveChanges();
        }

        public void AddVAT(Order order, decimal? vat)
        {
            order.VAT = vat;
            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.SaveChanges();
        }

        public Order ChangeTable(Order order, List<Table> tables)
        {
            foreach (OrderTable ot in order.OrderTables)
            {
                Table x = ot.Table;
                x.Status = 0;
                Connection.DBContext.Tables.AddOrUpdate(x);
            }

            order.OrderTables.Clear();
            foreach (Table table in tables)
            {
                table.Status = 1;
                Connection.DBContext.Tables.AddOrUpdate(table);
                order.OrderTables.Add(
                    new OrderTable
                    {
                        OrderID = order.ID,
                        TableID = table.ID
                    }
                );
            }
            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.SaveChanges();
            return order;
        }

        public Order Pay(Order order, decimal moneyReceive, decimal moneyBalance)
        {
            order = Connection.DBContext.Orders.SingleOrDefault(o=>o.ID == order.ID);
            if (order != null)
            {
                foreach (OrderTable ot in order.OrderTables)
                {
                    ot.Table.Status = 0;
                }
                order.MoneyReceive = moneyReceive;
                order.MoneyCharge = moneyBalance;
                Connection.DBContext.SaveChanges();
            }
            return order;
        }
    }
}
