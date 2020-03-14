using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Messaging;


namespace iHealthAccount.UI
{
    public partial class AnalyticReport : HealthAccountBase
    {
        //private MonthYearStatic staticData = new MonthYearStatic();        

        public AnalyticReport()
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            message1.Clear();
            MonthlyReport monthlyReport = new MonthlyReport();
            string month = cmbMonth.Text.Trim();
            string year = cmbYear.Text.Trim();
            DataSet dsReportData = new DataSet();
            string message = string.Empty;

            if (month.Equals("[SELECT]") && year.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                message1.MessageText = message;
                errorProvider1.SetError(cmbYear, message);
                errorProvider1.SetError(cmbMonth, message);

                dgrAnalyticReport.Visible = false;
                return;
            }

            if (month.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                message1.MessageText = message;                
                errorProvider1.SetError(cmbMonth, message);

                dgrAnalyticReport.Visible = false;
                return;
            }

            if (year.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                message1.MessageText = message;
                errorProvider1.SetError(cmbYear, message);                

                dgrAnalyticReport.Visible = false;
                return;
            }

            dsReportData = monthlyReport.MonthlyReportData(month, year);
            Analytics analytics = new Analytics();
            DataTable table = new DataTable();

            if(rbnItemWise.Checked)
                table = analytics.AnalyticReport(month, year, Analytics.AnalyticReportType.ItemWise );
            else
                table = analytics.AnalyticReport(month, year, Analytics.AnalyticReportType.Individual);
            
            if (table.Rows.Count > 0)
            {
                dgrAnalyticReport.Visible = true;
                dgrAnalyticReport.DataSource = table;
                grpAnalyticReport.Visible = true;
                SetGridStyle();
            }
            else
            {
                grpAnalyticReport.Visible = false;
                dgrAnalyticReport.Visible = false;
                MessageManager.DisplayMessage("45" ,month ,year);                    
            }

        }

        private void SetGridStyle()
        {
            foreach (DataGridViewColumn col in dgrAnalyticReport.Columns)
            {
                col.ReadOnly = true;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AnalyticReport_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            cmbMonth.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
        }

          
    }
}