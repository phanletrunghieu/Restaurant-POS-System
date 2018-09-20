using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class UserBLL
    {
        MyDBContext model = new MyDBContext();

        public void Login(string Username, string Pass)
        {
            model.Areas.Add(new Area { Name = "xxxx" });
            model.SaveChanges();
        }
    }
}
