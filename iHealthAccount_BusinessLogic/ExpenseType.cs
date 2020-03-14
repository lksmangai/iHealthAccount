using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;


namespace iHealthAccount.BusinessLogic
{
    public class ExpenseType
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public bool AddNewExpense(string itemName, string itemDescription, string amount, int entryBy)
        {
            string Query = "SELECT MAX([ItemId]) FROM [ExpenseMaster]";
            int itemId = (int)_dbHelper.ExecuteScalar(Query);

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemName", itemName));
            paramCollection.Add(new DBParameter("@itemDescription", itemDescription));
            paramCollection.Add(new DBParameter("@amount", amount));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@itemId", itemId + 1));

            Query = "INSERT INTO [ExpenseMaster] ([ItemName],  [ItemDesc] , [ItemAmount], " +
                " [EntryUser] , [EntryDate] ,  " +
                " [IsActive], [ItemId] ) " +
                "VALUES (@itemName, @itemDescription, @amount, @entryBy, '" +
                DateTime.Now.ToString() + "', 1, @itemId)";
            
            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;                
        }

        public bool ModifyExpenses(int expenseId, string itemName, string itemDescription, string amount, int entryBy)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@itemName", itemName));
            paramCollection.Add(new DBParameter("@itemDescription", itemDescription));
            paramCollection.Add(new DBParameter("@amount", amount));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@expenseId", expenseId));

            string Query = "UPDATE [ExpenseMaster] SET [ItemName] = @itemName , [ItemDesc] = @itemDescription , [ItemAmount] =  @amount, [EntryUser]=@entryBy " +
            "WHERE [ExpenseId]=@expenseId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;             
        }

        public bool DeleteExpenses(int expenseID)
        {
            string Query = "UPDATE ExpenseMaster SET IsActive = 2 WHERE ExpenseId=" + expenseID.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;                
        }

        public DataTable GetDisplayData(int expenseID)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT [ExpenseMaster].[ItemId],[ExpenseMaster].[ItemName],[ExpenseMaster].[ItemDesc],[ExpenseMaster].[ItemAmount] FROM [ExpenseMaster] " +                            
                           "WHERE [ExpenseMaster].[ItemId]=" + expenseID.ToString();

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int expenseID)
        {
            string Query = "SELECT [FirstName]+ ' ' + [LastName] from [UserDetails] where [UserId] IN (Select [EntryUser] from [ExpenseMaster] Where [ItemId]= " + expenseID.ToString()  + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int expenseID)
        {
            string Query = "Select [EntryUser] from [ExpenseMaster] Where [ExpenseId]= " + expenseID.ToString() ;
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
