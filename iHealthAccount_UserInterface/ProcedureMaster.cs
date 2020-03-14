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
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;
using System.Collections;
using System.Diagnostics;

namespace iHealthAccount.UI
{
    public partial class ProcedureMaster : HealthAccountBase
    {
        private int procId = 0;
        private int _expenseByUserID = 0;
        private Procedure proc = new Procedure();
        private Arch arch = new Arch();
        private DBHelper helper = new DBHelper();
        private HomeProcedureReport homePageReport = new HomeProcedureReport();
        private Form baseForm;
        
        public ProcedureMaster(Form baseForm)
        {
            InitializeComponent();
            this.baseForm = baseForm;
            Icon i = Icon.FromHandle(iHealthAccount.UI.Properties.Resources.address_book_alt_icon.GetHicon());
            this.Icon = i;
        }

        public ProcedureMaster()
        {
            InitializeComponent();
            Icon i = Icon.FromHandle(iHealthAccount.UI.Properties.Resources.address_book_alt_icon.GetHicon());
            this.Icon = i;
        }

        private void DisableAddAndClearMessage()
        {            
            btnAdd.Visible  = false;
        }
                   
        public void Home_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            InitScreenData();
            this.Text = ApplicationDetails.AssemblyProduct + " " + ApplicationDetails.AssemblyVersion;
            if (SessionParameters.UserRole == Common.UserRole.Admin)
                btnAdd.Enabled = false;
        }

        private void InitScreenData()
        {
            DisplayDataInGrid();
            Arch arch = new Arch();            
            //arch.FillDataInCombo(cmbItem, Arch.ComboBoxItem.Item );
            GetStatusBarDetails();

            if (SessionParameters.UserRole != Common.UserRole.Admin)
                MENU_FILE_ADD_USER.Enabled = false;

            lblExpenseCCY.Text = Configurations.ApplicationConfiguration.ExpenseCCY;
        }

        private void GetStatusBarDetails()
        {
            
            Users user = new Users();
            STATUS_BAR_USER_NAME.Text = "You are logged in as : " + SessionParameters.UserName;
            STATUS_BAR_LAST_LOGIN_DATE.Text = "Last login : " + user.GetLastLastLoginDate(SessionParameters.UserID);
            STATUS_BAR_CURRENT_DATE.Text = DataFormat.DateToDisp(System.DateTime.Now.ToShortDateString());
        }

     

        private void ShowUpdateDelBtn()
        {
            lblMessage.Text = string.Empty;           
            btnDel.Visible = true ;
            
            label6.Visible = true;
            txtEntryDate.Visible = true;
        }

        private void DisplayDataInGrid()
        {                               
            GRID_VIEW_EXPENSE_DETAILS.DataSource = homePageReport.HomeProcedureDetails();
            GRID_VIEW_EXPENSE_DETAILS.Columns[0].Visible = false;            
            lblItemStatus.Text = MessageManager.GetMessage("3", homePageReport.HomeProcedureDetails().Rows.Count.ToString());
            lblItemStatus.Visible = true;
        }   

