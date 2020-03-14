using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class EditUsers : Form
    {
        public EditUsers()
        {
            InitializeComponent();
            InitializeIcon();
            InitScreenData();
        }

        private int userId = 0;

        private void InitializeIcon()
        {
            this.Icon = Icon.FromHandle(Resources.user_id_icon.GetHicon());
        }

        private void InitScreenData()
        {
            (new Arch()).FillDataInCombo(comboBoxRole, Arch.ComboBoxItem.Role);
            comboBoxRole.SelectedIndex = 0;
            GetData(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private void AddUpdateUser()
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            BusinessLogic.Users user = new BusinessLogic.Users();
            string userName = textBoxUsername.Text.Trim();
            string firstName = textBoxFirstName.Text.Trim();
            string lastName = textBoxLastName.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string mobile = textBoxMobile.Text.Trim();
            string password = textBoxPassword.Text.Trim();
            string confirmPassword = textBoxConPassword.Text.Trim();
            int roleId = DataFormat.GetInteger(((DictionaryEntry)(comboBoxRole.SelectedItem)).Key);

            string message = string.Empty;

            if (SessionParameters.UserRole != Common.UserRole.Admin)
            {
                labelMessage.Text = MessageManager.GetMessage("4");
                return;
            }

            if (roleId == -1)
            {
                message = MessageManager.GetMessage("19");
                errorProvider1.SetError(comboBoxRole, message);
                labelMessage.Text = message;
                return;
            }

            if (userName == string.Empty)
            {
                message = MessageManager.GetMessage("20");
                errorProvider1.SetError(textBoxUsername, message);
                labelMessage.Text = message;
                return;
            }
            else
            {
                if (!textBoxUsername.ReadOnly)
                    if (user.CheckUserExist(userName))
                    {
                        message = MessageManager.GetMessage("27", userName);
                        errorProvider1.SetError(textBoxUsername, message);
                        return;
                    }
            }

            if (password == string.Empty)
            {
                message = MessageManager.GetMessage("21");
                errorProvider1.SetError(textBoxPassword, message);
                labelMessage.Text = message;
                return;
            }

            if (confirmPassword == string.Empty)
            {
                message = MessageManager.GetMessage("22");
                errorProvider1.SetError(textBoxConPassword, message);
                labelMessage.Text = message;
                return;
            }

            if (firstName == string.Empty)
            {
                message = MessageManager.GetMessage("23");
                errorProvider1.SetError(textBoxFirstName, message);
                labelMessage.Text = message;
                return;
            }

            if (string.Compare(password, confirmPassword, true) != 0)
            {
                message = MessageManager.GetMessage("24");
                errorProvider1.SetError(textBoxPassword, message);
                errorProvider1.SetError(textBoxConPassword, message);
                labelMessage.Text = message;
                return;
            }

            if (userId != 0)
            {
                if (user.UpdateUserDetails(userId, roleId, password, firstName, lastName, email, mobile, checkBoxActive.Checked))
                {
                    GetData(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    Clear();
                    labelMessage.Text = MessageManager.GetMessage("25");
                }
                else
                {
                    labelMessage.Text = MessageManager.GetMessage("26");
                }
            }
            else
            {
                if (user.CreateNewUser(roleId, userName, password, firstName, lastName, email, mobile, checkBoxActive.Checked))
                {
                    GetData(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    labelMessage.Text = string.Empty;
                    labelMessage.Text = MessageManager.GetMessage("28", userName);
                }
                else
                    labelMessage.Text = MessageManager.GetMessage("29", userName);
            }

        }

        private void GetData(int roleId, string userName, string firstName, string lastName, string email, string mobile)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;
            StringBuilder sqlCommand = new StringBuilder("SELECT U.UserId, U.Password , U.UserName,U.RoleId, R.Role, U.FirstName, U.LastName, U.EMail, U.Mobile, U.IsActive ");
            sqlCommand.Append("FROM UserDetails AS U, RoleDetails AS R ");
            sqlCommand.Append("WHERE U.RoleId = R.RoleId ");

            if (userName != string.Empty)
                sqlCommand.Append("AND U.UserName LIKE '" + userName + "'");

            if (firstName != string.Empty)
                sqlCommand.Append("AND U.FirstName LIKE '" + firstName + "' ");

            if (lastName != string.Empty)
                sqlCommand.Append("AND U.LastName LIKE '" + lastName + "' ");

            if (roleId != -1)
                sqlCommand.Append("AND U.RoleId = " + roleId.ToString());

            if (email != string.Empty)
                sqlCommand.Append("AND U.Email LIKE '" + email + "'");

            if (mobile != string.Empty)
                sqlCommand.Append("AND U.Mobile = '" + mobile + "'");

            dataGridViewUsers.AutoGenerateColumns = false;
            dataGridViewUsers.DataSource = (new DataAccess.DBHelper()).ExecuteDataTable(sqlCommand.ToString());

        }

        private void CreaNewUser(int roleId, string userName, string password, string confirmPassword, string firstName, string lastName, string email, string mobile, bool isActive)
        {
            BusinessLogic.Users objUsers = new BusinessLogic.Users();

            if (objUsers.CreateNewUser(roleId, userName, password, firstName, lastName, email, mobile, isActive))
                labelMessage.Text = MessageManager.GetMessage("28", userName);
            else
                labelMessage.Text = MessageManager.GetMessage("29", userName);
        }
        
        private void Clear()
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;
            textBoxConPassword.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPassword.Clear();
            textBoxUsername.Clear();
            textBoxEmail.Clear();
            textBoxMobile.Clear();
            textBoxUsername.ReadOnly = false;

            buttonUpdate.Enabled = false;
            buttonAdd.Enabled = true;
            buttonSearch.Enabled = true;
            comboBoxRole.SelectedIndex = 0;
            userId = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddUpdateUser();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            AddUpdateUser();  
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            GetData(DataFormat.GetInteger(((System.Collections.DictionaryEntry)(comboBoxRole.SelectedItem)).Key), textBoxUsername.Text.Trim(), textBoxFirstName.Text.Trim(), textBoxLastName.Text.Trim(), textBoxEmail.Text.Trim(), textBoxMobile.Text.Trim());
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            labelMessage.Text = string.Empty;

            if (e.RowIndex == -1)
                return;

            int userId = DataFormat.GetInteger(dataGridViewUsers.Rows[e.RowIndex].Cells["User"].Value);
            this.userId = userId;
            string password = (new DataSecurity()).Decrypt(DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["Password"].Value));

            textBoxUsername.Text = DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["UserName"].Value);
            textBoxFirstName.Text = DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["FirstName"].Value);
            textBoxLastName.Text = DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["LastName"].Value);
            textBoxEmail.Text = DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["Email"].Value);
            textBoxMobile.Text = DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["Mobile"].Value);
            textBoxPassword.Text = password;
            textBoxConPassword.Text = password;
            checkBoxActive.Checked = DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["IsActive"].Value) == "0" ? false : true;
            comboBoxRole.SelectedItem = new DictionaryEntry(DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["RoleId"].Value), DataFormat.GetString(dataGridViewUsers.Rows[e.RowIndex].Cells["Role"].Value));

            buttonUpdate.Enabled = true;
            textBoxUsername.ReadOnly = true;
            buttonAdd.Enabled = false;
            buttonSearch.Enabled = false;
        }

        private void textBoxMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }
    }
}
