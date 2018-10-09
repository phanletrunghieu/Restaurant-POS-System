using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBLL
    {
        public Order CreateOrder(Employee employee, Table table, string customerName, List<OrderDetail> listOrderDetail)
        {
            using (MyDBContext model = new MyDBContext())
            {
                Order order = new Order
                {
                    CustomerName = customerName,
                    EmployeeID = employee.ID,
                    OrderTables = new List<OrderTable>
                    {
                        new OrderTable{TableID = table.ID}
                    },
                    OrderDetails = listOrderDetail
                };
                model.Orders.Add(order);
                model.SaveChanges();
                order.InvoiceNo = order.ID.ToString("D5");
                model.SaveChanges();
                return order;
            }
        }
    }
}