        private void GetDataForUpdateAndDelete(int expenseID)
        {         
            DataTable dt = new DataTable();
            dt = proc.GetDisplayData(expenseID);
            if (dt.Rows.Count == 0)
                return;
            txtProcCode.Text = DataFormat.GetString(dt.Rows[0][1]);
            txtProcCode.Enabled = false;
            txtProcName.Text  = DataFormat.GetString(dt.Rows[0][2]);
            txtPrice.Text = DataFormat.GetString(dt.Rows[0][3]);
            txtEntryDate.Value  = Convert.ToDateTime( dt.Rows[0][4].ToString().Trim());
        }
          
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("4");                
                return;
            }         

            string amount = txtPrice.Text.Trim();
            string message = string.Empty;
            string procCode = txtProcCode.Text.Trim();

            if (procCode == string.Empty)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(txtProcCode, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (amount == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (txtEntryDate.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(txtEntryDate, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            //Logger.WriteTrace("ProcedureMaster [Add new Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Item Id : " + itemId.ToString() + Environment.NewLine + "Description : " + txtProcName.Text.Trim() + Environment.NewLine + "Price : " + txtPrice.Text.Trim());
            AddProcedure(procCode, txtProcName.Text.Trim(), txtPrice.Text.Trim(), SessionParameters.UserID, txtEntryDate.Value.ToString());
        }

        private void AddProcedure(string procCode, string expDesc, string expAmount, int expBy, string expDate)
        {
            Procedure objProc = new Procedure();
            bool addStatus = objProc.AddNewProcedure(procCode, expDesc, expAmount, expBy, expDate);

            if (addStatus)
            {
                DisplayDataInGrid();
                //objProc.UpdateProcId(procCode, expDesc);
                ClearFunction();                    
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("9");
            }
            else
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("10");
                           
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {

        }
        
            
        private void UpadteProcedure(int procId, string procCode, string procDesc, string procAmount, int entryBy, string effectiveDate)
        {
            ClearMessage();
            if (proc.ModifyProcedures(procId, procCode, procDesc, procAmount, entryBy, effectiveDate))
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("11");
                DisplayDataInGrid();
                ClearFunction();
            }
            else
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("12");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("13");
                return;
            }

            if (!_isSelectedForUpdate)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("14");
                return;
            }
            
            if (SessionParameters.UserID != _expenseByUserID)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("13");
                return;
            }


            if (MessageManager.DisplayMessage("15", new string[] { iHealthAccount.Configurations.ApplicationConfiguration.ExpenseCCY, GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["ProcedureAmount"].Value.ToString(), GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["Item"].Value.ToString() }, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Logger.WriteTrace("ProcedureMaster [Delete Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + procId);
                proc.DeleteProcedures(procId);
                DisplayDataInGrid();
                ClearFunction();
            }                                                              
         }

   
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFunction();
            _isSelectedForUpdate = false;
        }

        private void ClearMessage()
        {
            errorProvider1.Clear();
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;
        }

        private void ClearFunction()
        {
            errorProvider1.Clear();
            txtProcCode.Text = string.Empty;
            txtProcName.Text = string.Empty;            
            txtPrice.Text = string.Empty;
            txtEntryDate.Text = string.Empty;
            txtProcCode.Enabled = true;
            _isSelectedForUpdate = false;
            procId = 0;

            btnDel.Enabled = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;

            if (GRID_VIEW_EXPENSE_DETAILS.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in GRID_VIEW_EXPENSE_DETAILS.Rows)
                {
                    if (row.Selected)
                    {
                        row.Selected = false;
                        return;
                    }
                }
            }
        }

        private void MENU_FILE_LOG_OFF_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void MENU_FILE_ADD_USER_Click(object sender, EventArgs e)
        {
            OpenNewUser();
        }

        private void OpenNewUser()
        {          
            NewUser objNewUser = new NewUser();
            objNewUser.ShowDialog();
        }

        private void MENU_FILE_ADD_ITEM_Click(object sender, EventArgs e)
        {
            OpennewItem();      
        }

        private void OpennewItem()
        {            
            NewItem objItem = new NewItem();
            objItem.ShowDialog();
            //arch.FillDataInCombo(cmbItem, Arch.ComboBoxItem.Item);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            OpenExpenseReport();
        }

        private void OpenExpenseReport()
        {
            IndividualExpReport objExpenseReport = new IndividualExpReport();
            objExpenseReport.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                DisplayDataInGrid();
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (! _isApplicationExit)
                if (!DoLogOff())            
                    e.Cancel = true;
            
        }

        bool _isApplicationExit = false;
        private bool DoLogOff()
        {
            bool retValue = false;
            if (baseForm != null)
            {
                DialogResult dr = MessageManager.DisplayMessage("16", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    _isApplicationExit = true;
                    (new ApplicationContext()).Dispose();
                    baseForm.Dispose();
                    retValue = true;
                    Application.Exit();
                }
                else
                    retValue = false;
            }
            else
            {
                return true;
            }

            return retValue;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MENU_HELP_ABOUT_Click(object sender, EventArgs e)
        {
            AboutHealthAccount aboutHealthAccount = new AboutHealthAccount();
            aboutHealthAccount.ShowDialog();
        }

        private void MENU_REPORT_MONTHLY_REPORT_Click(object sender, EventArgs e)
        {
            OpenCustomReport();
        }

        private void OpenCustomReport()
        {
            CustomReport customReport = new CustomReport();
            customReport.ShowDialog();
        }

        private void MENU_EDIT_PROFILE_Click(object sender, EventArgs e)
        {
            EditProfileForm();
        }

        private void EditProfileForm()
        {
            Profile profile = new Profile();
            profile.ShowDialog();

            DisplayDataInGrid();
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration();
            config.Show();           
        }

        private void changeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            
        }

        private void analyticReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAnalyticReport();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void TOOL_PROFILE_Click(object sender, EventArgs e)
        {
            EditProfileForm();
        }



        private void TOOL_LOG_OFF_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TOOL_EXPENSE_REPORT_Click(object sender, EventArgs e)
        {
            OpenExpenseReport();
        }

        private void TOOL_MONTHLY_REPORT_Click(object sender, EventArgs e)
        {
            OpenCustomReport();
        }

        private void TOOL_ANALYTIC_REPORT_Click(object sender, EventArgs e)
        {
            ShowAnalyticReport();
        }

        private void ShowAnalyticReport()
        {
            ClearMessage();
            AnalyticReport analyticReport = new AnalyticReport();
            analyticReport.ShowDialog();
        }

   
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ClearMessage();
            OpennewItem();
        }

      

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            ClearMessage();
            DisplayDataInGrid();
        }

        private void manageMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {          
        }

        private void MENU_FILE_SWITCH_USER_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageManager.DisplayMessage("17", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                Preferences.ClearCredentials();
                this.Hide();
                Login objLogin = new Login();
                objLogin.Show();
            }
        }

        private bool _isSelectedForUpdate = false;
        private void GRID_VIEW_EXPENSE_DETAILS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            STATUS_BAR_STATUS_MESSAGE.Text = String.Empty;
            object value = GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells[0].Value;
            string strValue = (value == null) ? "1":value.ToString();
            
            procId = DataFormat.GetInteger( strValue);
            _expenseByUserID = DataFormat.GetInteger(GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["UserId"].Value.ToString());
            GetDataForUpdateAndDelete(procId);
            _isSelectedForUpdate = true;

            if (_expenseByUserID != SessionParameters.UserID)
            {
                btnUpdate.Enabled = false;
                btnDel.Enabled = false;
                btnAdd.Enabled = false;
                return;
            }
            else
            {                
                btnDel.Enabled = true;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;
            errorProvider1.Clear();
            string message = string.Empty;

            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("12");
                return;
            }

            if (SessionParameters.UserID != _expenseByUserID)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("4"); ;
                return;
            }

            if (!_isSelectedForUpdate)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("18");
                return;
            }

            int itemId = DataFormat.GetInteger(GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells[0].Value.ToString());
            string procCode = txtProcCode.Text.Trim();
            string amount = txtPrice.Text.Trim();

            if (procCode == string.Empty)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(txtProcCode, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (amount == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (txtEntryDate.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(txtEntryDate, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }
            
            Logger.WriteTrace("ProcedureMaster [Update Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + procId + Environment.NewLine + "Description : " + txtProcName.Text.Trim() + Environment.NewLine + "Price : " + txtPrice.Text.Trim());
            UpadteProcedure(itemId, procCode, txtProcName.Text.Trim(), txtPrice.Text.Trim(), SessionParameters.UserID, txtEntryDate.Value.ToShortDateString());
            ClearFunction();           
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true && e.KeyChar != '.' && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }

        private void MENU_REPORT_EXPENCE_REPORT_Click(object sender, EventArgs e)
        {
            IndividualExpReport expReport = new IndividualExpReport();
            expReport.ShowDialog();
        }

        private void MENU_REPORT_ANALYTIC_REPORT_Click(object sender, EventArgs e)
        {
            AnalyticReport analyticRep = new AnalyticReport();
            analyticRep.ShowDialog();
        }

        private void MENU_TOOLS_CONFIGURATION_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration();
            config.ShowDialog();
        }

        private void MENU_TOOLS_DIAGNOSTICS_Click(object sender, EventArgs e)
        {
            (new Diagnostic()).ShowDialog();
        }

        private void MENU_TOOLS_CALCULATOR_Click(object sender, EventArgs e)
        {
            RunProcess("calc");
        }

        private void RunProcess(string processName)
        {
            Process.Start(processName);
        }

        private void MENU_TOOLS_NOTEPAD_Click(object sender, EventArgs e)
        {
            RunProcess("notepad");
        }

        private void MENU_TOOLS_CALENDAR_Click(object sender, EventArgs e)
        {
            (new Calendar()).Show();
        }

        private void TOOL_HELP_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int itemId = DataFormat.GetInteger(((DictionaryEntry)(cmbItem.SelectedItem)).Key);
            //if ( itemId == -1) 
            {
                txtProcName.Clear();
                return;
            }
            //txtProcName.Text = (new Items()).GetItemDescription(itemId.ToString());            
        }

        private void MENU_HELP_CONTACT_ADMIN_Click(object sender, EventArgs e)
        {
            (new ContactAdmin()).ShowDialog();
        }

        private void MENU_HELP_HELP_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        private void ShowHelpFile()
        {
            Process.Start(Application.StartupPath + @"\Help\iHealthAccount.pdf");
        }

        private void windowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void procedureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcedureMaster pm = new ProcedureMaster();
            pm.Show();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iHome i = new iHome();
            i.Show();
        }

        private void expenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
        }   
    }
}