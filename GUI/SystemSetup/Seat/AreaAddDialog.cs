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
    public partial class AreaAddDialog : Form
    {
        public string AreaName;
        public bool IsAddTable;
        public decimal TableFrom;
        public decimal TableTo;

        public AreaAddDialog()
        {
            InitializeComponent();
        }

        private void cbAddTable_CheckedChanged(object sender, EventArgs e)
        {
            this.panelTable.Enabled = this.cbAddTable.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAreaName.Text == "")
            {
                MessageBox.Show("Please fill the area name.");
                return;
            }

            this.AreaName = txtAreaName.Text;

            if (this.cbAddTable.Checked)
            {
                if(Math.Round(txtTableFrom.Value) >= Math.Round(txtTableTo.Value))
                {
                    MessageBox.Show("\"Table from\" must be lower than \"Table to\"");
                    return;
                }

                this.IsAddTable = this.cbAddTable.Checked;
                this.TableFrom = Math.Round(txtTableFrom.Value);
                this.TableTo = Math.Round(txtTableTo.Value);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
