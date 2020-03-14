using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomeWalkInReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomeWalkInDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT [ReciptDetails].[CustomerId], [ReciptDetails].[CustomerCode] AS [CustomerCode], [ReciptDetails].[CustomerName] AS CustomerName, [ReciptDetails].[InvoiceNumber] AS [InvoiceNumber],  [ReciptDetails].[ReciptNumber] AS [ReciptNumber], [ReciptDetails].[ReciptAmount] AS [ReciptAmount],  [ReciptDetails].[EntryUser] as [UserId] " +
                           "FROM [ReciptDetails], [UserDetails] " +
                           "WHERE [ReciptDetails].[EntryUser]=[UserDetails].[UserId] And [CustomerMaster].[IsActive]<>0 ";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }

        public DataTable HomeWalkInReportData()
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
