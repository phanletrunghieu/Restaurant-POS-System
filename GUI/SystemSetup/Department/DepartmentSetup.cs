using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using GUI.Control;

namespace GUI.SystemSetup.Department
{
    public partial class DepartmentSetup : Form
    {
        List<DataGridView> listDataGridView = new List<DataGridView>();
        public DepartmentSetup()
        {
            InitializeComponent();
            this.Text = Utilities.UtilsForm.GetFormTitle(this.Text);
            this.LoadData();
        }

        public void LoadData()
        {
            this.tabControl.Controls.Clear();
            listDataGridView.Clear();

            DepartmentBLL departmentBLL = new DepartmentBLL();
            List<DAL.Department> listDepartment = departmentBLL.ListDepartment();
            for (int i = 0; i < listDepartment.Count; i++)
            {
                var t = new System.Windows.Forms.TabPage();
                t.Name = listDepartment[i].Name;
                t.Text = listDepartment[i].Name;
                t.Location = new System.Drawing.Point(4, 22);
                t.Padding = new System.Windows.Forms.Padding(3);
                t.Size = new System.Drawing.Size(597, 257);
                t.UseVisualStyleBackColor = true;
                t.AutoScroll = true;

                //add gridview
                var dataGridView = new System.Windows.Forms.DataGridView();
                var bindingSource = new System.Windows.Forms.BindingSource();
                ((System.ComponentModel.ISupportInitialize)(dataGridView)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(bindingSource)).BeginInit();
                dataGridView.AutoGenerateColumns = false;
                dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView.DataSource = bindingSource;
                dataGridView.Location = new System.Drawing.Point(0, 0);
                dataGridView.Size = new System.Drawing.Size(567, 228);

                var col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                var col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                var col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                col1.DataPropertyName = "ID";
                col1.HeaderText = "ID";
                col1.Width = 174;
                col2.DataPropertyName = "Name";
                col2.HeaderText = "Name";
                col2.Width = 174;
                col3.DataPropertyName = "Username";
                col3.HeaderText = "Username";
                col3.Width = 176;
                dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { col1 , col2 , col3 });

                //add list data gridview

                listDataGridView.Add(dataGridView);

                // add addbutton
                Button button = new Button();
                button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                button.Location = new System.Drawing.Point(490, 7);
                button.Size = new System.Drawing.Size(100, 20);
                button.Tag = listDepartment[i];
                button.Text = "Add emloyee";
                button.Click += new EventHandler(this.btnAddEmloyee_Click);

                t.Controls.Add(button);

                // add button edit department

                Button btEditDepartment = new Button();
                btEditDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                btEditDepartment.Location = new System.Drawing.Point(490, 30);
                btEditDepartment.Size = new System.Drawing.Size(100, 20);
                btEditDepartment.Tag = listDepartment[i];
                btEditDepartment.Text = "Edit Department";
                btEditDepartment.Click += new EventHandler(this.btEditDepartment_Click);

                t.Controls.Add(btEditDepartment);

                // add button delete department

                if(listDepartment[i].ID!=1 && listDepartment[i].ID != 2)
                {
                    Button btDeleteDepartment = new Button();
                    btDeleteDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                    btDeleteDepartment.Location = new System.Drawing.Point(480, 55);
                    btDeleteDepartment.Size = new System.Drawing.Size(120, 20);
                    btDeleteDepartment.Tag = listDepartment[i];
                    btDeleteDepartment.Text = "Delete Department";
                    btDeleteDepartment.Click += new EventHandler(this.btDeleteDepartment_Click);

                    t.Controls.Add(btDeleteDepartment);
                }

                //get emloyees for department 

                EmployeeBLL employeeBLL = new EmployeeBLL();
                List<Employee> listEmployee = employeeBLL.ListEmployeeByDepartment(listDepartment[i]);
                dataGridView.DataSource = listEmployee;

                t.Controls.Add(dataGridView);
                //var dmc = new DepartmentControl(listDepartment[i]);
                //dmc.OnDelete += new DepartmentControl.OnDeleteHandler(this.departmentControl_OnDelete);
                //int cols = (this.Width - minPadding) / (width + minPadding);
                //int rows = (this.Height - minPadding) / (height + minPadding);
                //dmc.Location = new System.Drawing.Point(minPadding + (width + minPadding) , minPadding + (height + minPadding) );
                //dmc.Size = new System.Drawing.Size(width, height);
                //t.Controls.Add(dmc);

                this.tabControl.Controls.Add(t);
            }
            this.UpdateControlPosition();
        }

