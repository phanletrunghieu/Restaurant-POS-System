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
        public List<Table> Tables;
        private Order order; // if booked

        private Area area;
        public Area Area
        {
            get { return area; }
            set
            {
                area = value;
                lbArea.Text = area.Name;
            }
        }

        private List<SelectMenuItemControl> SelectedMenuItems = new List<SelectMenuItemControl>();

        public CreateOrder()
        {
            InitializeComponent();
            this.Tables = new List<Table>{
                new Table
                {
                    ID = 1,
                    Name = "Bàn 101",
                    Status = 1,
                    AreaID = 1
                }
            };
        }

        public CreateOrder(Table table, Area area)
        {
            InitializeComponent();
            this.Tables = new List<Table>{table};
            this.Area = area;
        }

        private void CreateOrder_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.lbTable.Text = this.getTableName();

            // Get order
            if(this.Tables[0].Status != 0)
            {
                // had created order
                OrderBLL orderBLL = new OrderBLL();
                this.order = orderBLL.GetCurrentOrderByTable(this.Tables[0]);
                foreach(OrderDetail od in this.order.OrderDetails)
                {
                    this.NewSelectMenuItem(od.MenuItem, (int)od.Quantity, true);
                }
            }

            // Load menu
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

        private string getTableName()
        {
            string res="";
            foreach(Table table in this.Tables)
                res += table.Name + ", ";
            if (res.Length > 0)
                res = res.Substring(0, res.Length - 2);

            return res;
        }

        private void MenuItem_OnClick(object sender, EventArgs e)
        {
            MenuItemControl menuItemControl = (MenuItemControl)sender;
            DAL.MenuItem menuItem = menuItemControl.MenuItem;

            List<SelectMenuItemControl> listControls = this.flowLayoutPanelRight.Controls.OfType<SelectMenuItemControl>().ToList();
            try
            {
                SelectMenuItemControl find = listControls.Where(c => !c.ReadOnly && c.MenuItem.ID == menuItem.ID).Single();
                find.Quantity++;
            }
            catch (Exception)
            {
                this.NewSelectMenuItem(menuItem, 1);
            }
        }

        private void NewSelectMenuItem(DAL.MenuItem menuItem, int quantity = 1, bool readOnly = false)
        {
            SelectMenuItemControl selectMenuItem = new SelectMenuItemControl(menuItem, quantity, readOnly);
            selectMenuItem.Width = this.flowLayoutPanelRight.Width - 24;
            selectMenuItem.Height = (int)((double)selectMenuItem.Width / 3.6);
            selectMenuItem.OnDecrease += new SelectMenuItemControl.OnDecreaseHandle(this.SelectMenuItemControl_OnDecrease);
            this.flowLayoutPanelRight.Controls.Add(selectMenuItem);
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
                if (c.ReadOnly)
                    continue;

                listOrderDetail.Add(new OrderDetail
                {
                    OrderID = this.order!=null ? this.order.ID : 0,
                    MenuItemID = c.MenuItem.ID,
                    Price = c.MenuItem.Price,
                    Quantity = c.Quantity
                });
            }

            OrderBLL orderBLL = new OrderBLL();

            if (this.order == null)
            {
                //create order
                orderBLL.CreateOrder(GlobalData.EMPLOYEE, this.Tables, this.txtCustomerName.Text, listOrderDetail);
            }
            else
            {
                // add food
                orderBLL.AddFood(listOrderDetail);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
