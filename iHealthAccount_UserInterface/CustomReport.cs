using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iHealthAccount.BusinessLogic;
using iHealthAccount.Messaging;
using iHealthAccount.Format;
using iHealthAccount.Formatting;


namespace iHealthAccount.UI
{
    public partial class CustomReport : HealthAccountBase
    {
        
        private MonthlyReport monthlyReport = new MonthlyReport();
        

        public CustomReport()
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
            string month = cmbMonth.Text.Trim();
            string year = cmbYear.Text.Trim();
            DataSet dsReportData = new DataSet();
            int columnIndex = 0;
            string reportStatistics = string.Empty;
            string message = string.Empty;

            if (month.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                errorProvider1.SetError(cmbMonth, message);
                message1.MessageText = message;
                grpExpenseStatistics.Visible = false;
                grpReport.Visible = false ;
                return;
            }

            if (year.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                errorProvider1.SetError(cmbYear, message);
                message1.MessageText = message ;
                grpExpenseStatistics.Visible = false;
                grpReport.Visible = false;
                return;
            }
            
            if (rbnIndividual.Checked)                
                dsReportData = monthlyReport.MonthlyReportData(month, year, MonthlyReport.ReportType.Individual);                
            else                
                dsReportData = monthlyReport.MonthlyReportData(month, year, MonthlyReport.ReportType.ItemWise);

            if (dsReportData.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = monthlyReport.GeneralDetails(month, year);
                lblFinalizeDetails.Text = GetReportFinalizationDetails(month, year);
                grpExpenseStatistics.Visible = true;
                grpReport.Visible = true;
                dgrReport.DataSource = dsReportData.Tables[0];
                
                if(rbnIndividual.Checked)
                    dgrReport.Columns[0].Visible = false;

                columnIndex = rbnIndividual.Checked == true ? 2 : 1;
                dgrReport.Columns[columnIndex].Width = 150;
                SetGridStyle();
            }
            else
            {
                grpExpenseStatistics.Visible = false;
                grpReport.Visible = false;
                MessageManager.DisplayMessage("45", month, year);
            }                                                     
            
        }

        private void SetGridStyle()
        {
            foreach (DataGridViewColumn col in dgrReport.Columns)
            {
                col.ReadOnly = true;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private string GetReportFinalizationDetails(string month, string year)
        {
            string[] finalizationDetails = monthlyReport.ReportFinalizeDetails(month, year);
            string details = string.Empty;
            int serialNo =0;
            
            if(finalizationDetails != null && finalizationDetails.Length > 0)
            {
                details = details + "Finalization Dates" + Environment.NewLine;
                for(int i=0;i<finalizationDetails.Length ;i++)
                {                    
                    serialNo = i+1;
                    if(!finalizationDetails[i].Equals("N\\A"))
                        details = details + serialNo.ToString() + ". " + DataFormat.GetDateFromDBDate(finalizationDetails[i]) + Environment.NewLine ;
                    else
                        details = details + finalizationDetails[i] + Environment.NewLine;
                }                
            }

            return details;
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void CustomReport_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            cmbMonth.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
        }       
    }
}