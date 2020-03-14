using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.Application.Properties;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Messaging;
using iHealthAccount.Formatting;

namespace iHealthAccount.Application.Forms
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
            InitializeIcon();
            InitScreenData();
        }

        private BusinessLogic.Users manageProfile = new BusinessLogic.Users();

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.profile_icon.GetHicon());
            this.errorProvider1.Icon = Icon.FromHandle(Resources.sign_error_icon.GetHicon());
        }

        private void InitScreenData()
        {
            BusinessLogic.Users manageProfile = new BusinessLogic.Users();
            DataTable dtUserProfile = manageProfile.GetUserProfile(SessionParameters.UserID);
            textBoxUsername.Text = DataFormat.GetString(dtUserProfile.Rows[0]["UserName"]);
            textBoxFirstName.Text = DataFormat.GetString(dtUserProfile.Rows[0]["FirstName"]);
            textBoxLastName.Text = DataFormat.GetString(dtUserProfile.Rows[0]["LastName"]);
            textBoxEmail.Text = DataFormat.GetString(dtUserProfile.Rows[0]["Email"]);
            textBoxMobile.Text = DataFormat.GetString(dtUserProfile.Rows[0]["Mobile"]);
            groupBox1.Text = SessionParameters.UserName;
        }

        private void checkBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPassword.Checked)
            {
                textBoxOldPass.Enabled = true;
                textBoxNewPass.Enabled = true;
                textBoxConPass.Enabled = true;

                textBoxOldPass.Clear();
                textBoxNewPass.Clear();
                textBoxConPass.Clear();
            }
            else
            {
                textBoxOldPass.Enabled = false;
                textBoxNewPass.Enabled = false;
                textBoxConPass.Enabled = false;

                textBoxOldPass.Clear();
                textBoxNewPass.Clear();
                textBoxConPass.Clear();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {            
            labelMessage.Text = string.Empty;
            errorProvider1.Clear();

            string firstName = textBoxFirstName.Text.Trim();
            string middleName = textBoxMidName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string oldPassword = textBoxOldPass.Text.Trim();
            string newPassword = textBoxNewPass.Text.Trim();   
            string confirmNewPassword = textBoxConPass.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string mobile = textBoxMobile.Text.Trim();
            string message = string.Empty;

            if (firstName == string.Empty)
            {
                message = MessageManager.GetMessage("46");
                errorProvider1.SetError(textBoxFirstName, message);
                labelMessage.Text = message;
                return;
            }

            if (checkBoxPassword.Checked)
            {
                if (oldPassword == string.Empty)
                {
                    message = MessageManager.GetMessage("47");
                    errorProvider1.SetError(textBoxOldPass, message);
                    labelMessage.Text = message;
                    return;
                }

                if (newPassword == string.Empty)
                {
                    message = MessageManager.GetMessage("48");
                    errorProvider1.SetError(textBoxNewPass, message);
                    labelMessage.Text = message;
                    return;
                }

                if (confirmNewPassword == string.Empty)
                {
                    message = MessageManager.GetMessage("49");
                    errorProvider1.SetError(textBoxConPass, message);
                    labelMessage.Text = message;
                    return;
                }

                if (string.Compare(newPassword, confirmNewPassword, true) != 0)
                {
                    message = MessageManager.GetMessage("50");
                    errorProvider1.SetError(textBoxConPass, message);
                    labelMessage.Text = message;
                    return;
                }

                if (!manageProfile.IsValidPassword(SessionParameters.UserID, oldPassword))
                {
                    message = MessageManager.GetMessage("51");
                    errorProvider1.SetError(textBoxOldPass, message);
                    labelMessage.Text = message;
                    return;
                }
            }

            bool status = false;
            status = checkBoxPassword.Checked ? manageProfile.UpdateUserProfile(SessionParameters.UserID, firstName, lastName, newPassword, email, mobile) : manageProfile.UpdateUserProfile(SessionParameters.UserID, firstName, lastName, email, mobile);

            if (status)
                labelMessage.Text = MessageManager.GetMessage("52");
            else
                labelMessage.Text = MessageManager.GetMessage("53");

            /*this.DialogResult = DialogResult.OK;
            this.Dispose();*/
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
