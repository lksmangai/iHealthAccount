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
using iHealthAccount.Application.Classes;

namespace iHealthAccount.Application.Forms
{
    public partial class AboutApp : Form
    {
        public AboutApp()
        {
            InitializeComponent();
            InitializeIcon();
            InitScreenDetails();
        }

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.sign_info_icon.GetHicon());
        }

        private void InitScreenDetails()
        {
            this.labelProductName.Text = string.Format("Product Name: {0}", ApplicationDetails.AssemblyProduct);
            this.labelProductName1.Text = ApplicationDetails.AssemblyProduct;
            this.labelVersion.Text = string.Format("Version: {0}", ApplicationDetails.AssemblyVersion);
            this.labelVersion1.Text = ApplicationDetails.AssemblyVersion;
            this.labelCopyRight.Text = string.Format("Copyright: {0}", ApplicationDetails.AssemblyCopyright);
            this.labelCompanyName.Text = string.Format("Company Name: {0}", ApplicationDetails.AssemblyCompany);
            this.textBoxDescription.Text = ApplicationDetails.AssemblyDescription;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void linkLabelRedirectURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(iHealthAccount.Configurations.ApplicationConfiguration.SupportURL);
        }
    }
}
