using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using System.IO;

namespace iHealthAccount.BusinessLogic
{
    public class HomeInvoiceReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomeInvoiceDetails()
        {
            WriteNamesToFile("CustomerMaster", @"c:\gloStream\");
            WriteNamesToFile("ExpenseDetails", @"c:\gloStream\");
            WriteNamesToFile("ExpenseMaster", @"c:\gloStream\");
            WriteNamesToFile("InvoiceDetails", @"c:\gloStream\");
            WriteNamesToFile("LoginDetails", @"c:\gloStream\");
            WriteNamesToFile("MenuNameMaster", @"c:\gloStream\");
            WriteNamesToFile("MenuRightsMaster", @"c:\gloStream\");
            WriteNamesToFile("PaymentMaster", @"c:\gloStream\");
            WriteNamesToFile("ProcedureMaster", @"c:\gloStream\");
            WriteNamesToFile("ReciptDetails", @"c:\gloStream\");
            WriteNamesToFile("RoleDetails", @"c:\gloStream\");
            WriteNamesToFile("UserDetails", @"c:\gloStream\");
            WriteNamesToFile("WalkinDetails", @"c:\gloStream\");
            DataTable data = new DataTable();
            string Query = "SELECT InvoiceDetails.InvoiceId " + //, InvoiceDetails.Proc1, InvoiceDetails.Proc2, InvoiceDetails.Proc3, InvoiceDetails.Proc4, InvoiceDetails.Proc5, InvoiceDetails.Proc6, InvoiceDetails.Proc7, InvoiceDetails.Proc8, InvoiceDetails.Proc9, InvoiceDetails.Proc10, InvoiceDetails.Amt1, InvoiceDetails.Amt2, InvoiceDetails.Amt3, InvoiceDetails.Amt4, InvoiceDetails.Amt5, InvoiceDetails.Amt6, InvoiceDetails.Amt7, InvoiceDetails.Amt8, InvoiceDetails.Amt9, InvoiceDetails.Amt10, InvoiceDetails.ApprovalCode, InvoiceDetails.InvoiceDate, InvoiceDetails.ServiceDate, InvoiceDetails.InvoiceNumber, InvoiceDetails.Discount, InvoiceDetails.InvoiceAmount, InvoiceDetails.EntryUser, InvoiceDetails.EntryDate, InvoiceDetails.IsActive, UserDetails.FirstName " +
                           "FROM InvoiceDetails ";//, UserDetails "; //+
                           //"WHERE InvoiceDetails.EntryUser=UserDetails.UserId And InvoiceMaster.IsActive<>0";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }
        public void WriteNamesToFile(string TableName, string FileName)
        {
            string FileNameToWrite = FileName + TableName + ".txt";
            DataTable data = new DataTable();
            string Query = "SELECT * FROM " + TableName;
            data = _dbHelper.ExecuteDataTable(Query);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                if (i == 0)
                {
                    File.WriteAllText(FileNameToWrite, "S.No \t FieldName \t Type \t MaxLen\r\n");
                }
                 
                File.AppendAllText(FileNameToWrite,(i+1).ToString()+"\t"+ data.Columns[i].ColumnName + "\t" + data.Columns[i].DataType.ToString() + "\t" + data.Columns[i].MaxLength.ToString() + "\r\n");
                 
            }
        }
        public DataTable HomeInvoiceReportData()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Distinct(EntryUser) as UserId,UserDetails.FirstName as EntryUser, Sum(InvoiceAmount) as Amount from " +
            "InvoiceDetails,UserDetails Where " +
            "InvoiceDetails.EntryUser=UserDetails.UserId AND " +
            "InvoiceDetails.IsActive<>0 " +
            "Group by EntryUser, UserDetails.FirstName";

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }
    }
}
