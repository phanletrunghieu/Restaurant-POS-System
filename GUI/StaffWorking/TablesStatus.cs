using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.StaffWorking
{
    public partial class TablesStatus : Form
    {
        public TablesStatus()
        {
            InitializeComponent();
        }

        private void tableControl1_Click(object sender, EventArgs e)
        {
            new CreateOrder().Show();
        }
    }
}
