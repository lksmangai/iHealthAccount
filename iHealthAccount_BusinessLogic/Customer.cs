using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;

namespace iHealthAccount.BusinessLogic
{
    public class Customer
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public bool AddNewCustomer(string custCode, string firstName, string middleName, string lastName, int entryBy)
        {
            string Query = "SELECT MAX([CustomerId]) FROM [CustomerMaster]";
            int customerId = (int)_dbHelper.ExecuteScalar(Query);

            DBParameterCollection paramCollection = new DBParameterCollection();

            paramCollection.Add(new DBParameter("@custCode", custCode));
            paramCollection.Add(new DBParameter("@firstName", firstName));
            paramCollection.Add(new DBParameter("@middleName", middleName));
            paramCollection.Add(new DBParameter("@lastName", lastName));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@custId", customerId + 1));

            Query = "INSERT INTO [CustomerMaster] ([CustomerCode],  [FirstName] , " +
                " [MiddleName], [LastName], [EntryUser] ,  [EntryDate] ,  " +
                " [IsActive] , [CustomerId] ) " +
                "VALUES (@custCode, @firstName, @middleName, @lastName, @entryBy, '"+
                DateTime.Now.ToString() + "' , 1, @customerId ) "; 

            bool retValue = _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;

            //if (retValue)
            //{
            //    paramCollection.RemoveAll();
            //    paramCollection.Add(new DBParameter("@procCode", procCode, DbType.String));
            //    paramCollection.Add(new DBParameter("@procName", procName, DbType.String));

            //    string sqlCommand = "SELECT * FROM ProcedureMaster WHERE ProcedureCode=@procCode AND ProcedureName=@procName AND IsActive=1";

            //    int custId = 0;

            //    DataTable data = _dbHelper.ExecuteDataTable(sqlCommand, paramCollection);

            //    if (data.Rows.Count > 0)
            //    {

            //        custId = DataFormat.GetInteger(data.Rows[0]["TranId"]);
            //        paramCollection.RemoveAll();

            //        paramCollection.Add(new DBParameter("@custId", custId));

            //        Query = "UPDATE ProcedureMaster SET ProcedureId = @custId WHERE TranId = @custId";
            //        _dbHelper.ExecuteNonQuery(Query, paramCollection);
            //    }

            //}

            return retValue;
        }

        //public bool UpdatecustId(string procCode, string procName)
        //{
        //    return true;
        //    DBParameterCollection paramCollection = new DBParameterCollection();

        //    paramCollection.Add(new DBParameter("@procCode", procCode, DbType.String));
        //    paramCollection.Add(new DBParameter("@procName", procName, DbType.String));

        //    string sqlCommand = "SELECT TranId FROM ProcedureMaster WHERE ProcedureCode=@procCode AND ProcedureName=@procName";
        //    sqlCommand = "SELECT TranId FROM ProcedureMaster WHERE ProcedureCode="+procCode+" AND ProcedureName="+procName;
        //    sqlCommand = "SELECT TranId FROM ProcedureMaster WHERE ProcedureCode=\"" + procCode + "\" AND ProcedureName=\"" + procName+"\"";


        //    int custId = 0;
        //    bool retVal = false;
        //    DataTable data;// = _dbHelper.ExecuteDataTable(sqlCommand, paramCollection);
        //    data = _dbHelper.ExecuteDataTable(sqlCommand); //, paramCollection);

        //    if (data.Rows.Count > 0)
        //    {

        //        custId = DataFormat.GetInteger(data.Rows[0]["TranId"]);
        //       // paramCollection.RemoveAll();
        //        paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("@custId", custId));
        //        paramCollection.Add(new DBParameter("@tranId", custId));
        //       string Query = "UPDATE ProcedureMaster SET ProcedureId = @custId WHERE TranId = @tranId";
        //       retVal = _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;
        //    }
        //    return retVal;
        //}

        public bool ModifyCustomers(int custId, string custCode, string firstName, string middleName, string lastName, int entryBy)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@custCode", custCode));
            paramCollection.Add(new DBParameter("@firstName", firstName));
            paramCollection.Add(new DBParameter("@middleName", middleName));
            paramCollection.Add(new DBParameter("@lastName", lastName));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@custId", custId));

            string Query = "UPDATE [CustomerMaster] SET [FirstName] = @firstName , " +
            "[MiddleName] = @middleName, " + "[LastName] = @lastName, " +
            "[EntryBy] = @entryBy, " + 
            "WHERE [CustomerId]=@custId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;
        }

        public bool DeleteCustomers(int custId)
        {
            string Query = "UPDATE [CustomerMaster] SET [IsActive] = 2 WHERE CustomerId=" + custId.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;
        }

        public DataTable GetDisplayData(int custId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT [CustomerMaster].[CustomerId],[CustomerMaster].[CustomerCode],[CustomerMaster].[FirstName],[CustomerMaster].[MiddleName], [CustomerMaster].[LastName] " +
                            " from [CustomerMaster] " +
                            "where [CustomerMaster].[CustomerId]=" + custId.ToString();

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int custId)
        {
            string Query = "SELECT [FirstName] + ' ' + [LastName] from [UserDetails] where [UserId] IN (Select [EntryUser] from [CustomerMaster] Where [CustomerId]= " + custId.ToString() + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int custId)
        {
            string Query = "Select [EntryUser] from [CustomerMaster] Where [CustomerId]= " + custId.ToString();
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
