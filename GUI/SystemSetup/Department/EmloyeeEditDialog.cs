using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace GUI.SystemSetup.Department
{
    public partial class EmloyeeEditDialog : Form
    {
        public Employee employee;

        public EmloyeeEditDialog()
        {
            InitializeComponent();

            this.LoadData();
        }

        public EmloyeeEditDialog(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
            this.LoadData();
        }

        public void LoadData()
        {
            this.tvName.Text = this.employee.Name;
            this.tvUserName.Text = this.employee.Username;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.edit();
        }

        private void tvName_KeyUp(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                this.edit();
            }
        }

        private void tvUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.edit();
            }
        }
        private void edit()
        {
            if (this.tvName.Text == "")
            {
                MessageBox.Show("Please, enter your full name!", "Warning", MessageBoxButtons.OK);
            }
            else
            {
                if (this.tvUserName.Text == "")
                {
                    MessageBox.Show("Please, enter your full username!", "Warning", MessageBoxButtons.OK);
                }
                else
                {
                    EmployeeBLL employeeBLL = new EmployeeBLL();
                    this.employee.Name = this.tvName.Text;
                    this.employee.Username = this.tvUserName.Text;
                    employeeBLL.Update(employee);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
