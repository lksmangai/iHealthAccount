using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomeExpenseTypeReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomeExpenseTypeDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT ExpenseMaster.ItemId, ExpenseMaster.ItemName,ExpenseMaster.ItemDesc As ItemDescription, ExpenseMaster.ItemAmount, UserDetails.FirstName AS EnteredBy,  ExpenseMaster.EntryUser as UserId " +
                           "FROM ExpenseMaster, UserDetails " +
                           "WHERE ExpenseMaster.EntryUser=UserDetails.UserId And ExpenseMaster.IsActive<>0 ";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }

        public DataTable HomeExpenseTypeReportData()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Distinct(EntryUser) as UserId,UserDetails.FirstName as ExpencedBy, Sum(ExpenseAmount) as Amount from " +
            "ExpenseDetails,UserDetails Where " +
            "ExpenseDetails.EntryUser=UserDetails.UserId AND " +
            "ExpenseDetails.IsActive<>0 " +
            "Group by EntryUser, UserDetails.FirstName";

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }
    }
}
