namespace GUI.SystemSetup.Seat
{
    partial class AreaAddDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.cbAddTable = new System.Windows.Forms.CheckBox();
            this.panelTable = new System.Windows.Forms.Panel();
            this.txtTableTo = new System.Windows.Forms.NumericUpDown();
            this.txtTableFrom = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add a new area";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Area name";
            // 
            // txtAreaName
            // 
            this.txtAreaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAreaName.Location = new System.Drawing.Point(15, 77);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(257, 20);
            this.txtAreaName.TabIndex = 2;
            // 
            // cbAddTable
            // 
            this.cbAddTable.AutoSize = true;
            this.cbAddTable.Location = new System.Drawing.Point(15, 115);
            this.cbAddTable.Name = "cbAddTable";
            this.cbAddTable.Size = new System.Drawing.Size(76, 17);
            this.cbAddTable.TabIndex = 3;
            this.cbAddTable.Text = "Add tables";
            this.cbAddTable.UseVisualStyleBackColor = true;
            this.cbAddTable.CheckedChanged += new System.EventHandler(this.cbAddTable_CheckedChanged);
            // 
            // panelTable
            // 
            this.panelTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTable.Controls.Add(this.txtTableTo);
            this.panelTable.Controls.Add(this.txtTableFrom);
            this.panelTable.Controls.Add(this.label4);
            this.panelTable.Controls.Add(this.label3);
            this.panelTable.Enabled = false;
            this.panelTable.Location = new System.Drawing.Point(15, 139);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(257, 46);
            this.panelTable.TabIndex = 4;
            // 
            // txtTableTo
            // 
            this.txtTableTo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTableTo.Location = new System.Drawing.Point(148, 16);
            this.txtTableTo.Maximum = new decimal(new int[] {
            -1304428545,
            434162106,
            542,
            0});
            this.txtTableTo.Name = "txtTableTo";
            this.txtTableTo.Size = new System.Drawing.Size(107, 20);
            this.txtTableTo.TabIndex = 4;
            // 
            // txtTableFrom
            // 
            this.txtTableFrom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTableFrom.Location = new System.Drawing.Point(2, 16);
            this.txtTableFrom.Maximum = new decimal(new int[] {
            -1304428545,
            434162106,
            542,
            0});
            this.txtTableFrom.Name = "txtTableFrom";
            this.txtTableFrom.Size = new System.Drawing.Size(107, 20);
            this.txtTableFrom.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Table to";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Table from";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddAreaDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 226);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.cbAddTable);
            this.Controls.Add(this.txtAreaName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAreaDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add a new area";
            this.panelTable.ResumeLayout(false);
            this.panelTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.CheckBox cbAddTable;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.NumericUpDown txtTableFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtTableTo;
        private System.Windows.Forms.Button button1;
    }
}