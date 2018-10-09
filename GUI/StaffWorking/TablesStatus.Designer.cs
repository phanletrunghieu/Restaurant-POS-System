using GUI.Control;

namespace GUI.StaffWorking
{
    partial class TablesStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DAL.Table table1 = new DAL.Table();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TablesStatus));
            this.tableControl1 = new TableControl();
            this.SuspendLayout();
            // 
            // tableControl1
            // 
            this.tableControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableControl1.Location = new System.Drawing.Point(12, 12);
            this.tableControl1.Name = "tableControl1";
            this.tableControl1.Size = new System.Drawing.Size(142, 75);
            this.tableControl1.TabIndex = 0;
            table1.Area = null;
            table1.AreaID = null;
            table1.ID = 0;
            table1.Name = "Bàn 1";
            table1.Status = 0;
            this.tableControl1.Table = table1;
            this.tableControl1.UseBkColorForStatus = true;
            this.tableControl1.Click += new System.EventHandler(this.tableControl1_Click);
            // 
            // TablesStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 342);
            this.Controls.Add(this.tableControl1);
            this.Name = "TablesStatus";
            this.Text = "TablesStatus";
            this.ResumeLayout(false);

        }

        #endregion

        private TableControl tableControl1;
    }
}