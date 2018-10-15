using BLL;
using DAL;
using GUI.Control;
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
    public partial class MenuSetup : Form
    {
        public MenuSetup()
        {
            InitializeComponent();
            this.LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteMenu delMenu = new DeleteMenu();
            delMenu.Show();
        }

        private void lsCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            AddMenuItem addMenu = new AddMenuItem();
            addMenu.Show();
            
        }
        private void LoadData()
        {
            MenuBLL menuBLL = new MenuBLL();
            List<DAL.Menu> menus = menuBLL.ListMenu();

            //lsCategory.DataSource = listMenuItem; problem

            this.tabControl1.Controls.Clear();
            foreach (DAL.Menu Category in menus)
            {
                var t = new TabPage();
                t.Location = new Point(4, 22);
                //t.Name = menu.Name;
                t.Name = Category.Name;

                t.Padding = new Padding(3);
                t.Size = new Size(597, 257);
                t.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                t.Text = Category.Name;
                t.UseVisualStyleBackColor = true;
                t.AutoScroll = true;
                this.tabControl1.Controls.Add(t);

                // add layout
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Dock = DockStyle.Fill;
                flowLayoutPanel.AutoScroll = true;
                t.Controls.Add(flowLayoutPanel);

                // add menu item
                MenuItemBLL menuItemBLL = new MenuItemBLL();
                //List<DAL.MenuItem> menuItems = menuItemBLL.FindByMenuID(menu);
                List<DAL.MenuItem> menuItems = menuItemBLL.FindByMenuID(Category);
                //lsCategory.DataSource = listMenuItem;
                for (int i = 0; i < menuItems.Count; i++)
                {
                    MenuItemControl menuItemControl = new MenuItemControl(menuItems[i]);
                    menuItemControl.Tag = i;
                    flowLayoutPanel.Controls.Add(menuItemControl);
                    //menuItemControl.Click += new EventHandler(this.MenuItem_OnClick);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditMenu_Click(object sender, EventArgs e)
        {
            EditMenu editMenu = new EditMenu();
            editMenu.Show();
        }
    }
}

