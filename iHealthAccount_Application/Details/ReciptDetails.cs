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
using iHealthAccount.Configurations;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace iHealthAccount.Application.Details
{
    public partial class ReciptDetails : Form
    {
        public ReciptDetails()
        {
            InitializeComponent();
            InitializeIcon();
            InitScreenData();
        }

        private int recptId = 0;
        private int recptByUserID = 0;
        private bool isSelectedForUpdate = false;
        private Recipt recpt = new Recipt();
        private Arch arch = new Arch();
        private DBHelper helper = new DBHelper();
        private HomeReciptReport homeRecptReport = new HomeReciptReport();
        
        private void ReciptDetails_Load(object sender, EventArgs e)
        {
            this.menuStrip1.Renderer = new iRenderer();
            this.toolStrip1.Renderer = new iRenderer();
            this.statusStrip1.Renderer = new iRenderer();
            ClearFunction();
        }

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.address_book_alt_icon.GetHicon());
        }

        private void InitScreenData()
        {
            DisplayDataInGrid();
            GetStatusBarDetails();            
            Arch arch = new Arch();

            if (SessionParameters.UserRole != Common.UserRole.Admin)
                usersToolStripMenuItem.Enabled = false;

            labelExpenseCurrency.Text = Configurations.ApplicationConfiguration.ExpenseCCY;
        }

        private void GetStatusBarDetails()
        {
            Users user = new Users();
            DataTable dataTableUserProfile = user.GetUserProfile(SessionParameters.UserID);
            toolStripStatusLabelUserName.Text = "Logged in as: " + DataFormat.GetString(dataTableUserProfile.Rows[0]["FirstName"]);
            toolStripStatusLabelLoginDate.Text = "Last login: " + user.GetLastLastLoginDate(SessionParameters.UserID);
            toolStripStatusLabelDate.Text = DataFormat.DateToDisp(System.DateTime.Now.ToShortDateString());
        }

        private void DisplayDataInGrid()
        {
            dataGridViewRecpt.DataSource = homeRecptReport.HomeReciptDetails();
            dataGridViewRecpt.Columns[0].Visible = false;
        }   

        private void ImportDatabase()
        {
            openFileDialog1.ShowDialog();
            string selectedPath = openFileDialog1.FileName;
            string sourceFileName = GetDatabaseLocation();

            if (selectedPath == string.Empty)
            {
                labelMessage.Text = MessageManager.GetMessage("54");
                return;
            }
            
            try
            {
                File.Copy(selectedPath, sourceFileName, true);
                labelMessage.Text = MessageManager.GetMessage("55");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExportDatabase()
        {
            saveFileDialog1.ShowDialog();
            string selectedPath = saveFileDialog1.FileName;
            string sourceFileName = GetDatabaseLocation();

            if (selectedPath == string.Empty)
            {
                labelMessage.Text = MessageManager.GetMessage("54");
                return;
            }
            
            try
            {
                File.Copy(sourceFileName, selectedPath, true);
                labelMessage.Text = MessageManager.GetMessage("55");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetDatabaseLocation()
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

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportDatabase();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDatabase();
        }
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Settings settings = new Forms.Settings();
            settings.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.EditUsers user = new Forms.EditUsers();
            user.ShowDialog();
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.EditProfile profile = new Forms.EditProfile();
            profile.ShowDialog();
        }

        private void aboutAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.AboutApp about = new Forms.AboutApp();
            about.ShowDialog();
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.ContactSupport contact = new Forms.ContactSupport();
            contact.ShowDialog();
        }

        private void AddRecipts(string customerName, string customerCode, string invoiceNumber, string reciptNumber, string reciptAmount, string reciptDate, int entryBy)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            if (recpt.AddNewRecipt(customerName, customerCode, invoiceNumber, reciptNumber, reciptAmount, reciptDate, entryBy))
            {
                labelMessage.Text = MessageManager.GetMessage("9");
                DisplayDataInGrid();
                ClearFunction();
            }
            else
            {
                labelMessage.Text = MessageManager.GetMessage("10");
            }
        }

        private void UpadteRecipts(int recptId, string customerName, string customerCode, string invoiceNumber, string reciptNumber, string reciptAmount, string reciptDate, int entryBy)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            if (recpt.ModifyRecipt(recptId, customerName, customerCode, invoiceNumber, reciptNumber, reciptAmount, reciptDate, entryBy))
            {
                labelMessage.Text = MessageManager.GetMessage("11");
                DisplayDataInGrid();
                ClearFunction();
            }
            else
            {
                labelMessage.Text = MessageManager.GetMessage("12");
            }
        }

        private void GetDataForUpdateAndDelete(int expenseID)
        {
            DataTable dt = new DataTable();
            dt = recpt.GetDisplayData(expenseID);
            if (dt.Rows.Count == 0)
                return;
            
            textBoxInvNumber.Text = DataFormat.GetString(dt.Rows[0][1]);
            textBoxInvNumber.Enabled = false;
            textBoxCustomerCode.Text = DataFormat.GetString(dt.Rows[0][2]);
            textBoxInvNumber.Text = DataFormat.GetString(dt.Rows[0][3]);
        }

        private void ClearFunction()
        {
            errorProvider1.Clear();
            textBoxInvNumber.Text = string.Empty;
            textBoxCustomerCode.Text = string.Empty;
            textBoxInvNumber.Text = string.Empty;
            textBoxInvNumber.Enabled = true;
            isSelectedForUpdate = false;
            recptId = 0;

            buttonAdd.Enabled = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
            labelMessage.Text = string.Empty;

            if (dataGridViewRecpt.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewRecpt.Rows)
                {
                    if (row.Selected)
                    {
                        row.Selected = false;
                        return;
                    }
                }
            }
            if (recptByUserID != 0)
                CheckPrevilleges();
        }

        private void CheckPrevilleges()
        {
            if (recptByUserID != SessionParameters.UserID)
            {
                buttonUpdate.Enabled = false;
                buttonDelete.Enabled = false;
                buttonDupe.Enabled = false;
                buttonAdd.Enabled = false;
                return;
            }
            else
            {
                buttonUpdate.Enabled = true;
                buttonDelete.Enabled = true;
                buttonDupe.Enabled = true;
                buttonAdd.Enabled = false;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                labelMessage.Text = MessageManager.GetMessage("4");
                return;
            }

            string message = string.Empty;
            string custCode = textBoxCustomerCode.Text.Trim();
            string custName = comboBoxCustomerName.Text.Trim();
            string invNumber = textBoxInvNumber.Text.Trim();
            string recptNumber = textBoxReciptNumber.Text.Trim();
            string amount = textBoxAmount.Text.Trim();
            string recptDate = reciptDatePicker.Value.ToString();

            if (custCode == string.Empty)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(textBoxCustomerCode, message);
                labelMessage.Text = message;
                return;
            }

            if (custName == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(comboBoxCustomerName, message);
                labelMessage.Text = message;
                return;
            }

            if (invNumber == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(textBoxInvNumber, message);
                labelMessage.Text = message;
                return;
            }

            if (recptNumber == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(textBoxReciptNumber, message);
                labelMessage.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(textBoxAmount, message);
                labelMessage.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(textBoxAmount, message);
                labelMessage.Text = message;
                return;
            }

            if (reciptDatePicker.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(reciptDatePicker, message);
                labelMessage.Text = message;
                return;
            }

            //Logger.WriteTrace("ExpenseDetails [Add new Procedure]", "User : " + SessionParameters.UserName + Environment.NewLine + "Item Id: " + itemId.ToString() + Environment.NewLine + "Description : " + textBoxProcName.Text.Trim() + Environment.NewLine + "Price : " + textBoxAmount.Text.Trim());
            AddRecipts(custName, custCode, invNumber, recptNumber, amount, recptDate, SessionParameters.UserID);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                labelMessage.Text = MessageManager.GetMessage("12");
                return;
            }

            if (SessionParameters.UserID != recptByUserID)
            {
                labelMessage.Text = MessageManager.GetMessage("4");
                return;
            }

            if (!isSelectedForUpdate)
            {
                labelMessage.Text = MessageManager.GetMessage("18");
                return;
            }

            int itemId = DataFormat.GetInteger(dataGridViewRecpt.SelectedRows[0].Cells[0].Value.ToString());
            string message = string.Empty;
            string custCode = textBoxCustomerCode.Text.Trim();
            string custName = comboBoxCustomerName.Text.Trim();
            string invNumber = textBoxInvNumber.Text.Trim();
            string recptNumber = textBoxReciptNumber.Text.Trim();
            string amount = textBoxAmount.Text.Trim();
            string recptDate = reciptDatePicker.Value.ToString();

            if (custCode == string.Empty)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(textBoxCustomerCode, message);
                labelMessage.Text = message;
                return;
            }

            if (custName == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(comboBoxCustomerName, message);
                labelMessage.Text = message;
                return;
            }

            if (invNumber == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(textBoxInvNumber, message);
                labelMessage.Text = message;
                return;
            }

            if (recptNumber == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(textBoxReciptNumber, message);
                labelMessage.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(textBoxAmount, message);
                labelMessage.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(textBoxAmount, message);
                labelMessage.Text = message;
                return;
            }

            if (reciptDatePicker.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(reciptDatePicker, message);
                labelMessage.Text = message;
                return;
            }

            //Logger.WriteTrace("ExpenseDetails [Update Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + recptId + Environment.NewLine + "Description : " + textBoxProcName.Text.Trim() + Environment.NewLine + "Price : " + textBoxAmount.Text.Trim());
            UpadteRecipts(itemId, custName, custCode, invNumber, recptNumber, amount, recptDate, SessionParameters.UserID);
            ClearFunction();           
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                labelMessage.Text = MessageManager.GetMessage("13");
                return;
            }

            if (!isSelectedForUpdate)
            {
                labelMessage.Text = MessageManager.GetMessage("14");
                return;
            }

            if (SessionParameters.UserID != recptByUserID)
            {
                labelMessage.Text = MessageManager.GetMessage("13");
                return;
            }

            if (MessageManager.DisplayMessage("15", new string[] { iHealthAccount.Configurations.ApplicationConfiguration.ExpenseCCY, dataGridViewRecpt.SelectedRows[0].Cells["ProcedureAmount"].Value.ToString(), dataGridViewRecpt.SelectedRows[0].Cells["Item"].Value.ToString() }, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Logger.WriteTrace("ExpenseDetails [Delete Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + recptId);
                recpt.DeleteRecipt(recptId);
                DisplayDataInGrid();
                ClearFunction();
            }            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearFunction();
        }

        private void buttonDupe_Click(object sender, EventArgs e)
        {

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            ImportDatabase();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            ExportDatabase();
        }

        #region Render
        
        public class iRenderer : ToolStripProfessionalRenderer
        {
            public iRenderer() : base(new iTheme()) { }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                ToolStripSeparator seperator = (ToolStripSeparator)e.Item;
                e.Graphics.FillRectangle(new SolidBrush(seperator.BackColor), 0, 0, seperator.Width, seperator.Height);

                if (seperator.Width > seperator.Height)
                    e.Graphics.DrawLine(new Pen(SystemColors.HotTrack), 4, seperator.Height / 2, seperator.Width - 4, seperator.Height / 2);
                else
                    e.Graphics.DrawLine(new Pen(SystemColors.HotTrack), seperator.Width / 2, 4, seperator.Width / 2, seperator.Height - 4);
            }

            protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
            {
                return;
            }
        }

        public class iTheme : ProfessionalColorTable
        {
            public override Color MenuBorder
            {
                get
                {
                    return SystemColors.HotTrack;
                }
            }

            public override Color MenuItemBorder
            {
                get
                {
                    return SystemColors.HotTrack;
                }
            }

            public override Color MenuItemSelected
            {
                get
                {
                    return SystemColors.HotTrack;
                }
            }

            public override Color ButtonSelectedBorder
            {
                get
                {
                    return SystemColors.HotTrack;
                }
            }

            public override Color ButtonPressedBorder
            {
                get
                {
                    return SystemColors.HotTrack;
                }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color ButtonPressedGradientBegin
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color ButtonPressedGradientEnd
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color ButtonSelectedGradientBegin
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color ButtonSelectedGradientEnd
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color SeparatorDark
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }

            public override Color SeparatorLight
            {
                get
                {
                    return SystemColors.Highlight;
                }
            }
        }

        #endregion        

        private void dataGridViewRecpt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            dataGridViewRecpt.Text = String.Empty;
            object value = dataGridViewRecpt.SelectedRows[0].Cells[0].Value;
            string strValue = (value == null) ? "1" : value.ToString();

            recptId = DataFormat.GetInteger(strValue);
            recptByUserID = DataFormat.GetInteger(dataGridViewRecpt.SelectedRows[0].Cells["UserId"].Value.ToString());
            GetDataForUpdateAndDelete(recptId);
            isSelectedForUpdate = true;

            CheckPrevilleges();
        }

        private void recptedureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Masters.ProcedureMaster recptMaster = new Masters.ProcedureMaster();
            recptMaster.Show();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Masters.PaymentMaster payMaster = new Masters.PaymentMaster();
            payMaster.Show();
        }

        private void recptomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Masters.CustomerMaster recptMaster = new Masters.CustomerMaster();
            recptMaster.Show();
        }

        private void expenseTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Masters.ExpenseMaster expMaster = new Masters.ExpenseMaster();
            expMaster.Show();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Details.InvoiceDetails invoice = new Details.InvoiceDetails();
            invoice.Show();
        }

        private void reciptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Details.ExpenseDetails recipt = new Details.ExpenseDetails();
            recipt.Show();
        }

        private void reciptWalkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Details.WalkInDetails walkIn = new Details.WalkInDetails();
            walkIn.Show();
        }

        private void expenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Details.ExpenseDetails expense = new Details.ExpenseDetails();
            expense.Show();
        }

        private void textBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true && e.KeyChar != '.' && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }

        private void textBoxReciptNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }
    }
}
