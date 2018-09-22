using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;

namespace GUI
{
    public partial class TableControl : UserControl
    {
        public delegate void OnDeleteHandler(Table table);
        public event OnDeleteHandler OnDelete;

        private Table table;

        public TableControl()
        {
            InitializeComponent();
            this.UpdatGUI();
        }

        public TableControl(Table table)
        {
            this.table = table;
            InitializeComponent();
            this.UpdatGUI();
        }

        private void UpdatGUI()
        {
            this.txtName.Text = this.table.Name;
        }

        private void TableControl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OnClick" + this.table.Name);
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OnClick" + this.table.Name);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete table \"" + this.table.Name + "\"", "Confirm", MessageBoxButtons.YesNo);
            if(dr==DialogResult.Yes)
            {
                TableBLL tableBLL = new TableBLL();
                tableBLL.Delete(this.table);

                // fire event
                this.OnDelete(this.table);
            }
        }
    }
}
