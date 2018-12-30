using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Utilities;

namespace GUI.Control
{
    public partial class SelectMenuItemControl : UserControl
    {
        int hover;

        private DAL.MenuItem menuItem;
        public DAL.MenuItem MenuItem
        {
            get { return menuItem; }
            set
            {
                menuItem = value;
                this.lbName.Text = menuItem.Name;
                this.lbPrice.Text = string.Format("{0:0}", menuItem.Price) + "đ";
                this.pictureBox.Image = UtilsImage.ByteArrayToImage(menuItem.Image);
            }
        }

        // use for ordered menuitem
        private DAL.OrderDetail orderDetail;
        public DAL.OrderDetail OrderDetail
        {
            get { return orderDetail; }
            set
            {
                orderDetail = value;
                if (orderDetail != null)
                {
                    this.btnDecrease.Hide();
                    this.btnRemove.Show();
                }
                else
                {
                    this.btnDecrease.Show();
                    this.btnRemove.Hide();
                }
            }
        }

        public delegate void OnDecreaseHandle(SelectMenuItemControl sender);
        public delegate void OnRemoveHandle(SelectMenuItemControl sender);
        public event OnDecreaseHandle OnDecrease;
        public event OnRemoveHandle OnRemove;

        private int quantity;
        public int Quantity {
            get { return quantity; }
            set
            {
                quantity = value;
                this.lbQuantity.Text = "x" + quantity.ToString();
            }
        }

        public SelectMenuItemControl()
        {
            InitializeComponent();
        }

        public SelectMenuItemControl(DAL.MenuItem menuItem, int quantity, DAL.OrderDetail orderDetail = null)
        {
            InitializeComponent();
            AddEvent();
            this.Quantity = quantity;
            this.MenuItem = menuItem;
            this.OrderDetail = orderDetail;
        }

        private void AddEvent()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].MouseEnter += new EventHandler((object sender, EventArgs e) => this.OnMouseEnter(e));
                this.Controls[i].MouseLeave += new EventHandler((object sender, EventArgs e) => this.OnMouseLeave(e));
                this.Controls[i].Click += new EventHandler((object sender, EventArgs e) => this.OnClick(e));
            }
        }

        private void SelectMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.hover++;
            if (this.hover > 0)
            {
                this.BackColor = Color.White;
            }
        }

        private void SelectMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.hover--;
            if (this.hover <= 0)
            {
                this.BackColor = Color.Transparent;
            }
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            this.Quantity--;

            if (this.OnDecrease != null)
                this.OnDecrease(this);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.OnRemove != null)
                this.OnRemove(this);
        }
    }
}
