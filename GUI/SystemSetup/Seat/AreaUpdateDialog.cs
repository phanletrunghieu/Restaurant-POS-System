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

namespace GUI.SystemSetup.Seat
{
    public partial class AreaUpdateDialog : Form
    {
        public DAL.Area area;

        public AreaUpdateDialog()
        {
            InitializeComponent();
        }

        public AreaUpdateDialog(DAL.Area area)
        {
            InitializeComponent();
            this.area = area;
            this.LoadData();
        }

        private void LoadData()
        {
            this.txtName.Text = this.area.Name;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void save()
        {
            this.area.Name = this.txtName.Text;
            AreaBLL areaBLL = new AreaBLL();
            areaBLL.Update(this.area);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.save();
            }
        }
    }
}
