using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.Application.Properties;
using iHealthAccount.Messaging;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Formatting;

namespace iHealthAccount.Application
{
    public partial class Login : Form
    {
        private Size originalSize;
        public Login()
        {
            InitializeComponent();
            originalSize = this.Size;
            this.Size = new Size(450, 260);
            InitializeIcon();

        }

        private bool info = false;
        private UserAuthentication userAuthentication = new UserAuthentication();

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.address_book_alt_icon.GetHicon());
        }

        private void InitScreenData()
        {
            BusinessLogic.Users profile = new BusinessLogic.Users();
            DataTable dataTableUserProfile = profile.GetUserProfile(SessionParameters.UserID);
            this.Text = string.Format("iHealthAccounts [{0}]" ,DataFormat.GetString(dataTableUserProfile.Rows[0]["FirstName"]));
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBoxUsername.Text.Trim();
                string password = textBoxPassword.Text.Trim();
                errorProvider1.Clear();
                labelMessage.Text = string.Empty;

                if (userName == string.Empty && password == string.Empty)
                {
                    labelMessage.Text = MessageManager.GetMessage("1", false);
                    errorProvider1.SetError(textBoxUsername, MessageManager.GetMessage("1", false));
                    return;
                }

                if (userName == string.Empty)
                {
                    labelMessage.Text = MessageManager.GetMessage("1", false);
                    errorProvider1.SetError(textBoxUsername, MessageManager.GetMessage("1", false));

                    return;
                }

                if (password == string.Empty)
                {
                    labelMessage.Text = MessageManager.GetMessage("1", false);
                    errorProvider1.SetError(textBoxPassword, MessageManager.GetMessage("1", false));

                    return;
                }

                int userId = 0;
                Common.UserRole role = new Common.UserRole();

                bool validUser = userAuthentication.IsValidUser(userName, password, out userId, out role);
                Logger.WriteTrace("Login", "Username : " + userName + Environment.NewLine + "Success : " + validUser.ToString());

                if (!validUser)
                    MessageManager.DisplayCustomMessage("Invalid user Id or password.");
                else
                {
                    if (checkBoxRemember.Checked)
                        SavePreference(true);
                    else
                        SavePreference(false);

                    SessionParameters.UserID = userId;
                    SessionParameters.UserName = userName;
                    SessionParameters.UserRole = role;
                    (new Users()).UpdateLastLoginDate(SessionParameters.UserID);

                    panelLogin.Hide();
                    Size currentSize = this.Size;
                    AddSize = new Size(originalSize.Width - currentSize.Width, originalSize.Height - currentSize.Height);
                    AdjustSize(AddSize);
                    this.Size = new Size( originalSize.Width, originalSize.Height+40);
                    panelOpen.Show();

                    InitScreenData();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
        private Size AddSize = new Size(0, 0);
        private void AdjustSize(System.Drawing.Size AddSize)
        {
            //groupBox1.Location = new Point(groupBox1.Location.X + AddSize.Width, groupBox1.Location.Y + AddSize.Height);
            //buttonLogin.Location = new Point(buttonLogin.Location.X + AddSize.Width, buttonLogin.Location.Y + AddSize.Height);
            //buttonCancel.Location = new Point(buttonCancel.Location.X + AddSize.Width, buttonCancel.Location.Y + AddSize.Height);
            //buttonInfo.Location = new Point(buttonInfo.Location.X + AddSize.Width, buttonInfo.Location.Y + AddSize.Height);
            //buttonInfo1.Location = new Point(buttonInfo1.Location.X + AddSize.Width, buttonInfo1.Location.Y + AddSize.Height);
            //buttonProfile.Location = new Point(buttonProfile.Location.X + AddSize.Width, buttonProfile.Location.Y + AddSize.Height);
            //buttonExit.Location = new Point(buttonExit.Location.X + AddSize.Width, buttonExit.Location.Y + AddSize.Height);
            //this.Size = new Size(800, 800);
            this.Location = new Point(100, 100);
        }
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            (new ApplicationContext()).Dispose();
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            if (info == false)
            {

                groupBox1.Location = new Point(109+AddSize.Width, 155 + 37+AddSize.Height+30);
                groupBox1.BringToFront();
                panelOpen.Size = new Size(panelOpen.Size.Width, panelOpen.Height + 100);
                buttonLogin.Location = new Point(buttonLogin.Location.X, buttonLogin.Location.Y + 100);
                buttonCancel.Location = new Point(buttonCancel.Location.X, buttonCancel.Location.Y + 100);
                buttonInfo.Location = new Point(buttonInfo.Location.X, buttonInfo.Location.Y + 100);
                buttonInfo1.Location = new Point(buttonInfo1.Location.X, buttonInfo1.Location.Y + 100);
                buttonProfile.Location = new Point(buttonProfile.Location.X, buttonProfile.Location.Y + 100);
                buttonExit.Location = new Point(buttonExit.Location.X, buttonExit.Location.Y + 100);
                buttonInfo.Text = "&Info<<";
                this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height + 100);
                panelOpen.Size = new Size(panelOpen.Size.Width, panelOpen.Height + 100);
                info = true;
            }
            else
            {
                buttonLogin.Location = new Point(buttonLogin.Location.X, buttonLogin.Location.Y - 100);
                buttonCancel.Location = new Point(buttonCancel.Location.X, buttonCancel.Location.Y - 100);
                buttonInfo.Location = new Point(buttonInfo.Location.X, buttonInfo.Location.Y - 100);
                buttonInfo1.Location = new Point(buttonInfo1.Location.X, buttonInfo1.Location.Y - 100);
                buttonProfile.Location = new Point(buttonProfile.Location.X, buttonProfile.Location.Y - 100);
                buttonExit.Location = new Point(buttonExit.Location.X, buttonExit.Location.Y - 100);
                buttonInfo.Text = "&Info>>";
                groupBox1.Location = new Point(500+AddSize.Width, 500+AddSize.Height+30);
                groupBox1.BringToFront();
                this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height - 100);
                info = false;
            }
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            Forms.EditProfile p = new Forms.EditProfile();
            p.ShowDialog();

            InitScreenData();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (DoLogOff())
                this.Dispose();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LoadPreferences();
            panelOpen.Hide();
            labelProductName.Text = Classes.ApplicationDetails.AssemblyProduct;
            labelVersion.Text = Classes.ApplicationDetails.AssemblyVersion;
            labelAppDescription.Text = Classes.ApplicationDetails.AssemblyDescription;
            labelProductName1.Text = Classes.ApplicationDetails.AssemblyProduct;
            labelVersion1.Text = Classes.ApplicationDetails.AssemblyVersion;
            labelAppDescription1.Text = Classes.ApplicationDetails.AssemblyDescription;
        }

        private void SavePreference(bool rememberMe)
        {
            if (rememberMe)
            {
                Preferences.SavePreference(Preferences.Preference.Username, textBoxUsername.Text.Trim());
                Preferences.SavePreference(Preferences.Preference.Password, textBoxPassword.Text.Trim());
                Preferences.SavePreference(Preferences.Preference.RememberMe, "true");
            }
            else
            {
                Preferences.SavePreference(Preferences.Preference.Username, string.Empty);
                Preferences.SavePreference(Preferences.Preference.Password, string.Empty);
                Preferences.SavePreference(Preferences.Preference.RememberMe, "false");
            }

            Preferences.SavePreference(Preferences.Preference.LastLoginDate, DateTime.Now.ToString());
            Preferences.SavePreference(Preferences.Preference.LastUser, textBoxUsername.Text.Trim());
            Preferences.Save();
        }

        private void LoadPreferences()
        {
            if (DataFormat.GetBoolean(Preferences.GetPreference(Preferences.Preference.RememberMe)))
            {
                textBoxUsername.Text = Preferences.GetPreference(Preferences.Preference.Username).Trim();
                textBoxPassword.Text = Preferences.GetPreference(Preferences.Preference.Password).Trim();
            }

            labelComputerName.Text = "Computer Name : " + Environment.MachineName;
            labelLogonUser.Text = "Last Logon By : " + Preferences.GetPreference(Preferences.Preference.LastUser);
            labelLogonDate.Text = "Last Logon Date : " + Preferences.GetPreference(Preferences.Preference.LastLoginDate);
        }

        private bool DoLogOff()
        {
            bool retValue = false;
            DialogResult dr = MessageManager.DisplayMessage("16", MessageBoxButtons.YesNo);
            
            if (dr == DialogResult.Yes)
            {
                (new ApplicationContext()).Dispose();
                retValue = true;
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                retValue = false;
            }

            return retValue;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Details.InvoiceDetails invoice = new Details.InvoiceDetails();
            invoice.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Details.ReciptDetails recipt = new Details.ReciptDetails();
            recipt.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Details.WalkInDetails walkIn = new Details.WalkInDetails();
            walkIn.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Masters.ProcedureMaster procMaster = new Masters.ProcedureMaster();
            procMaster.Show();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Masters.CustomerMaster custMaster = new Masters.CustomerMaster();
            custMaster.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Masters.ExpenseMaster expMaster = new Masters.ExpenseMaster();
            expMaster.Show();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Masters.PaymentMaster payMaster = new Masters.PaymentMaster();
            payMaster.Show();
        }
    }
}
