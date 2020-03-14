using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class iHome : Form
    {
        public iHome(Form baseForm)
        {
            InitializeComponent();
            this.baseForm = baseForm;
        }

        public iHome()
        {
            InitializeComponent();
        }

        private int invoiceId = 0;
        private int expenseByUserID = 0;
        private bool isSelectedForUpdate = false;
        private Invoice invoice = new Invoice();
        private Arch arch = new Arch();
        private DBHelper helper = new DBHelper();
        private HomeInvoiceReport homePageReport = new HomeInvoiceReport();
        private Form baseForm;
        private bool _isApplicationExit = false;

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reciptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reciptWalkInToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void expenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutAppToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                toolStripStatusLabel4.Text = MessageManager.GetMessage("4");
                return;
            }

            string amount = textBox2.Text.Trim();
            string invoiceedure = textBox21.Text.Trim();
            string message = string.Empty;

            if (invoiceedure == string.Empty)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(textBox21, message);
                toolStripStatusLabel4.Text = message;
                return;
            }

            if (amount == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(textBox2, message);
                toolStripStatusLabel4.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(textBox2, message);
                toolStripStatusLabel4.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(textBox2, message);
                toolStripStatusLabel4.Text = message;
                return;
            }

            if (dateTimePicker1.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(dateTimePicker1, message);
                toolStripStatusLabel4.Text = message;
                return;
            }
            
            AddInvoice(invoiceedure, textBox2.Text.Trim(), textBox2.Text.Trim(), SessionParameters.UserID, dateTimePicker1.Value.ToString());
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                toolStripStatusLabel4.Text = MessageManager.GetMessage("13");
                return;
            }

            if (!isSelectedForUpdate)
            {
                toolStripStatusLabel4.Text = MessageManager.GetMessage("14");
                return;
            }

            if (SessionParameters.UserID != expenseByUserID)
            {
                toolStripStatusLabel4.Text = MessageManager.GetMessage("13");
                return;
            }


            if (MessageManager.DisplayMessage("15", new string[] { iHealthAccount.Configurations.ApplicationConfiguration.ExpenseCCY, dataGridView1.SelectedRows[0].Cells["invoiceedureAmount"].Value.ToString(), dataGridView1.SelectedRows[0].Cells["Item"].Value.ToString() }, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Logger.WriteTrace("invoiceedureMaster [Delete Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + invoiceId);
                invoice.DeleteInvoices(invoiceId);
                DisplayDataInGrid();
                ClearFunction();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearFunction();
            isSelectedForUpdate = false;
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            toolStripStatusLabel4.Text = string.Empty;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {

        }

        private void AddInvoice(string invoiceCode, string expDesc, string expAmount, int expBy, string expDate)
        {
            Invoice objinvoice = new Invoice();
            bool addStatus = objinvoice.AddNewInvoice(invoiceCode, expDesc, expAmount, expBy, expDate);

            if (addStatus)
            {
                DisplayDataInGrid();
                objinvoice.UpdateProcId(invoiceCode, expDesc);
                ClearFunction();
                toolStripStatusLabel4.Text = MessageManager.GetMessage("9");
            }
            else
                toolStripStatusLabel4.Text = MessageManager.GetMessage("10");

        }

        private void UpdateInvoice(int invoiceId, string invoiceCode, string invoiceDesc, string invoiceAmount, int entryBy, string effectiveDate)
        {
            ClearMessage();
            if (invoice.ModifyInvoices(invoiceId, invoiceCode, invoiceDesc, invoiceAmount, entryBy, effectiveDate))
            {
                toolStripStatusLabel4.Text = MessageManager.GetMessage("11");
                DisplayDataInGrid();
                ClearFunction();
            }
            else
                toolStripStatusLabel4.Text = MessageManager.GetMessage("12");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            toolStripStatusLabel4.Text = String.Empty;
            object value = dataGridView1.SelectedRows[0].Cells[0].Value;
            string strValue = (value == null) ? "1" : value.ToString();

            invoiceId = DataFormat.GetInteger(strValue);
            expenseByUserID = DataFormat.GetInteger(dataGridView1.SelectedRows[0].Cells["UserId"].Value.ToString());
            GetDataForUpdateAndDelete(invoiceId);
            isSelectedForUpdate = true;

            if (expenseByUserID != SessionParameters.UserID)
            {
                buttonUpdate.Enabled = false;
                buttonDelete.Enabled = false;
                buttonAdd.Enabled = false;
                return;
            }
            else
            {
                buttonUpdate.Enabled = true;
                buttonDelete.Enabled = true;
                buttonAdd.Enabled = true;
            }
        }

        private void DisplayDataInGrid()
        {
            dataGridView1.DataSource = homePageReport.HomeInvoiceDetails();
            dataGridView1.Columns[0].Visible = false;
            toolStripStatusLabel5.Text = MessageManager.GetMessage("3", homePageReport.HomeInvoiceDetails().Rows.Count.ToString());
            toolStripStatusLabel5.Visible = true;
        }

        private void GetDataForUpdateAndDelete(int expenseID)
        {
            DataTable dt = new DataTable();
            dt = invoice.GetDisplayData(expenseID);
            if (dt.Rows.Count == 0)
                return;
            textBox1.Text = DataFormat.GetString(dt.Rows[0][1]);
            textBox1.Enabled = false;
            comboBox1.Text = DataFormat.GetString(dt.Rows[0][2]);
            label6.Text = DataFormat.GetString(dt.Rows[0][3]);
            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][4].ToString().Trim());
        }

        private void iHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isApplicationExit)
                if (!DoLogOff())
                    e.Cancel = true;
        }

        private void iHome_Load(object sender, EventArgs e)
        {
            InitScreenData();
            this.Text = ApplicationDetails.AssemblyProduct + " " + ApplicationDetails.AssemblyVersion;
            if (SessionParameters.UserRole == Common.UserRole.Admin)
                buttonAdd.Enabled = false;
        }

        private void InitScreenData()
        {
            DisplayDataInGrid();
            Arch arch = new Arch();
            SetStatusBarDetails();

            if (SessionParameters.UserRole != Common.UserRole.Admin)
                usersToolStripMenuItem.Enabled = false;
        }

        private void SetStatusBarDetails()
        {
            Users user = new Users();
            toolStripStatusLabel3.Text = "You are logged in as : " + SessionParameters.UserName;
            toolStripStatusLabel2.Text = "Last login : " + user.GetLastLastLoginDate(SessionParameters.UserID);
            toolStripStatusLabel1.Text = DataFormat.DateToDisp(System.DateTime.Now.ToShortDateString());
        }

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
          
        private void ClearMessage()
        {
            errorProvider1.Clear();
            toolStripStatusLabel4.Text = string.Empty;
        }

        private void ClearFunction()
        {
            errorProvider1.Clear();
            textBox21.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox2.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            textBox21.Enabled = true;
            isSelectedForUpdate = false;
            invoiceId = 0;

            buttonDelete.Enabled = false;
            buttonAdd.Enabled = true;
            buttonUpdate.Enabled = false;
            toolStripStatusLabel4.Text = string.Empty;

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        row.Selected = false;
                        return;
                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcedureMaster pm = new ProcedureMaster();
            pm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            iHome i = new iHome();
            i.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
        }
    }
}
