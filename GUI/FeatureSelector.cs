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
    public partial class FeatureSelector : Form
    {
        public FeatureSelector()
        {
            InitializeComponent();
        }

        private void btnSystemSetup_Click(object sender, EventArgs e)
        {
            new RestaurantSetup().Show();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
        }
    }
}
