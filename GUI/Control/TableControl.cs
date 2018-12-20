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

namespace GUI.Control
{
    public partial class TableControl : UserControl
    {
        public delegate void OnDeleteHandler(Table table);
        public event OnDeleteHandler OnDelete;

        public delegate void OnEditHandler(TableControl tableControl);
        public event OnEditHandler OnEdit;

        private Table table;
        public Table Table {
            get { return table; }
            set
            {
                table = value;
                this.UpdatGUI();
            }
        }

        private bool useBkColorForStatus;
        public bool UseBkColorForStatus {
            get { return useBkColorForStatus; }
            set
            {
                useBkColorForStatus = value;
                this.ContextMenuStrip = useBkColorForStatus ? null : this.contextMenuStrip;
                this.txtName.ContextMenuStrip = useBkColorForStatus ? null : this.contextMenuStrip;
            }
        }

        public TableControl()
        {
            InitializeComponent();
            this.Table = new Table { Name = "Bàn 1" };
        }

        public TableControl(Table table, bool useBkColorForStatus)
        {
            InitializeComponent();
            AddEvent();
            this.UseBkColorForStatus = useBkColorForStatus;
            this.Table = table;
        }

        private void AddEvent()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].MouseDown += new MouseEventHandler((object sender, MouseEventArgs e) => this.OnMouseDown(e));
            }
        }

        private void UpdatGUI()
        {
            this.txtName.Text = this.Table.Name;

            if (this.UseBkColorForStatus)
            {
                switch (this.Table.Status)
                {
                    case 0:
                        //empty table
                        this.BackColor = System.Drawing.SystemColors.ControlDark;
                        break;
                    case 1:
                        // serving
                        this.BackColor = Color.Green;
                        break;
                    case 2:
                        //request payment
                        this.BackColor = Color.Red;
                        break;
                    default:
                        //empty table
                        this.BackColor = System.Drawing.SystemColors.ControlDark;
                        break;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete table \"" + this.Table.Name + "\"", "Confirm", MessageBoxButtons.YesNo);
            if(dr==DialogResult.Yes)
            {
                TableBLL tableBLL = new TableBLL();
                tableBLL.Delete(this.Table);

                // fire event
                if (this.OnDelete != null)
                    this.OnDelete(this.Table);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.OnEdit != null)
                this.OnEdit(this);
        }

        private void txtName_DoubleClick(object sender, EventArgs e)
        {
            if (this.OnEdit != null)
                this.OnEdit(this);
        }

        private void TableControl_DoubleClick(object sender, EventArgs e)
        {
            if (this.OnEdit != null)
                this.OnEdit(this);
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
