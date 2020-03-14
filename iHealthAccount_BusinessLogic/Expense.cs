using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;


namespace iHealthAccount.BusinessLogic
{
    public class Expense
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();
    

        public bool AddNewExpense(int itemID, string expenseDesc,string expenseAmount, int expenseBy, string expenseDate)
        {
            

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemID", itemID));
            paramCollection.Add(new DBParameter("@expenseDesc", expenseDesc));
            paramCollection.Add(new DBParameter("@expenseAmount", expenseAmount));
            paramCollection.Add(new DBParameter("@expenseBy", expenseBy));
            paramCollection.Add(new DBParameter("@expenseDate", expenseDate, DbType.DateTime));
           

            string Query = "INSERT INTO [ExpenseDetails] ([ItemId],  [ExpenseDescription] , " +
                " [PaymentAmount],  [EntryUser] ,  [PaymentDate] , [EntryDate] ,  " +
                " [IsActive] ) "  + 
                "VALUES (@itemId, @expenseDesc, @expenseAmount, " +
                "@expenseBy, @expenseDate, '" +
                DateTime.Now.ToString() + "' 1)";
            
            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                
        }

        public bool ModifyExpenses(int itemId, int expenseID, string expenseDesc, string expenseAmount, string expenseDate)
        {
           
            
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@expenseDesc", expenseDesc));
            paramCollection.Add(new DBParameter("@expenseAmount", expenseAmount));
            paramCollection.Add(new DBParameter("@expDate", expenseDate, DbType.DateTime ));
            paramCollection.Add(new DBParameter("@itemId", itemId));
           
            paramCollection.Add(new DBParameter("@expenseID", expenseID));
            
            string Query = "UPDATE [ExpenseDetails] SET [ExpenseDescription] = @expenseDesc , " +
            "[PaymentAmount] = @expenseAmount, " +
            "[PaymentDate] =@expDate, [ItemId] =@itemId WHERE [ExpenseId]=@expenseID";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;             
        }

        public bool DeleteExpenses(int expenseID)
        {           
            string Query = "UPDATE ExpenseDetails SET IsActive = 2 WHERE ExpenseId=" + expenseID.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;                
        }

        public DataTable GetDisplayData(int expenseID)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT [ExpenseDetails].[ExpenseId],[ExpenseMaster].[ItemName],[ExpenseDetails].[ExpenseDescription],[ExpenseDetails].[ExpenseAmount], " +                            
                            "[ExpenseDetails].[PaymentDate] from [ExpenseDetails],[ExpenseMaster] " +
                            "where [ExpenseDetails].[ItemId]=[ExpenseMaster].[ItemId]  AND  [ExpenseDetails].[ExpenseId]=" + expenseID.ToString();

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int expenseID)
        {
            string Query = "SELECT [FirstName]+ ' ' + [LastName] from [UserDetails] where [UserId] IN (Select [EntryUser] from [ExpenseDetails] Where [ExpenseId]= " + expenseID.ToString()  + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int expenseID)
        {
            string Query = "Select [EntryUser] from [ExpenseDetails] Where [ExpenseId]= " + expenseID.ToString() ;
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
