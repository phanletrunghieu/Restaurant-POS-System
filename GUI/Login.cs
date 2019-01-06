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
        int timesTypeFailPassword = 0;

        bool lockLogin;
        public bool LockLogin
        {
            get { return lockLogin; }
            set
            {
                lockLogin = value;

                if (lockLogin)
                {
                    this.txtUserName.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.btnLogin.Enabled = false;
                    this.timer.Start();
                }
                else
                {
                    this.txtUserName.Enabled = true;
                    this.txtPassword.Enabled = true;
                    this.btnLogin.Enabled = true;
                    this.timer.Stop();
                }
            }
        }

        public Login()
        {
            InitializeComponent();
            this.LockLogin = false;
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

                this.timesTypeFailPassword++;
                if (this.timesTypeFailPassword >= 5)
                {
                    this.LockLogin = true;
                    this.timesTypeFailPassword = 0;
                    MessageBox.Show("You logined fail 5 times! Please wait 10s.");
                }
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, null);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // unlock login
            this.LockLogin = false;
        }
    }
}
