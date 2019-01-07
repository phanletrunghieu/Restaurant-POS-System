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

            // calculate food price (price before)
            decimal priceBefore = 0;
            foreach(OrderDetail od in listOrderDetail)
            {
                if (od.Price != null)
                    priceBefore += (decimal)od.Price * (int)od.Quantity;
            }
            Order order = new Order
            {
                CustomerName = "",
                EmployeeID = null,
                OrderTables = null,
                OrderDetails = null,
                DateCreated = DateTime.Now,
                PriceBefore = 0,
                PriceAfter = 0,
            };
            if (customerName!= null && customerName != "")
            {
                if (employee.ID != null)
                {
                    if (orderTables != null)
                    {
                        if (listOrderDetail != null)
                        {
                            if (priceBefore != null && priceBefore != 0)
                            {
                                order = new Order
                                {
                                    CustomerName = customerName,
                                    EmployeeID = employee.ID,
                                    OrderTables = orderTables,
                                    OrderDetails = listOrderDetail,
                                    DateCreated = DateTime.Now,
                                    PriceBefore = priceBefore,
                                    PriceAfter = priceBefore,
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
                            }
                        }
                    }
                }
            }
            return order;
        }

        private decimal CalculatePriceAfter(Order order)
        {
            if (order.PriceBefore != null)
            {
                decimal price = (decimal)order.PriceBefore;
                // discount
                if (order.Discount != null)
                    price -= (decimal)order.Discount;

                // extra
                if (order.Extra != null)
                    price += (decimal)order.Extra;

                // vat
                if (order.VAT != null)
                    price += price * (decimal)order.VAT / 100;

                return price;
            }
            return 0;
        }

        public Order AddDiscount(Order order , int discount , byte discountType)
        {
            order.Discount = discount;
            order.DiscountType = discountType; // 1 : Cash  2 : Percent
            order.PriceAfter = CalculatePriceAfter(order);
            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.SaveChanges();
            return order;
        }

        public DAL.Order AddExtra(Order order, int extra, String content)
        {
            order.Extra = extra;
            order.ExtraContent = content;
            order.PriceAfter = CalculatePriceAfter(order);
            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.SaveChanges();
            return order;
        }

        public Order GetCurrentOrderByTable(Table table)
        {
            var orders = Connection.DBContext.Orders.Where(o => o.OrderTables.Where(ot => ot.TableID == table.ID).Count() > 0).ToList();
            return orders.Count > 0 ? orders[orders.Count-1] : null;
        }

        public void AddFood(Order order, List<OrderDetail> orderDetails)
        {
            // calculate food price (price before)
            decimal priceBefore = 0;
            foreach (OrderDetail od in orderDetails)
            {
                if (od.Price != null)
                    priceBefore += (decimal)od.Price * (int)od.Quantity;
            }

            order.PriceBefore += priceBefore;
            order.PriceAfter = CalculatePriceAfter(order);

            Connection.DBContext.Orders.AddOrUpdate(order);
            Connection.DBContext.OrderDetails.AddRange(orderDetails);
            Connection.DBContext.SaveChanges();
        }

        public void RemoveFood(Order order, DAL.OrderDetail orderDetail)
        {
            Connection.DBContext.OrderDetails.Remove(orderDetail);
            Connection.DBContext.SaveChanges();
        }

        public void AddVAT(Order order, decimal? vat)
        {
            order.VAT = vat;
            order.PriceAfter = CalculatePriceAfter(order);
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
                if(table.Status == 0)
                {
                    // available table
                    table.Status = 1;
                    Connection.DBContext.Tables.AddOrUpdate(table);
                    order.OrderTables.Add(
                        new OrderTable
                        {
                            OrderID = order.ID,
                            TableID = table.ID
                        }
                    );
                } else if(table.Status == 1)
                {
                    // remove table from old order
                    Order oldOrder = GetCurrentOrderByTable(table);
                    OrderTable orderTable = oldOrder.OrderTables.First(ot => ot.TableID == table.ID);
                    if(orderTable != null)
                    {
                        Connection.DBContext.OrderTables.Remove(orderTable);
                    }

                    // add table to new order
                    order.OrderTables.Add(
                        new OrderTable
                        {
                            OrderID = order.ID,
                            TableID = table.ID
                        }
                    );

                    // move food
                    foreach(OrderDetail od in oldOrder.OrderDetails)
                    {
                        order.OrderDetails.Add(od);
                    }

                }
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
