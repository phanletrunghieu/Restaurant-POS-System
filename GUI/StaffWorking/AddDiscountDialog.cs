using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace GUI.StaffWorking
{
    public partial class AddDiscountDialog : Form
    {
        public Order order;
        Byte typeDiscount;

        public AddDiscountDialog()
        {
            InitializeComponent();
        }

        public AddDiscountDialog(Order order)
        {
            InitializeComponent();
            this.order = order;
            this.loadData();
        }

        private void loadData()
        {
            if(order != null)
            {
                if(order.Discount != null && order.DiscountType != null)
                {
                    if (order.DiscountType == 1) rbCash.Checked = true;
                    else rbPercent.Checked = true;
                    tvDiscount.Value = (decimal)order.Discount;
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            this.addDiscount();
        }

        private void tvDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.addDiscount();
            }
        }

        private void addDiscount()
        {
            if(rbCash.Checked ==false && rbPercent.Checked == false)
            {
                MessageBox.Show("Please, Select a valid discount!", "Warning", MessageBoxButtons.OK);
            }
            else
            {
                if(tvDiscount.Value == 0)
                {
                    MessageBox.Show("Please, Enter the value for the discount!", "Warning", MessageBoxButtons.OK);
                }
                else
                {
                    if(rbPercent.Checked == true)
                    {
                        if (tvDiscount.Value <= 100 && tvDiscount.Value > 0)
                        {
                            typeDiscount = 2;
                            OrderBLL orderBLL = new OrderBLL();
                            this.order = orderBLL.AddDiscount(order, Convert.ToInt32(tvDiscount.Value), typeDiscount);
                            this.Close();
                        }
                        else MessageBox.Show("The value you entered is not valid for a percentage!", "Warning", MessageBoxButtons.OK);
                    }
                    else
                    {
                        typeDiscount = 1;
                        OrderBLL orderBLL = new OrderBLL();
                        this.order = orderBLL.AddDiscount(order, Convert.ToInt32(tvDiscount.Value), typeDiscount);
                        this.Close();
                    }

                }
            }
        }

        private void rbPercent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPercent.Checked == true) rbCash.Checked = false;
        }

        private void rbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCash.Checked == true) rbPercent.Checked = false;
        }
    }
}
