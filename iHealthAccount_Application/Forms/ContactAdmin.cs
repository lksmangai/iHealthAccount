using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using iHealthAccount.Application.Properties;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Messaging;
using iHealthAccount.Formatting;

namespace iHealthAccount.Application.Forms
{
    public partial class ContactAdmin : Form
    {
        public ContactAdmin()
        {
            InitializeComponent();
            InitializeIcon();
        }

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.bubbles_icon.GetHicon());
            this.errorProvider1.Icon = Icon.FromHandle(Resources.sign_error_icon.GetHicon());
        }

        private void Options_CheckedChanged(object sender, EventArgs e)
        {
            string optionSelected = ((RadioButton)(sender)).Name;
            string subject = string.Empty;
            switch (optionSelected)
            {
                case "radioButtonBug":
                    if (((RadioButton)(sender)).Checked)
                        subject = "iHealthAccount: Bug Report";
                    break;
                case "radioButtonQuery":
                    if (((RadioButton)(sender)).Checked)
                        subject = "iHealthAccount: Query";
                    break;
                case "radioButtonRequest":
                    if (((RadioButton)(sender)).Checked)
                        subject = "iHealthAccount: Feature Request";
                    break;
                case "radioButtonSuggestion":
                    if (((RadioButton)(sender)).Checked)
                        subject = "iHealthAccount: Suggestion";
                    break;
            }

            textBoxSubject.Text = subject;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            labelMessage.Text = string.Empty;

            int length = textBoxMessage.Text.Trim().Length;
            string errorMessage = string.Empty;
            string message = textBoxMessage.Text.Trim();
            string subject = textBoxSubject.Text.Trim();
            string toMail = iHealthAccount.Configurations.ApplicationConfiguration.SupportMail;

            if (length == 0)
            {
                errorMessage = MessageManager.GetMessage("62");
                errorProvider1.SetError(textBoxMessage, errorMessage);
                labelMessage.Text = errorMessage;
                return;
            }

            Process.Start("mailto:" + toMail + "&Subject=" + subject + "&Body=" + message);
            this.Close();
            this.Dispose();
        }


        private void linkLabelRedirectURL_LocationChanged(object sender, EventArgs e)
        {
            Process.Start(iHealthAccount.Configurations.ApplicationConfiguration.SupportURL);
        }
    }
}
