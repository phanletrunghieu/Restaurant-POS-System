using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.SystemSetup.Department
{
    public partial class DepartmentEditDialog : Form
    {
        public DAL.Department department;

        public DepartmentEditDialog()
        {
            InitializeComponent();
            this.LoadData();
        }

        public DepartmentEditDialog(DAL.Department department)
        {
            this.department = department;

            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            this.tvNameOfDepartment.Text = this.department.Name;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void save()
        {
            this.department.Name = this.tvNameOfDepartment.Text;
            DepartmentBLL departmentBLL = new DepartmentBLL();
            departmentBLL.Update(this.department);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tvNameOfDepartment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.save();
            }
        }
    }
}
