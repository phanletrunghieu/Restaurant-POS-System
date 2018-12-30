using GUI.SystemSetup.Department;
using GUI.SystemSetup.Menu;
using GUI.SystemSetup.Seat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class RestaurantSetup : Form
    {
        public RestaurantSetup()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {
            new SeatSetup().Show();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            new DepartmentSetup().Show();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            new MenuSetup().Show();
        }
    }
}
