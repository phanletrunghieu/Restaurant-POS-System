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
    public partial class DepartmentAddDialog : Form
    {
        public DepartmentAddDialog()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.addDepartment();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.addDepartment();
            }
        }
        private void addDepartment()
        {
            DepartmentBLL departmentBLL = new DepartmentBLL();
            departmentBLL.CreateDepartment(this.txtName.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
