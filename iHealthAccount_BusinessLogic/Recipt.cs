using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;

namespace iHealthAccount.BusinessLogic
{
    public class Recipt
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();

        public bool AddNewRecipt(string customerName, string customerCode, string invoiceNumber, string reciptNumber, string reciptAmount, string reciptDate, int entryBy)
        {
            string Query = "SELECT MAX([PaymentId]) FROM [PaymentMaster]";
            int reciptId = (int)_dbHelper.ExecuteScalar(Query);
            DBParameterCollection paramCollection = new DBParameterCollection();

            /*paramCollection.Add(new DBParameter("@paymentMode", paymentMode));
            paramCollection.Add(new DBParameter("@paymentDesc", paymentDesc));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@reciptId", reciptId + 1));*/

            Query = "INSERT INTO [PaymentMaster] ([PaymentMode],  [PaymentDesc] , " +
                " [EntryUser] ,  [EntryDate] ,  " +
                " [IsActive] , [PaymentId] ) " +
                "VALUES (@paymentMode, @paymentDesc, @entryBy, '" +
                DateTime.Now.ToString() + "' , 1, @reciptId ) "; 

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

        public bool ModifyRecipt(int reciptId, string customerName, string customerCode, string invoiceNumber, string reciptNumber, string reciptAmount, string reciptDate, int entryBy)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            /*paramCollection.Add(new DBParameter("@paymentMode", paymentMode));
            paramCollection.Add(new DBParameter("@paymentDesc", paymentDesc));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@reciptId", reciptId));*/

            string Query = "UPDATE [PaymentMaster] SET [PaymentMode] = @paymentMode , " +
            "[PaymentDescription] = @paymentDesc, " +
            "[EntryUser] = @entryBy, " +
            "WHERE [PaymentId]=@reciptId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;
        }

        public bool DeleteRecipt(int reciptId)
        {
            string Query = "UPDATE [PaymentMaster] SET [IsActive] = 2 WHERE PaymentId=" + reciptId.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;
        }

        public DataTable GetDisplayData(int reciptId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT [PaymentMaster].[PaymentId],[PaymentMaster].[PaymentMode],[PaymentMaster].[PaymentDesc] " +
                            " from [PaymentMaster] " +
                            "where [PaymentMaster].[PaymentId]=" + reciptId.ToString();
                        dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int reciptId)
        {
            string Query = "SELECT [FirstName] + ' ' + [LastName] from [UserDetails] where [UserId] IN (Select [EntryUser] from [PaymentMaster] Where [PaymentId]= " + reciptId.ToString() + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int reciptId)
        {
            string Query = "Select [EntryUser] from [PaymentMaster] Where [PaymentId]= " + reciptId.ToString();
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
