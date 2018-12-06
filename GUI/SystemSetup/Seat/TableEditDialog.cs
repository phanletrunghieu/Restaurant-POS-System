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
    public partial class TableEditDialog : Form
    {
        public Table table;

        public TableEditDialog()
        {
            InitializeComponent();
            this.LoadData();
        }

        public TableEditDialog(Table table)
        {
            this.table = table;

            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            this.txtName.Text = this.table.Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                this.save();
            }
        }

        private void save()
        {
            this.table.Name = this.txtName.Text;
            TableBLL tableBLL = new TableBLL();
            tableBLL.Update(this.table);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
