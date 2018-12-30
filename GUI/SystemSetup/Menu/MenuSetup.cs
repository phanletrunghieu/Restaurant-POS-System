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

namespace GUI.SystemSetup.Menu
{
    public partial class MenuSetup : Form
    {
        public MenuSetup()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            this.LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DAL.Menu menu = (DAL.Menu)this.tabControl.SelectedTab.Tag;
            DialogResult dr = MessageBox.Show("Delete menu \"" + menu.Name + "\"", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                MenuBLL menuBLL = new MenuBLL();
                menuBLL.Delete(menu);
                this.LoadData();
            }
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            AddMenu addMenu = new AddMenu();
            DialogResult dr = addMenu.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }
        private void LoadData()
        {
            MenuBLL menuBLL = new MenuBLL();
            List<DAL.Menu> menus = menuBLL.ListMenu();

            //lsCategory.DataSource = listMenuItem; problem

            this.tabControl.Controls.Clear();
            foreach (DAL.Menu menu in menus)
            {
                var t = new TabPage();
                t.Location = new Point(4, 22);
                //t.Name = menu.Name;
                t.Name = menu.Name;
                t.Padding = new Padding(3);
                t.Size = new Size(597, 257);
                t.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                t.Text = menu.Name;
                t.UseVisualStyleBackColor = true;
                t.AutoScroll = true;
                t.Tag = menu;
                this.tabControl.Controls.Add(t);

                // add layout
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Dock = DockStyle.Fill;
                flowLayoutPanel.AutoScroll = true;
                t.Controls.Add(flowLayoutPanel);

                // add menu item
                MenuItemBLL menuItemBLL = new MenuItemBLL();
                //List<DAL.MenuItem> menuItems = menuItemBLL.FindByMenuID(menu);
                List<DAL.MenuItem> menuItems = menuItemBLL.FindByMenuID(menu);
                //lsCategory.DataSource = listMenuItem;
                for (int i = 0; i < menuItems.Count; i++)
                {
                    MenuItemControl menuItemControl = new MenuItemControl(menuItems[i], false);
                    menuItemControl.Tag = i;
                    menuItemControl.OnEdit += new MenuItemControl.OnEditHandler(this.menuItemControl_OnEdit);
                    menuItemControl.OnDelete += new MenuItemControl.OnDeleteHandler(this.menuItemControl_OnDelete);
                    flowLayoutPanel.Controls.Add(menuItemControl);
                    //menuItemControl.Click += new EventHandler(this.MenuItem_OnClick);
                }
            }
        }

        private void btnEditMenu_Click(object sender, EventArgs e)
        {
            DAL.Menu menu = (DAL.Menu)this.tabControl.SelectedTab.Tag;

            AddMenu addMenu = new AddMenu(menu);
            DialogResult dr = addMenu.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            DAL.Menu menu = (DAL.Menu)this.tabControl.SelectedTab.Tag;

            AddFood addFood = new AddFood(menu);
            DialogResult dr = addFood.ShowDialog();
            if(dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        private void menuItemControl_OnDelete(DAL.MenuItem menuItem)
        {
            List<TabPage> tabPages = this.tabControl.Controls.OfType<TabPage>().ToList();
            for (int i = 0; i < tabPages.Count; i++)
            {
                List<FlowLayoutPanel> flowLayouts = tabPages[i].Controls.OfType<FlowLayoutPanel>().ToList();
                if(flowLayouts.Count > 0)
                {
                    List<MenuItemControl> controls = flowLayouts[0].Controls.OfType<MenuItemControl>().ToList();
                    for (int j = 0; j < controls.Count; j++)
                    {
                        if (controls[j].MenuItem.ID == menuItem.ID)
                        {
                            flowLayouts[0].Controls.RemoveAt(j);
                        }
                    }
                }
            }
        }

        private void menuItemControl_OnEdit(MenuItemControl sender)
        {
            DAL.Menu menu = (DAL.Menu)this.tabControl.SelectedTab.Tag;

            AddFood addFoodDialog = new AddFood(menu, sender.MenuItem);
            DialogResult dr = addFoodDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                sender.MenuItem = addFoodDialog.MenuItem;
            }
        }
    }
}

