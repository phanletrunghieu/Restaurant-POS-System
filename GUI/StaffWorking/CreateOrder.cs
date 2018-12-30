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
        public List<Table> OldTables;// use for reset table status (change table)
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

        private decimal foodPrice;
        public decimal FoodPrice
        {
            get { return foodPrice; }
            set
            {
                foodPrice = value;
                this.lbFoodPrice.Text = string.Format("{0:0}", foodPrice) + "đ";
                this.calculateTotalPrice();
            }
        }
        private decimal discount;
        public decimal Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                this.lbDiscount.Text = string.Format("{0:0}", discount) + "đ";
                this.calculateTotalPrice();
            }
        }
        private decimal extra;
        public decimal Extra
        {
            get { return extra; }
            set
            {
                extra = value;
                this.lbExtra.Text = string.Format("{0:0}", extra) + "đ";
                this.calculateTotalPrice();
            }
        }
        private decimal vat;
        public decimal VAT
        {
            get { return vat; }
            set
            {
                vat = value;
                this.lbVAT.Text = string.Format("{0:0}", vat) + "% (" + string.Format("{0:0}", (this.FoodPrice - this.Discount + this.Extra)*vat/100) + "đ)";
                this.calculateTotalPrice();
            }
        }
        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                this.lbTotal.Text = string.Format("{0:0}", totalPrice) + "đ";
            }
        }

        private List<SelectMenuItemControl> SelectedMenuItems = new List<SelectMenuItemControl>();

        public CreateOrder()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            this.Tables = new List<Table>{
                new Table
                {
                    ID = 1,
                    Name = "Bàn 101",
                    Status = 1,
                    AreaID = 1
                }
            };
            this.OldTables = new List<Table>(this.Tables);
        }

        public CreateOrder(Table table, Area area)
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            this.Tables = new List<Table>{table};
            this.OldTables = new List<Table>(this.Tables);
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
                if (order != null)
                {
                    foreach (OrderDetail od in this.order.OrderDetails)
                    {
                        this.NewSelectMenuItem(od.MenuItem, (int)od.Quantity, od);
                    }

                    // get order tables
                    this.Tables.Clear();
                    foreach(var od in this.order.OrderTables)
                    {
                        this.Tables.Add(od.Table);
                    }
                    this.lbTable.Text = this.getTableName();

                    this.OldTables = new List<Table>(this.Tables);
                }

                this.txtCustomerName.Text = this.order.CustomerName;

                this.calculateFoodPrice();
                this.calculateDiscount();
                this.Extra = this.order.Extra == null ? 0 : (decimal)this.order.Extra;
                if (this.order.VAT != null)
                    this.VAT = (decimal)this.order.VAT;
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

            // Show/Hide bottom buttonBar
            if (this.order == null)
            {
                this.layoutButton.Hide();
                this.tabControl.Height += this.layoutButton.Height;
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

        private void calculateFoodPrice()
        {
            decimal price = 0;
            List<SelectMenuItemControl> listControls = this.flowLayoutPanelRight.Controls.OfType<SelectMenuItemControl>().ToList();
            foreach(var control in listControls)
            {
                var p = control.MenuItem.Price == null ? 0 : (decimal)control.MenuItem.Price;
                price += p * control.Quantity;
            }

            this.FoodPrice = price;
        }

        private void calculateDiscount()
        {
            if(this.order.Discount == null)
            {
                this.Discount = 0;
                return;
            }

            decimal d = (decimal)this.order.Discount;
            this.Discount = this.order.DiscountType == 1 ? d : (d* this.foodPrice / 100);
        }

        private void calculateTotalPrice()
        {
            decimal price = this.FoodPrice;

            if (this.order!=null)
            {
                // discount
                price -= this.Discount;

                // extra
                if (this.order.Extra != null)
                    price += (decimal)this.order.Extra;

                // vat
                if (this.order.VAT != null)
                    price += price * (decimal)this.order.VAT / 100;
            }

            this.TotalPrice = price;
        }

        private void MenuItem_OnClick(object sender, EventArgs e)
        {
            MenuItemControl menuItemControl = (MenuItemControl)sender;
            DAL.MenuItem menuItem = menuItemControl.MenuItem;

            List<SelectMenuItemControl> listControls = this.flowLayoutPanelRight.Controls.OfType<SelectMenuItemControl>().ToList();
            try
            {
                SelectMenuItemControl find = listControls.Where(c => c.OrderDetail==null && c.MenuItem.ID == menuItem.ID).Single();
                find.Quantity++;
            }
            catch (Exception)
            {
                this.NewSelectMenuItem(menuItem, 1);
            }

            this.calculateFoodPrice();
        }

        private void NewSelectMenuItem(DAL.MenuItem menuItem, int quantity = 1, DAL.OrderDetail orderDetail = null)
        {
            SelectMenuItemControl selectMenuItem = new SelectMenuItemControl(menuItem, quantity, orderDetail);
            selectMenuItem.Width = this.flowLayoutPanelRight.Width - 24;
            selectMenuItem.Height = (int)((double)selectMenuItem.Width / 3.6);
            selectMenuItem.OnDecrease += new SelectMenuItemControl.OnDecreaseHandle(this.SelectMenuItemControl_OnDecrease);
            selectMenuItem.OnRemove += new SelectMenuItemControl.OnRemoveHandle(this.SelectMenuItemControl_OnRemove);
            this.flowLayoutPanelRight.Controls.Add(selectMenuItem);
        }

        private void SelectMenuItemControl_OnDecrease(SelectMenuItemControl sender)
        {
            if (sender.Quantity == 0)
            {
                this.flowLayoutPanelRight.Controls.Remove(sender);
            }
        }

        private void SelectMenuItemControl_OnRemove(SelectMenuItemControl sender)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete food '" + sender.MenuItem.Name + "'?", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                // remove food
                OrderBLL orderBLL = new OrderBLL();
                orderBLL.RemoveFood(this.order, sender.OrderDetail);
                this.flowLayoutPanelRight.Controls.Remove(sender);
                this.calculateFoodPrice();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();

            List<SelectMenuItemControl> listControls = this.flowLayoutPanelRight.Controls.OfType<SelectMenuItemControl>().ToList();
            foreach(SelectMenuItemControl c in listControls)
            {
                if (c.OrderDetail != null)
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
                orderBLL.AddFood(this.order, listOrderDetail);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            AddDiscountDialog addDiscountDialog = new AddDiscountDialog(order);
            addDiscountDialog.ShowDialog();
            if (addDiscountDialog.order != null)
            {
                this.order.Discount = addDiscountDialog.order.Discount;
                this.order.DiscountType = addDiscountDialog.order.DiscountType;
                this.calculateDiscount();
            }
        }

        private void btnExtra_Click(object sender, EventArgs e)
        {
            AddExtraDialog addExtraDialog = new AddExtraDialog(order);
            DialogResult dr = addExtraDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.order.Extra = addExtraDialog.order.Extra;
                this.order.ExtraContent = addExtraDialog.order.ExtraContent;
                this.Extra = this.order.Extra==null ? 0 : (decimal)this.order.Extra;
            }
        }
        
	    private void btnVAT_Click(object sender, EventArgs e)
        {
            DialogResult dr = new AddVAT(this.order).ShowDialog();
            if (dr == DialogResult.OK)
            {
                // update VAT in UI
                if (this.order.VAT != null)
                {
                    this.VAT = (decimal)this.order.VAT;
                }
                else
                {
                    this.VAT = 0;
                }
            }
        }

        private void btnChangeTable_Click(object sender, EventArgs e)
        {
            ChangeTable changeTable = new ChangeTable(this.order, this.Tables);
            changeTable.ShowDialog();
            this.Tables = changeTable.Tables;
            this.DialogResult = DialogResult.OK;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            PaymentDialog paymentDialog = new PaymentDialog(this.order, this.FoodPrice, this.Discount, this.Extra, this.VAT, this.TotalPrice);
            DialogResult dr = paymentDialog.ShowDialog();
            if(dr == DialogResult.OK)
            {
                for(int i = 0; i < this.Tables.Count; i++)
                {
                    this.Tables[i].Status = 0;
                }
            }
            this.DialogResult = DialogResult.Yes;
        }
    }
}
