namespace iHealthAccount.Application.Forms
{
    partial class Settings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxWriteLog = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteTrace = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxWriteLog);
            this.groupBox2.Controls.Add(this.checkBoxWriteTrace);
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 86);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logging";
            // 
            // checkBoxWriteLog
            // 
            this.checkBoxWriteLog.AutoSize = true;
            this.checkBoxWriteLog.Checked = true;
            this.checkBoxWriteLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWriteLog.Location = new System.Drawing.Point(6, 48);
            this.checkBoxWriteLog.Name = "checkBoxWriteLog";
            this.checkBoxWriteLog.Size = new System.Drawing.Size(85, 21);
            this.checkBoxWriteLog.TabIndex = 1;
            this.checkBoxWriteLog.Text = "Write Log";
            this.checkBoxWriteLog.UseVisualStyleBackColor = true;
            this.checkBoxWriteLog.CheckedChanged += new System.EventHandler(this.checkBoxWriteLog_CheckedChanged);
            // 
            // checkBoxWriteTrace
            // 
            this.checkBoxWriteTrace.AutoSize = true;
            this.checkBoxWriteTrace.Checked = true;
            this.checkBoxWriteTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWriteTrace.Location = new System.Drawing.Point(6, 21);
            this.checkBoxWriteTrace.Name = "checkBoxWriteTrace";
            this.checkBoxWriteTrace.Size = new System.Drawing.Size(96, 21);
            this.checkBoxWriteTrace.TabIndex = 0;
            this.checkBoxWriteTrace.Text = "Write Trace";
            this.checkBoxWriteTrace.UseVisualStyleBackColor = true;
            this.checkBoxWriteTrace.CheckedChanged += new System.EventHandler(this.checkBoxWriteTrace_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(294, 221);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxWriteLog;
        private System.Windows.Forms.CheckBox checkBoxWriteTrace;
    }
}