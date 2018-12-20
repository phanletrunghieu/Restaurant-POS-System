using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class Connection
    {
        public static MyDBContext DBContext = new MyDBContext();
    }
}
