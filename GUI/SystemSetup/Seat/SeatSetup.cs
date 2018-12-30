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

namespace GUI.SystemSetup.Seat
{
    public partial class SeatSetup : Form
    {
        private List<System.Windows.Forms.TabPage> tabPages;

        public SeatSetup()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            this.LoadData();
        }

        public void LoadData()
        {
            int currIndex = this.tabControl.SelectedIndex;
            this.tabControl.Controls.Clear();

            AreaBLL areaBLL = new AreaBLL();
            List<Area> listArea = areaBLL.ListArea();
            this.tabPages = new List<System.Windows.Forms.TabPage>();
            foreach (Area area in listArea)
            {
                var t = new System.Windows.Forms.TabPage();
                t.Location = new System.Drawing.Point(4, 22);
                t.Name = area.Name;
                t.Padding = new System.Windows.Forms.Padding(3);
                t.Size = new System.Drawing.Size(597, 257);
                t.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                t.Text = area.Name;
                t.UseVisualStyleBackColor = true;
                t.AutoScroll = true;

                // button add table
                Button btnAddTable = new Button();
                btnAddTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                btnAddTable.Location = new System.Drawing.Point(6, 6);
                btnAddTable.Size = new System.Drawing.Size(75, 20);
                btnAddTable.Tag = area;
                btnAddTable.Text = "Add table";
                btnAddTable.Click += new EventHandler(this.btnAddTable_Click);
                t.Controls.Add(btnAddTable);

                // button update area
                Button btnUpdateArea= new Button();
                btnUpdateArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                btnUpdateArea.Location = new System.Drawing.Point(6, 29);
                btnUpdateArea.Size = new System.Drawing.Size(75, 20);
                btnUpdateArea.Tag = area;
                btnUpdateArea.Text = "Update area";
                btnUpdateArea.Click += new EventHandler(this.btnUpdateArea_Click);
                t.Controls.Add(btnUpdateArea);

                // button delete area
                Button btnDelArea = new Button();
                btnDelArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                btnDelArea.Location = new System.Drawing.Point(6, 61);
                btnDelArea.Size = new System.Drawing.Size(75, 20);
                btnDelArea.Tag = area;
                btnDelArea.Text = "Delete area";
                btnDelArea.Click += new EventHandler(this.btnDeleteArea_Click);
                t.Controls.Add(btnDelArea);

                // add tables
                TableBLL tableBLL = new TableBLL();
                List<Table> listTable = tableBLL.ListTablesByArea(area);
                for (int i = 0; i < listTable.Count; i++)
                {
                    var tt = new TableControl(listTable[i], false);
                    tt.OnEdit += new TableControl.OnEditHandler(this.tableControl_OnEdit);
                    tt.OnDelete += new TableControl.OnDeleteHandler(this.tableControl_OnDelete);
                    t.Controls.Add(tt);
                }

                // store
                this.tabPages.Add(t);
                this.tabControl.Controls.Add(t);
            }
            this.tabControl.SelectedIndex = Math.Min(listArea.Count, currIndex);
        
            this.UpdateControlPosition();
        }

        private void UpdateControlPosition()
        {
            int tableWidth = 150;
            int tableHeight = 70;
            int minPadding = 6;
            int contentPadding = 90;

            for (int i = 0; i < this.tabPages.Count; i++)
            {
                List<TableControl> controls = this.tabPages[i].Controls.OfType<TableControl>().ToList();

                int cols = (this.tabPages[i].Width - minPadding) / (tableWidth + minPadding);
                int rows = (this.tabPages[i].Height - minPadding) / (tableHeight + minPadding);

                // calculate paddingHorizontal
                int containerWidth = (tableWidth + minPadding) * Math.Min(cols, controls.Count) - minPadding;
                int paddingHorizontal = (this.tabPages[i].Width - containerWidth) / 2;
                paddingHorizontal = Math.Max(paddingHorizontal, contentPadding);


                for (int j = 0; j < controls.Count; j++)
                {
                    int x = j % cols;
                    int y = j / cols;
                    controls[j].Size = new System.Drawing.Size(tableWidth, tableHeight);
                    controls[j].Location = new System.Drawing.Point(paddingHorizontal + (tableWidth + minPadding) * x, minPadding + (tableHeight + minPadding) * y);
                }
            }
        }

        private void SeatSetup_Resize(object sender, EventArgs e)
        {
            this.UpdateControlPosition();
        }

        private void btnAddArea_Click(object sender, EventArgs e)
        {
            AreaAddDialog addAreaDialog = new AreaAddDialog();
            DialogResult dr=addAreaDialog.ShowDialog();
            if(dr==DialogResult.OK)
            {
                AreaBLL areaBLL = new AreaBLL();
                Area area = areaBLL.CreateArea(addAreaDialog.AreaName);

                // add table
                if (addAreaDialog.IsAddTable)
                {
                    TableBLL tableBLL = new TableBLL();
                    for (var i = addAreaDialog.TableFrom; i <= addAreaDialog.TableTo; i++)
                    {
                        tableBLL.CreateTable(new Table { Name = "Bàn " + i, AreaID = area.ID });
                    }
                }

                this.LoadData();
            }
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            this.UpdateControlPosition();
        }

        private void tableControl_OnDelete(Table table)
        {
            for(int i = 0; i < this.tabPages.Count; i++)
            {
                List<TableControl> controls = this.tabPages[i].Controls.OfType<TableControl>().ToList();
                for (int j = 0; j < controls.Count; j++)
                {
                    if(controls[j].Table.ID == table.ID)
                    {
                        this.tabPages[i].Controls.RemoveAt(j);
                        this.UpdateControlPosition();
                    }
                }
            }
        }

        private void tableControl_OnEdit(TableControl sender)
        {
            TableEditDialog tableEditDialog = new TableEditDialog(sender.Table);
            DialogResult dr = tableEditDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                sender.Table = tableEditDialog.table;
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Area area = (Area)btn.Tag;
            DialogResult dr = new TableAddDialog(area).ShowDialog();
            if(dr==DialogResult.OK)
            {
                this.LoadData();
            }
        }

        private void btnDeleteArea_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Area area = (Area)btn.Tag;
            DialogResult dr=MessageBox.Show("Are you sure to delete area '" + area.Name + "'?", "Confirm", MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                AreaBLL areaBLL = new AreaBLL();
                areaBLL.Delete(area);
                this.LoadData();
            }
        }

        private void btnUpdateArea_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Area area = (Area)btn.Tag;

            AreaUpdateDialog areaUpdateDialog = new AreaUpdateDialog(area);
            DialogResult dr = areaUpdateDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }
    }
}
