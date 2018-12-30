using BLL;
using DAL;
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
    public partial class TablesStatus : Form
    {
        public TablesStatus()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            this.LoadData();
        }

        private void LoadData()
        {
            AreaBLL areaBLL = new AreaBLL();
            List<DAL.Area> areas = areaBLL.ListArea();

            this.tabControl.Controls.Clear();

            foreach (DAL.Area area in areas)
            {
                var t = new TabPage();
                t.Location = new Point(4, 22);
                t.Name = area.Name;
                t.Padding = new Padding(3);
                t.Size = new Size(597, 257);
                t.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                t.Text = area.Name;
                t.UseVisualStyleBackColor = true;
                t.AutoScroll = true;
                this.tabControl.Controls.Add(t);

                // add layout
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Dock = DockStyle.Fill;
                flowLayoutPanel.AutoScroll = true;
                t.Controls.Add(flowLayoutPanel);

                // add menu item
                TableBLL tableBLL = new TableBLL();
                List<DAL.Table> tables = tableBLL.ListTablesByArea(area);
                for (int i = 0; i < tables.Count; i++)
                {
                    TableControl tableControl = new TableControl(tables[i], true);
                    tableControl.Tag = area;
                    flowLayoutPanel.Controls.Add(tableControl);
                    tableControl.Click += new EventHandler(this.tableControl_Click);
                }
            }
        }

        private void tableControl_Click(object sender, EventArgs e)
        {
            TableControl tableControl = (TableControl)sender;
            Area area = (Area)tableControl.Tag;
            CreateOrder createOrder = new CreateOrder(tableControl.Table, area);
            DialogResult dr = createOrder.ShowDialog();

            //update table status
            if(dr == DialogResult.OK || dr == DialogResult.Yes)
            {
                var tps = this.tabControl.Controls.OfType<TabPage>().ToList();
                foreach (TabPage tp in tps)
                {
                    var layout = tp.Controls.OfType<FlowLayoutPanel>().ToList()[0];
                    var tcs = layout.Controls.OfType<TableControl>();
                    foreach (TableControl tc in tcs)
                    {
                        // de-highlight old table
                        var tt = createOrder.OldTables.Find(t => t.ID == tc.Table.ID);
                        if (tt != null)
                        {
                            var tb = tc.Table;
                            tb.Status = 0;
                            tc.Table = tb;
                        }

                        // highlight old table
                        // DialogResult.Yes => order had finish
                        if (dr != DialogResult.Yes)
                        {
                            tt = createOrder.Tables.Find(t => t.ID == tc.Table.ID);
                            if (tt != null)
                            {
                                var tb = tc.Table;
                                tb.Status = 1;
                                tc.Table = tb;
                            }
                        }
                    }
                }
            }
        }
    }
}
