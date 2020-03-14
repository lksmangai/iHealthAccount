using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;

namespace iHealthAccount.BusinessLogic
{
    public class UserRoleRights
    {
        public int RoleId;
        public string Role;
        public DataTable RoleRights;
        private DBHelper _dbHelper = new DBHelper();

        public DataTable GetRoleRights()
        {
            string Query = "SELECT Expense_Details.Exp_Id,Item_Details.Item_Name,Expense_Details.Exp_Desc,Expense_Details.Exp_Amount, " +
                           "Expense_Details.Exp_Date from Expense_Details,Item_Details " +
                           "where Expense_Details.Item_Id=Item_Details.Item_Id  AND  Expense_Details.Exp_Id=" + Role;

            DataTable dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }
    }
}
