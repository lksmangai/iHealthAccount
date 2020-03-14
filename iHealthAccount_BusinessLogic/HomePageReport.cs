using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomePageReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable  HomePageDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT ExpenseDetails.ExpenseId,ExpenseMaster.ItemName AS Item, ExpenseDetails.ExpenseCode AS Details, ExpenseDetails.ExpenseAmount AS Amount, UserDetails.FirstName AS ExpensedBy, ExpenseDetails.PaymentDate AS ExpDate, ExpenseDetails.EntryUser as UserId " +
                           "FROM ExpenseDetails, ExpenseMaster, UserDetails " +
                           "WHERE ExpenseDetails.ItemId=ExpenseMaster.ItemId And ExpenseDetails.EntryUser=UserDetails.UserId And ExpenseDetails.IsActive<>0 ";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }

        public DataTable HomePageReportData()
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
