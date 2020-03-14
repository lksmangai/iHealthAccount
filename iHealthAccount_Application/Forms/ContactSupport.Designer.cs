namespace iHealthAccount.Application.Forms
{
    partial class ContactSupport
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelRedirectURL = new System.Windows.Forms.LinkLabel();
            this.radioButtonSuggestion = new System.Windows.Forms.RadioButton();
            this.radioButtonQuery = new System.Windows.Forms.RadioButton();
            this.radioButtonRequest = new System.Windows.Forms.RadioButton();
            this.radioButtonBug = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelMessage = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabelRedirectURL);
            this.groupBox1.Controls.Add(this.radioButtonSuggestion);
            this.groupBox1.Controls.Add(this.radioButtonQuery);
            this.groupBox1.Controls.Add(this.radioButtonRequest);
            this.groupBox1.Controls.Add(this.radioButtonBug);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(408, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Regarding";
            // 
            // linkLabelRedirectURL
            // 
            this.linkLabelRedirectURL.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkLabelRedirectURL.AutoSize = true;
            this.linkLabelRedirectURL.DisabledLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkLabelRedirectURL.LinkColor = System.Drawing.SystemColors.Highlight;
            this.linkLabelRedirectURL.Location = new System.Drawing.Point(19, 91);
            this.linkLabelRedirectURL.Name = "linkLabelRedirectURL";
            this.linkLabelRedirectURL.Size = new System.Drawing.Size(190, 17);
            this.linkLabelRedirectURL.TabIndex = 40;
            this.linkLabelRedirectURL.TabStop = true;
            this.linkLabelRedirectURL.Text = "Click here to go to support site";
            this.linkLabelRedirectURL.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabelRedirectURL.LocationChanged += new System.EventHandler(this.linkLabelRedirectURL_LocationChanged);
            // 
            // radioButtonSuggestion
            // 
            this.radioButtonSuggestion.AutoSize = true;
            this.radioButtonSuggestion.Location = new System.Drawing.Point(148, 66);
            this.radioButtonSuggestion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonSuggestion.Name = "radioButtonSuggestion";
            this.radioButtonSuggestion.Size = new System.Drawing.Size(92, 21);
            this.radioButtonSuggestion.TabIndex = 12;
            this.radioButtonSuggestion.Text = "Suggestion";
            this.radioButtonSuggestion.UseVisualStyleBackColor = true;
            this.radioButtonSuggestion.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // radioButtonQuery
            // 
            this.radioButtonQuery.AutoSize = true;
            this.radioButtonQuery.Location = new System.Drawing.Point(148, 37);
            this.radioButtonQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonQuery.Name = "radioButtonQuery";
            this.radioButtonQuery.Size = new System.Drawing.Size(61, 21);
            this.radioButtonQuery.TabIndex = 11;
            this.radioButtonQuery.Text = "Query";
            this.radioButtonQuery.UseVisualStyleBackColor = true;
            this.radioButtonQuery.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // radioButtonRequest
            // 
            this.radioButtonRequest.AutoSize = true;
            this.radioButtonRequest.Location = new System.Drawing.Point(19, 66);
            this.radioButtonRequest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonRequest.Name = "radioButtonRequest";
            this.radioButtonRequest.Size = new System.Drawing.Size(123, 21);
            this.radioButtonRequest.TabIndex = 10;
            this.radioButtonRequest.Text = "Feature Request";
            this.radioButtonRequest.UseVisualStyleBackColor = true;
            this.radioButtonRequest.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // radioButtonBug
            // 
            this.radioButtonBug.AutoSize = true;
            this.radioButtonBug.Checked = true;
            this.radioButtonBug.Location = new System.Drawing.Point(19, 37);
            this.radioButtonBug.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonBug.Name = "radioButtonBug";
            this.radioButtonBug.Size = new System.Drawing.Size(91, 21);
            this.radioButtonBug.TabIndex = 9;
            this.radioButtonBug.TabStop = true;
            this.radioButtonBug.Text = "Report Bug";
            this.radioButtonBug.UseVisualStyleBackColor = true;
            this.radioButtonBug.CheckedChanged += new System.EventHandler(this.Options_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(395, 29);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 83);
            this.panel4.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 29);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 83);
            this.panel3.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 112);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 10);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 10);
            this.panel1.TabIndex = 5;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(14, 175);
            this.textBoxMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessage.Size = new System.Drawing.Size(408, 128);
            this.textBoxMessage.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(347, 319);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 25);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(266, 319);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 25);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Subject:";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSubject.Location = new System.Drawing.Point(77, 146);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.ReadOnly = true;
            this.textBoxSubject.Size = new System.Drawing.Size(345, 22);
            this.textBoxSubject.TabIndex = 5;
            this.textBoxSubject.Text = "iHealthAccount: Bug Report";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(14, 323);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 17);
            this.labelMessage.TabIndex = 6;
            // 
            // ContactAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 356);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContactAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ContactAdmin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.RadioButton radioButtonSuggestion;
        private System.Windows.Forms.RadioButton radioButtonQuery;
        private System.Windows.Forms.RadioButton radioButtonRequest;
        private System.Windows.Forms.RadioButton radioButtonBug;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabelRedirectURL;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelMessage;
    }
}