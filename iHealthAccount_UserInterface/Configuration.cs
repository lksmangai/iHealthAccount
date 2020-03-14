using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Messaging;
using iHealthAccount.Configurations;
using System.IO;

namespace iHealthAccount.UI
{
    public partial class Configuration : HealthAccountBase
    {        

        public Configuration()
        {
            InitializeComponent();
        }
                      
        private void Configuration_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            InitScreenData();
        }
                     
        private void InitScreenData()
        {
            lblDBName.Text = iHealthAccount.Configurations.ApplicationConfiguration.DBProvider;
            if (string.Compare(lblDBName.Text.Trim(), "MSACCESS", true) == 0 && SessionParameters.UserRole == Common.UserRole.Admin )
                btnBackup.Enabled = true;                      
        }
             
        private void btnBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();
            folderBrowse.ShowDialog();
            string selectedPath = folderBrowse.SelectedPath;

            if (selectedPath == string.Empty)
            {
                message1.SetMessage(MessageManager.GetMessage("54"));
                return;
            }

            string sourceFileName = GetDBLocation();

            try
            {
                File.Copy(sourceFileName, selectedPath + @"\" + Path.GetFileName(sourceFileName), true);
                message1.SetMessage(MessageManager.GetMessage("55"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetDBLocation()
        {
            string msAccessDBPath = string.Empty;
            string dbNameAttribute = iHealthAccount.Configurations.ApplicationConfiguration.DBProvider == "MSACCESS" ? "Data Source" : "dbq";
            foreach (string str in iHealthAccount.Configurations.ApplicationConfiguration.ConnectionString.Split(Convert.ToChar(";")))
            {
                if (str.Contains("="))
                    if (string.Compare(str.Split(Convert.ToChar("="))[0], dbNameAttribute, true) == 0)
                    {
                        msAccessDBPath = str.Split(Convert.ToChar("="))[1];
                        break;
                    }                
            }

            return msAccessDBPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       
    }
}