        private void btEditDepartment_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DAL.Department department = (DAL.Department)btn.Tag;
            this.EditDepartment(department);
        }

        private void btDeleteDepartment_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DAL.Department department = (DAL.Department)btn.Tag;
            this.DeleteDepartment(department);
        }

        private void btnAddEmloyee_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DAL.Department department = (DAL.Department)btn.Tag;
            new EmloyeeAddDialog(department).ShowDialog();
            this.LoadData();
        }

      
        private void UpdateControlPosition()
        {
            int tableWidth = 150;
            int tableHeight = 70;
            int minPadding = 6;

            List<TableControl> controls = this.Controls.OfType<TableControl>().ToList();

            int cols = (this.Width - minPadding) / (tableWidth + minPadding);
            int rows = (this.Height - minPadding) / (tableHeight + minPadding);

            // calculate paddingHorizontal
            int containerWidth = (tableWidth + minPadding) * Math.Min(cols, controls.Count) - minPadding;
            int paddingHorizontal = (this.Width - containerWidth) / 2;

            for (int j = 0; j < controls.Count; j++)
            {
                int x = j % cols;
                int y = j / cols;
                controls[j].Size = new System.Drawing.Size(tableWidth, tableHeight);
                controls[j].Location = new System.Drawing.Point(paddingHorizontal + (tableWidth + minPadding) * x, minPadding + (tableHeight + minPadding) * y);
                }
        }

        private void DepartmentSetup_Resize(object sender, EventArgs e)
        {
            this.UpdateControlPosition();
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            DepartmentAddDialog departmentAddDialog = new DepartmentAddDialog();
            DialogResult dr = departmentAddDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            this.UpdateControlPosition();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.listDataGridView[this.tabControl.SelectedIndex];
            int index = dgv.SelectedRows[0].Index;
            List<Employee> listEmp = (List<Employee>)dgv.DataSource;

            if (MessageBox.Show("Are you sure to delete this emloyee \"" + listEmp[index].Name + "\"", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EmployeeBLL employeeBLL = new EmployeeBLL();
                employeeBLL.Delete(listEmp[index]);
                this.LoadData();

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.listDataGridView[this.tabControl.SelectedIndex];
            int index = dgv.SelectedRows[0].Index;
            List<Employee> listEmp = (List<Employee>)dgv.DataSource;

            EmloyeeEditDialog emloyeeEditDialog = new EmloyeeEditDialog(listEmp[index]);
            DialogResult dr = emloyeeEditDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }

        private void editPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.listDataGridView[this.tabControl.SelectedIndex];
            int index = dgv.SelectedRows[0].Index;
            List<Employee> listEmp = (List<Employee>)dgv.DataSource;

            PassworkEditDialog passworkEditDialog = new PassworkEditDialog(listEmp[index]);
            DialogResult dr = passworkEditDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {

            }
        }
        private void DeleteDepartment(DAL.Department department)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete department \"" + department.Name + "\"", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                EmloyeeDepartmentBLL emloyeeDepartmentBLL = new EmloyeeDepartmentBLL();
                EmployeeBLL employeeBLL = new EmployeeBLL();
                employeeBLL.DeleteByDepartment(emloyeeDepartmentBLL.ListEmployeeDepartmentByDepartment(department));
                DepartmentBLL departmentBLL = new DepartmentBLL();
                departmentBLL.DeleteDepartment(department);
                this.LoadData();
            }
        }

         private void EditDepartment(DAL.Department department)
        {
            DepartmentEditDialog departmentEditDialog = new DepartmentEditDialog(department);
            DialogResult dr = departmentEditDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.LoadData();
            }
        }
    }
}
