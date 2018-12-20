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
    public partial class PrintBill : Form
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

        public PrintBill()
        {
            InitializeComponent();
        }

        public PrintBill(DAL.Order order, decimal foodPrice, decimal discount, decimal extra, decimal vat, decimal totalPrice)
        {
            InitializeComponent();
            this.order = order;
            this.FoodPrice = foodPrice;
            this.Discount = discount;
            this.Extra = extra;
            this.VAT = vat;
            this.TotalPrice = totalPrice;
            this.LoadData();
        }

        private void LoadData()
        {
            this.tableLayoutPanel1.RowCount = this.order.OrderDetails.Count+1;
            var orderDetails = this.order.OrderDetails.ToList();
            for (int i = 0; i < orderDetails.Count; i++)
            {
                var od = orderDetails[i];
                Label lbFood = new Label { Text = od.MenuItem.Name };
                Label lbQuantity = new Label { Text = od.Quantity.ToString() };
                Label lbPrice = new Label { Text = string.Format("{0:0}", od.Price) + "đ" };
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
                this.tableLayoutPanel1.Controls.Add(lbFood, 0, i + 1);
                this.tableLayoutPanel1.Controls.Add(lbQuantity, 1, i + 1);
                this.tableLayoutPanel1.Controls.Add(lbPrice, 2, i + 1);
            }

            this.panelSumary.Location = new Point(0, this.tableLayoutPanel1.Location.Y + this.tableLayoutPanel1.Size.Height);
            this.lbInvoiceNo.Text = "Invoice No: " + this.order.InvoiceNo.ToString();
            this.lbCustomer.Text = "Customer: " + this.order.CustomerName;
            this.lbDate.Text = "Date: " + this.order.DateCreated.ToString();
            this.Balance = (decimal)this.order.MoneyCharge;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(this.panelPrint.Width, this.panelPrint.Height);
            this.panelPrint.DrawToBitmap(bmp, new Rectangle(0, 0, this.panelPrint.Width, this.panelPrint.Height));

            Rectangle recSource = new Rectangle(0, 0, (int)this.panelPrint.Width, (int)this.panelPrint.Height);
            Rectangle recDest = new Rectangle((int)x, (int)y, (int)this.panelPrint.Width * 2, (int)this.panelPrint.Height * 2);
            e.Graphics.DrawImage((Image)bmp, recDest, recSource, GraphicsUnit.Pixel);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printDialog.ShowDialog();
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog.ShowDialog();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog.ShowDialog();
        }
    }
}
