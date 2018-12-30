using GUI.StaffWorking;
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
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
        }

        private void btnSystemSetup_Click(object sender, EventArgs e)
        {
            new RestaurantSetup().ShowDialog();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            new TablesStatus().ShowDialog();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            new Analytics().ShowDialog();
        }
    }
}
