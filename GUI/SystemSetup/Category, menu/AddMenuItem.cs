using System;
using BLL;
using DAL;
using GUI;
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
    public partial class AddMenuItem : Form
    {
        public AddMenuItem()
        {
            InitializeComponent();
            var menus = new MenuBLL().ListMenu();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = menus;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
