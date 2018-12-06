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

namespace GUI.StaffWorking
{
    public partial class PaymentDialog : Form
    {
        DAL.Order order;
        private decimal foodPrice;
        public decimal FoodPrice
        {
            get { return foodPrice; }
            set
            {
                foodPrice = value;
                this.lbFoodPrice.Text = string.Format("{0:0}", foodPrice) + "đ";
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
            }
        }
        private decimal vat;
        public decimal VAT
        {
            get { return vat; }
            set
            {
                vat = value;
                this.lbVAT.Text = string.Format("{0:0}", vat) + "% (" + string.Format("{0:0}", (this.FoodPrice - this.Discount + this.Extra) * vat / 100) + "đ)";
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
        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                this.lbBalance.Text = string.Format("{0:0}", balance) + "đ";
            }
        }

        public PaymentDialog()
        {
            InitializeComponent();
        }

        public PaymentDialog(DAL.Order order, decimal foodPrice, decimal discount, decimal extra, decimal vat, decimal totalPrice)
        {
            InitializeComponent();
            this.order = order;
            this.FoodPrice = foodPrice;
            this.Discount = discount;
            this.Extra = extra;
            this.VAT = vat;
            this.TotalPrice = totalPrice;
            this.numericUpDown1.Value = this.TotalPrice;
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            this.Balance = this.numericUpDown1.Value - this.TotalPrice;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OrderBLL orderBLL = new OrderBLL();
            orderBLL.Pay(this.order, this.numericUpDown1.Value, this.Balance);
            this.DialogResult = DialogResult.OK;
            new PrintBill(this.order, this.FoodPrice, this.Discount, this.Extra, this.VAT, this.TotalPrice).ShowDialog();
            this.Close();
        }
    }
}
