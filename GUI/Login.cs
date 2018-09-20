using BLL;
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
                bool success = userBLL.Login(txtUserName.Text, txtPassword.Text);
                if (!success)
                {
                    throw new Exception("Login fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
