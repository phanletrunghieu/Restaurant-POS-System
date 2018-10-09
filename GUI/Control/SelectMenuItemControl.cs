﻿using System;
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
                this.lbPrice.Text = menuItem.Price.ToString();
                this.pictureBox.Image = UtilsImage.ByteArrayToImage(menuItem.Image);
            }
        }

        public delegate void OnDecreaseHandle(SelectMenuItemControl sender);
        public event OnDecreaseHandle OnDecrease;

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

        public SelectMenuItemControl(DAL.MenuItem menuItem, int quantity)
        {
            InitializeComponent();
            AddEvent();
            this.Quantity = quantity;
            this.MenuItem = menuItem;
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
    }
}
