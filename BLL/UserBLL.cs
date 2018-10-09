using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL
    {
        public bool Login(string username, string pass)
        {
            using (MyDBContext model = new MyDBContext())
            {
                Employee employee = model.Employees.Where(e => e.Username == username).FirstOrDefault();
                if(employee == null)
                {
                    throw new Exception("Employee is not exist");
                }
                return BCrypt.Net.BCrypt.CheckPassword(pass, employee.Password);
            }
        }
    }
}
