using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Utilities
{
    public class UtilsForm
    {
        public static string GetFormTitle(string title)
        {
            return title + " [" + GlobalData.EMPLOYEE.Name + "]";
        }
    }
}
