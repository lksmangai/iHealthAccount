namespace iHealthAccount.Application.Forms
{
    partial class AboutApp
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
            this.panelDetails = new System.Windows.Forms.Panel();
            this.linkLabelRedirectURL = new System.Windows.Forms.LinkLabel();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelVersion1 = new System.Windows.Forms.Label();
            this.labelAppDescription1 = new System.Windows.Forms.Label();
            this.labelProductName1 = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCopyRight = new System.Windows.Forms.Label();
            this.panelDetails.SuspendLayout();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDetails
            // 
            this.panelDetails.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelDetails.Controls.Add(this.linkLabelRedirectURL);
            this.panelDetails.Controls.Add(this.panelInfo);
            this.panelDetails.Controls.Add(this.buttonOk);
            this.panelDetails.Controls.Add(this.labelVersion);
            this.panelDetails.Controls.Add(this.labelCompanyName);
            this.panelDetails.Controls.Add(this.textBoxDescription);
            this.panelDetails.Controls.Add(this.labelProductName);
            this.panelDetails.Controls.Add(this.labelCopyRight);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelDetails.Location = new System.Drawing.Point(0, 0);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(384, 271);
            this.panelDetails.TabIndex = 28;
            // 
            // linkLabelRedirectURL
            // 
            this.linkLabelRedirectURL.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkLabelRedirectURL.AutoSize = true;
            this.linkLabelRedirectURL.DisabledLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkLabelRedirectURL.LinkColor = System.Drawing.Color.White;
            this.linkLabelRedirectURL.Location = new System.Drawing.Point(12, 210);
            this.linkLabelRedirectURL.Name = "linkLabelRedirectURL";
            this.linkLabelRedirectURL.Size = new System.Drawing.Size(251, 17);
            this.linkLabelRedirectURL.TabIndex = 39;
            this.linkLabelRedirectURL.TabStop = true;
            this.linkLabelRedirectURL.Text = "Know more /post feedback or suggestion";
            this.linkLabelRedirectURL.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabelRedirectURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRedirectURL_LinkClicked);
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelInfo.Controls.Add(this.labelVersion1);
            this.panelInfo.Controls.Add(this.labelAppDescription1);
            this.panelInfo.Controls.Add(this.labelProductName1);
            this.panelInfo.Controls.Add(this.pictureBoxLogo);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(384, 84);
            this.panelInfo.TabIndex = 38;
            // 
            // labelVersion1
            // 
            this.labelVersion1.AutoSize = true;
            this.labelVersion1.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion1.Location = new System.Drawing.Point(327, 24);
            this.labelVersion1.Name = "labelVersion1";
            this.labelVersion1.Size = new System.Drawing.Size(45, 17);
            this.labelVersion1.TabIndex = 19;
            this.labelVersion1.Text = "1.0.0.0";
            // 
            // labelAppDescription1
            // 
            this.labelAppDescription1.AutoSize = true;
            this.labelAppDescription1.BackColor = System.Drawing.Color.Transparent;
            this.labelAppDescription1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppDescription1.ForeColor = System.Drawing.Color.Black;
            this.labelAppDescription1.Location = new System.Drawing.Point(106, 56);
            this.labelAppDescription1.Name = "labelAppDescription1";
            this.labelAppDescription1.Size = new System.Drawing.Size(246, 17);
            this.labelAppDescription1.TabIndex = 18;
            this.labelAppDescription1.Text = "Practice Account Management System.";
            // 
            // labelProductName1
            // 
            this.labelProductName1.AutoSize = true;
            this.labelProductName1.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName1.Font = new System.Drawing.Font("Century Gothic", 16F);
            this.labelProductName1.ForeColor = System.Drawing.Color.Black;
            this.labelProductName1.Location = new System.Drawing.Point(104, 17);
            this.labelProductName1.Name = "labelProductName1";
            this.labelProductName1.Size = new System.Drawing.Size(180, 25);
            this.labelProductName1.TabIndex = 17;
            this.labelProductName1.Text = "iHealthAccount";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::iHealthAccount.Application.Properties.Resources.address_book_alt;
            this.pictureBoxLogo.Location = new System.Drawing.Point(12, 9);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 16;
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(297, 236);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 25);
            this.buttonOk.TabIndex = 37;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(12, 104);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(98, 17);
            this.labelVersion.TabIndex = 31;
            this.labelVersion.Text = "Version : 1.0.0.0";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompanyName.Location = new System.Drawing.Point(12, 138);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(113, 17);
            this.labelCompanyName.TabIndex = 33;
            this.labelCompanyName.Text = "Company Name : ";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDescription.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.Location = new System.Drawing.Point(12, 158);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.Size = new System.Drawing.Size(360, 49);
            this.textBoxDescription.TabIndex = 34;
            this.textBoxDescription.TabStop = false;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.Location = new System.Drawing.Point(12, 87);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(197, 17);
            this.labelProductName.TabIndex = 30;
            this.labelProductName.Text = "Product Name : iHealthAccount";
            // 
            // labelCopyRight
            // 
            this.labelCopyRight.AutoSize = true;
            this.labelCopyRight.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyRight.Location = new System.Drawing.Point(12, 121);
            this.labelCopyRight.Name = "labelCopyRight";
            this.labelCopyRight.Size = new System.Drawing.Size(162, 17);
            this.labelCopyRight.TabIndex = 32;
            this.labelCopyRight.Text = "Copy Right : Open License";
            // 
            // AboutApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 271);
            this.Controls.Add(this.panelDetails);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AboutApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutApp";
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelCopyRight;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelVersion1;
        private System.Windows.Forms.Label labelAppDescription1;
        private System.Windows.Forms.Label labelProductName1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.LinkLabel linkLabelRedirectURL;

    }
}