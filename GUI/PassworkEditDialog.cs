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

namespace GUI
{
    public partial class PassworkEditDialog : Form
    {
        public Employee employee;

        public PassworkEditDialog()
        {
            InitializeComponent();
        }

        public PassworkEditDialog(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.tvNewPassword.Text == "")
            {
                MessageBox.Show("Please, enter your new password!", "Warning", MessageBoxButtons.OK);
            }
            else
            {
                if (this.tvOldPasswork.Text == "")
                {
                    MessageBox.Show("Please, enter your old password!", "Warning", MessageBoxButtons.OK);
                }
                else
                {
                    if (this.tvRetypeNewPassword.Text == "")
                    {
                        MessageBox.Show("Please, enter your retype password!", "Warning", MessageBoxButtons.OK);
                    }else
                    {
                        if (!BCrypt.Net.BCrypt.CheckPassword(this.tvOldPasswork.Text, employee.Password))
                        {
                            MessageBox.Show("Old password entered incorrectly!", "Warning", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (this.tvNewPassword.Text != this.tvRetypeNewPassword.Text)
                            {
                                MessageBox.Show("You re-enter the password incorrectly!", "Warning", MessageBoxButtons.OK);
                            }else
                            {
                                EmployeeBLL employeeBLL = new EmployeeBLL();

                                string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
                                string userNewPasswork = BCrypt.Net.BCrypt.HashPassword(this.tvRetypeNewPassword.Text, mySalt);

                                this.employee.Password = userNewPasswork;
                                employeeBLL.Update(employee);

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
