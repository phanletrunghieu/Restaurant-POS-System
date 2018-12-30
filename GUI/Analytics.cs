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
using Excel = Microsoft.Office.Interop.Excel;

namespace GUI
{
    public partial class Analytics : Form
    {
        private int monthOrYear = 0;
        List<BLL.ReportType> reports;

        public Analytics()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            reports = new List<ReportType>();
            this.LoadData();
        }

        private void LoadData()
        {
            AnalyticsBLL analyticsBLL = new AnalyticsBLL();
            if (this.monthOrYear == 0)
            {
                this.reports = analyticsBLL.GetAnalyticsByMonth(this.dateTimePicker1.Value.Month, this.dateTimePicker1.Value.Year);
            }
            else
            {
                this.reports = analyticsBLL.GetAnalyticsByYear(this.dateTimePicker1.Value.Year);
            }
            this.chart1.DataSource = reports;
            this.chart1.DataBind();

            this.dataGridView1.DataSource = reports;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (this.monthOrYear == 0)
                return;

            this.monthOrYear = 0;
            this.LoadData();
        }

        private void rbYear_CheckedChanged(object sender, EventArgs e)
        {
            if (this.monthOrYear == 1)
                return;

            this.monthOrYear = 1;
            this.LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.ShowDialog();
            if (this.saveFileDialog.FileName != "")
            {
                Excel.Application application = new Excel.Application();
                Excel.Workbook wb = application.Workbooks.Add(Type.Missing);
                try
                {
                    Excel.Worksheet sheet = wb.ActiveSheet;
                    sheet.Name = "Report";

                    sheet.Cells[1, 1].Value = "Date";
                    sheet.Cells[1, 2].Value = "Total Revenue";
                    sheet.Cells[1, 3].Value = "Number of order";

                    for (int i = 0; i < this.reports.Count; i++)
                    {
                        sheet.Cells[i + 2, 1].Value = this.reports[i].DateCreated;
                        sheet.Cells[i + 2, 2].Value = this.reports[i].TotalRevenue;
                        sheet.Cells[i + 2, 3].Value = this.reports[i].NumOrder;
                    }
                    wb.SaveAs(this.saveFileDialog.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    application.Quit();
                    wb = null;
                }
            }
        }
    }
}
