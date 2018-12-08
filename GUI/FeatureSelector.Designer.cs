namespace GUI
{
    partial class FeatureSelector
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
            this.btnSystemSetup = new System.Windows.Forms.Button();
            this.btnWork = new System.Windows.Forms.Button();
            this.btnAnalytics = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSystemSetup
            // 
            this.btnSystemSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSystemSetup.Location = new System.Drawing.Point(40, 82);
            this.btnSystemSetup.Name = "btnSystemSetup";
            this.btnSystemSetup.Size = new System.Drawing.Size(100, 100);
            this.btnSystemSetup.TabIndex = 0;
            this.btnSystemSetup.Text = "System setup";
            this.btnSystemSetup.UseVisualStyleBackColor = true;
            this.btnSystemSetup.Click += new System.EventHandler(this.btnSystemSetup_Click);
            // 
            // btnWork
            // 
            this.btnWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWork.Location = new System.Drawing.Point(187, 82);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(100, 100);
            this.btnWork.TabIndex = 0;
            this.btnWork.Text = "Work";
            this.btnWork.UseVisualStyleBackColor = true;
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // btnAnalytics
            // 
            this.btnAnalytics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalytics.Location = new System.Drawing.Point(331, 82);
            this.btnAnalytics.Name = "btnAnalytics";
            this.btnAnalytics.Size = new System.Drawing.Size(100, 100);
            this.btnAnalytics.TabIndex = 1;
            this.btnAnalytics.Text = "Analytics";
            this.btnAnalytics.UseVisualStyleBackColor = true;
            this.btnAnalytics.Click += new System.EventHandler(this.btnAnalytics_Click);
            // 
            // FeatureSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 248);
            this.Controls.Add(this.btnAnalytics);
            this.Controls.Add(this.btnWork);
            this.Controls.Add(this.btnSystemSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatureSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FeatureSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSystemSetup;
        private System.Windows.Forms.Button btnWork;
        private System.Windows.Forms.Button btnAnalytics;
    }
}