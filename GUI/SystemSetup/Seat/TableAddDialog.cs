using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.SystemSetup.Seat
{
    public partial class TableAddDialog : Form
    {
        Area area;

        public TableAddDialog()
        {
            InitializeComponent();
        }

        public TableAddDialog(Area area)
        {
            InitializeComponent();

            this.area = area;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.addTable();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.addTable();
            }
        }

        private void addTable()
        {
            TableBLL tableBLL = new TableBLL();
            tableBLL.CreateTable(new Table { Name = this.txtName.Text, AreaID = this.area.ID });
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
