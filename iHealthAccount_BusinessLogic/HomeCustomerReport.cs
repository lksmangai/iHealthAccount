using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomeCustomerReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomeCustomerDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT [CustomerMaster].[CustomerId], [CustomerMaster].[CustomerCode] AS [CustomerCode], [UserDetails].[FirstName] AS EnteredBy, [CustomerMaster].[FirstName] AS [FirstName],  [CustomerMaster].[LastName] AS [LastName],  [CustomerMaster].[EntryUser] as [UserId] " +
                           "FROM [CustomerMaster], [UserDetails] " +
                           "WHERE [CustomerMaster].[EntryUser]=[UserDetails].[UserId] And [CustomerMaster].[IsActive]<>0 ";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }

        public DataTable HomeCustomerReportData()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Distinct(EntryUser) as UserId,UserDetails.FirstName as EnteredBy, Sum(ProcedureAmount) as Amount from " +
            "ProcedureMaster,UserDetails Where " +
            "ProcedureMaster.EntryUser=UserDetails.UserId AND " +
            "ProcedureMaster.IsActive<>0 " +
            "Group by EntryUser, UserDetails.FirstName";

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }
    }
}
