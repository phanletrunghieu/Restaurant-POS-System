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

namespace GUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please fill your username & password");
                return;
            }

            try
            {
                UserBLL userBLL = new UserBLL();
                Employee employee = userBLL.Find(this.txtUserName.Text);
                if(!BCrypt.Net.BCrypt.CheckPassword(txtPassword.Text, employee.Password))
                {
                    throw new Exception("Login fail");
                }

                GlobalData.EMPLOYEE = employee;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, null);
            }
        }
    }
}
