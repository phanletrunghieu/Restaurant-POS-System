using BLL;
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
    public partial class ChangeTable : Form
    {
        private DAL.Order order;
        private List<DAL.Table> tables;
        public List<DAL.Table> Tables
        {
            get { return this.tables; }
            set { this.tables = value; }
        }
        public List<DAL.Table> ListAvailableTables = new List<DAL.Table>();
        public List<DAL.Table> ListOrderedTables = new List<DAL.Table>();

        public ChangeTable()
        {
            InitializeComponent();
        }

        public ChangeTable(DAL.Order order, List<DAL.Table> tables)
        {
            InitializeComponent();
            this.order = order;
            this.Tables = tables;
            this.LoadData();
        }

        private void LoadData()
        {
            TableBLL tableBLL = new TableBLL();
            this.ListOrderedTables = tableBLL.ListOrderedTables();
            this.ListAvailableTables = tableBLL.ListAvailableTables();

            foreach(DAL.Table table in this.Tables)
            {
                TableControl tableControl = new TableControl(table, false);
                tableControl.MouseDown += new MouseEventHandler(this.tableControl1_MouseDown);
                this.flowLayoutPanel1.Controls.Add(tableControl);
            }

            foreach (DAL.Table table in this.ListAvailableTables)
            {
                TableControl tableControl = new TableControl(table, false);
                tableControl.MouseDown += new MouseEventHandler(this.tableControl1_MouseDown);
                this.flowLayoutPanel2.Controls.Add(tableControl);
            }

            foreach (DAL.Table table in this.ListOrderedTables)
            {
                DAL.Table tt = this.Tables.Find(t => t.ID == table.ID);
                if (tt != null)
                    continue;

                TableControl tableControl = new TableControl(table, false);
                tableControl.MouseDown += new MouseEventHandler(this.tableControl1_MouseDown);
                this.flowLayoutPanel3.Controls.Add(tableControl);
            }
        }

        private void tableControl1_MouseDown(object sender, MouseEventArgs e)
        {
            TableControl tableControl = (TableControl)sender;
            tableControl.Select();
            DataObject data = new DataObject(DataFormats.Serializable, tableControl.Table);
            tableControl.DoDragDrop(data, DragDropEffects.Copy);
        }

        private void flowLayoutPanel2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void flowLayoutPanel3_DragDrop(object sender, DragEventArgs e)
        {
            DAL.Table data = (DAL.Table)e.Data.GetData(DataFormats.Serializable);

            // check table status is != 1
            if (data.Status != 1)
                return;

            // check exist in layout 3
            List<TableControl> layout3controls = this.flowLayoutPanel3.Controls.OfType<TableControl>().ToList();
            var find = layout3controls.Find(t => t.Table.ID == data.ID);
            if (find != null)
                return;

            // remove from layout 1
            this.Tables.Remove(data);
            List<TableControl> controls = this.flowLayoutPanel1.Controls.OfType<TableControl>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].Table.ID == data.ID)
                {
                    this.flowLayoutPanel1.Controls.RemoveAt(i);
                    break;
                }
            }

            // add to layout 3
            TableControl tableControl = new TableControl(data, false);
            tableControl.MouseDown += new MouseEventHandler(this.tableControl1_MouseDown);
            this.flowLayoutPanel3.Controls.Add(tableControl);
        }

        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            DAL.Table data = (DAL.Table)e.Data.GetData(DataFormats.Serializable);

            // check table status is != 0
            if (data.Status != 0)
                return;

            // check exist in layout 2
            List<TableControl> layout2controls = this.flowLayoutPanel2.Controls.OfType<TableControl>().ToList();
            var find = layout2controls.Find(t => t.Table.ID == data.ID);
            if (find != null)
                return;

            // remove from layout 1
            this.Tables.Remove(data);
            List<TableControl> controls = this.flowLayoutPanel1.Controls.OfType<TableControl>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].Table.ID == data.ID)
                {
                    this.flowLayoutPanel1.Controls.RemoveAt(i);
                    break;
                }
            }

            // add to layout 2
            TableControl tableControl = new TableControl(data, false);
            tableControl.MouseDown += new MouseEventHandler(this.tableControl1_MouseDown);
            this.flowLayoutPanel2.Controls.Add(tableControl);
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            DAL.Table data = (DAL.Table)e.Data.GetData(DataFormats.Serializable);

            // check exist in layout 1
            var find = this.Tables.Find(t => t.ID == data.ID);
            if (find != null)
                return;

            this.Tables.Add(data);

            // remove from layout 2
            List<TableControl> controls = this.flowLayoutPanel2.Controls.OfType<TableControl>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].Table.ID == data.ID)
                {
                    this.flowLayoutPanel2.Controls.RemoveAt(i);
                    break;
                }
            }

            // remove from layout 3
            controls = this.flowLayoutPanel3.Controls.OfType<TableControl>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].Table.ID == data.ID)
                {
                    this.flowLayoutPanel3.Controls.RemoveAt(i);
                    break;
                }
            }

            // add to layout 1
            TableControl tableControl = new TableControl(data, false);
            tableControl.MouseDown += new MouseEventHandler(this.tableControl1_MouseDown);
            this.flowLayoutPanel1.Controls.Add(tableControl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OrderBLL orderBLL = new OrderBLL();
            orderBLL.ChangeTable(this.order, this.Tables);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
