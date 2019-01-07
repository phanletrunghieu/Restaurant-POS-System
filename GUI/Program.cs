using GUI.StaffWorking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string absolute = Path.GetFullPath(baseDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login loginForm = new Login();
            DialogResult dr = loginForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (checkPermission())
                {
                    Application.Run(new FeatureSelector());
                }
                else
                {
                    Application.Run(new TablesStatus());
                }
            }
        }

        private static bool checkPermission()
        {
            bool isManager = false;
            foreach (DAL.EmployeeDepartment employeeDepartment in GlobalData.EMPLOYEE.EmployeeDepartments)
            {
                if (employeeDepartment.DepartmentID == 1)
                    isManager = true;
            }

            return isManager;
        }
    }
}
