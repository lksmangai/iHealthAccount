using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.BusinessLogic;

namespace iHealthAccount.UI
{
    public partial class MiscellaneousReport : Form
    {        
        public MiscellaneousReport()
        {
            InitializeComponent();
            AddItems();
        }

        private void AddItems()
        {
            DateTime dt = DateTime.Now;
            int yearStart = dt.Year - 15;

            cmbYear.Items.Clear();
            cmbYear.Items.Add("[SELECT]");

            for (int i = yearStart; i <= dt.Year; i++)
            {
                cmbYear.Items.Add(i);
            }
        }
        private void MiscellaneousReport_Load(object sender, EventArgs e)
        {
            cmbMonth.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
        }

       

        private void btnGenerate_Click(object sender, EventArgs e)
        {            
            string month = cmbMonth.Text.Trim();
            string year = cmbYear.Text.Trim();
            DataSet dsReportData = new DataSet();

            if (month.Equals("") || year.Equals(""))
            {
                
            }
            else
            {
                
            }
        }

      }
}