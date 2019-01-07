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
using System.IO;

namespace GUI.SystemSetup.Menu
{
    public partial class AddFood : Form
    {
        private DAL.Menu menu;
        public DAL.MenuItem MenuItem;

        public AddFood()
        {
            InitializeComponent();
            this.LoadData();
        }

        public AddFood(DAL.Menu menu, DAL.MenuItem menuItem = null)
        {
            InitializeComponent();
            this.menu = menu;
            this.MenuItem = menuItem;
            this.LoadData();
        }

        private void LoadData()
        {
            if(this.MenuItem == null)
            {
                string relativePath = "/data/images/placeholder.png";
                string rootPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName;
                if (!File.Exists(rootPath + relativePath))
                {
                    rootPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                }
                this.pictureBox.Image = Utilities.UtilsImage.ByteArrayToImage(File.ReadAllBytes(rootPath + relativePath));
            } else
            {
                this.txtName.Text = this.MenuItem.Name;
                this.txtPrice.Value = (decimal)this.MenuItem.Price;
                this.pictureBox.Image = Utilities.UtilsImage.ByteArrayToImage(this.MenuItem.Image);
                this.btnAdd.Text = "Update";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtName.Text=="")
            {
                MessageBox.Show("Please fill food's name.");
                return;
            }

            DAL.MenuItem newMenuItem = new DAL.MenuItem
            {
                Name = txtName.Text,
                Price = txtPrice.Value,
                Image = Utilities.UtilsImage.ImageToByteArray(this.pictureBox.Image),
                MenuID = menu.ID
            };

            MenuItemBLL menuItemBLL = new MenuItemBLL();

            if (this.MenuItem == null)
                menuItemBLL.CreateMenuItem(newMenuItem);
            else
                menuItemBLL.Update(this.MenuItem, newMenuItem);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
            this.pictureBox.Image=Utilities.UtilsImage.ByteArrayToImage(File.ReadAllBytes(this.openFileDialog.FileName));
        }
    }
}
