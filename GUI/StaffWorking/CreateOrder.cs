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

namespace GUI.StaffWorking
{
    public partial class CreateOrder : Form
    {
        public Table Table { get; set; }

        private List<SelectMenuItemControl> SelectedMenuItems = new List<SelectMenuItemControl>();

        public CreateOrder()
        {
            InitializeComponent();
            this.LoadData();
            this.Table = new Table
            {
                ID = 1,
                Name = "Bàn 101",
                Status = 1,
                AreaID = 1
            };
        }

        private void LoadData()
        {
            MenuBLL menuBLL = new MenuBLL();
            List<DAL.Menu> menus = menuBLL.ListMenu();

            this.tabControl.Controls.Clear();

            foreach (DAL.Menu menu in menus)
            {
                var t = new TabPage();
                t.Location = new Point(4, 22);
                t.Name = menu.Name;
                t.Padding = new Padding(3);
                t.Size = new Size(597, 257);
                t.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                t.Text = menu.Name;
                t.UseVisualStyleBackColor = true;
                t.AutoScroll = true;
                this.tabControl.Controls.Add(t);

                // add layout
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Dock = DockStyle.Fill;
                flowLayoutPanel.AutoScroll = true;
                t.Controls.Add(flowLayoutPanel);

                // add menu item
                MenuItemBLL menuItemBLL = new MenuItemBLL();
                List<DAL.MenuItem> menuItems = menuItemBLL.FindByMenuID(menu);
                for(int i = 0; i < menuItems.Count; i++)
                {
                    MenuItemControl menuItemControl = new MenuItemControl(menuItems[i]);
                    menuItemControl.Tag = i;
                    flowLayoutPanel.Controls.Add(menuItemControl);
                    menuItemControl.Click += new EventHandler(this.MenuItem_OnClick);
                }
            }
        }

        private void MenuItem_OnClick(object sender, EventArgs e)
        {
            MenuItemControl menuItemControl = (MenuItemControl)sender;
            DAL.MenuItem menuItem = menuItemControl.MenuItem;

            List<SelectMenuItemControl> listControls = this.flowLayoutPanelRight.Controls.OfType<SelectMenuItemControl>().ToList();
            try
            {
                SelectMenuItemControl find = listControls.Where(c => c.MenuItem.ID == menuItem.ID).Single();
                find.Quantity++;
            }
            catch (Exception)
            {
                SelectMenuItemControl selectMenuItem = new SelectMenuItemControl(menuItem, 1);
                selectMenuItem.Width = this.flowLayoutPanelRight.Width - 24;
                selectMenuItem.Height = (int)((double)selectMenuItem.Width / 3.6);
                selectMenuItem.OnDecrease += new SelectMenuItemControl.OnDecreaseHandle(this.SelectMenuItemControl_OnDecrease);
                this.flowLayoutPanelRight.Controls.Add(selectMenuItem);
            }
        }

        private void SelectMenuItemControl_OnDecrease(SelectMenuItemControl sender)
        {
            if (sender.Quantity == 0)
            {
                this.flowLayoutPanelRight.Controls.Remove(sender);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();

            List<SelectMenuItemControl> listControls = this.flowLayoutPanelRight.Controls.OfType<SelectMenuItemControl>().ToList();
            foreach(SelectMenuItemControl c in listControls)
            {
                listOrderDetail.Add(new OrderDetail
                {
                    MenuItemID = c.MenuItem.ID,
                    Price = c.MenuItem.Price,
                    Quantity = c.Quantity
                });
            }

            OrderBLL orderBLL = new OrderBLL();
            Order order = orderBLL.CreateOrder(GlobalData.EMPLOYEE, this.Table, this.txtCustomerName.Text, listOrderDetail);
            this.Close();
        }
    }
}
