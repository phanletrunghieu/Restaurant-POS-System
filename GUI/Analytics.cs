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

namespace GUI
{
    public partial class Analytics : Form
    {
        public Analytics()
        {
            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            AnalyticsBLL analyticsBLL = new AnalyticsBLL();
            List<BLL.ReportType> reports = analyticsBLL.GetAnalyticsByMonth(this.dateTimePicker1.Value.Month, this.dateTimePicker1.Value.Year);
            this.chart1.DataSource = reports;
            this.chart1.DataBind();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}
