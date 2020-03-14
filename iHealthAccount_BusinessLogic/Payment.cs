using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;

namespace iHealthAccount.BusinessLogic
{
    public class Payment
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public bool AddNewPayment(string paymentMode, string paymentDesc, int entryBy)
        {
            string Query = "SELECT MAX([PaymentId]) FROM [PaymentMaster]";
            int paymentId = (int)_dbHelper.ExecuteScalar(Query);
            DBParameterCollection paramCollection = new DBParameterCollection();

            paramCollection.Add(new DBParameter("@paymentMode", paymentMode));
            paramCollection.Add(new DBParameter("@paymentDesc", paymentDesc));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@paymentId", paymentId + 1));

            Query = "INSERT INTO [PaymentMaster] ([PaymentMode],  [PaymentDesc] , " +
                " [EntryUser] ,  [EntryDate] ,  " +
                " [IsActive] , [PaymentId] ) " +
                "VALUES (@paymentMode, @paymentDesc, @entryBy, '" +
                DateTime.Now.ToString() + "' , 1, @paymentId ) "; 

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

        public bool ModifyPayment(int paymentId, string paymentMode, string paymentDesc, int entryBy)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@paymentMode", paymentMode));
            paramCollection.Add(new DBParameter("@paymentDesc", paymentDesc));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@paymentId", paymentId));

            string Query = "UPDATE [PaymentMaster] SET [PaymentMode] = @paymentMode , " +
            "[PaymentDescription] = @paymentDesc, " +
            "[EntryUser] = @entryBy, " +
            "WHERE [PaymentId]=@paymentId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;
        }

        public bool DeletePayment(int paymentId)
        {
            string Query = "UPDATE [PaymentMaster] SET [IsActive] = 2 WHERE PaymentId=" + paymentId.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;
        }

        public DataTable GetDisplayData(int paymentId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT [PaymentMaster].[PaymentId],[PaymentMaster].[PaymentMode],[PaymentMaster].[PaymentDesc] " +
                            " from [PaymentMaster] " +
                            "where [PaymentMaster].[PaymentId]=" + paymentId.ToString();
                        dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int paymentId)
        {
            string Query = "SELECT [FirstName] + ' ' + [LastName] from [UserDetails] where [UserId] IN (Select [EntryUser] from [PaymentMaster] Where [PaymentId]= " + paymentId.ToString() + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int paymentId)
        {
            string Query = "Select [EntryUser] from [PaymentMaster] Where [PaymentId]= " + paymentId.ToString();
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
