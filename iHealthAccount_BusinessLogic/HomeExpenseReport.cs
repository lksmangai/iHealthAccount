using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomeExpenseReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomePageDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT ExpenseDetails.ExpenseId,ExpenseMaster.ExpenseCode, ExpenseDetails.ExpenseDescription, ExpenseDetails.ExpenseAmount, ExpenseDetails.PaymentMode, ExpenseDetails.PaymentDate, ExpenseDetails.ChequeNumber, ExpenseDetails.MpesaPayment, UserDetails.FirstName AS EntryUser, ExpenseDetails.EntryUser as UserId " +
                           "FROM ExpenseDetails, UserDetails " +
                           "WHERE ExpenseDetails.EntryUser=UserDetails.UserId And ExpenseDetails.IsActive<>0 ";
         
            
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
