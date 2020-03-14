using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;
using iHealthAccount.Formatting;


namespace iHealthAccount.BusinessLogic
{
    public class Invoice
    {
        private DBHelper _dbHelper = new DBHelper();
        private Arch arch = new Arch();    

        public bool AddNewInvoice(string procCode, string procName,string procAmount, int entryBy, string effectiveDate)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@procCode", procCode));
            paramCollection.Add(new DBParameter("@procName", procName));
            paramCollection.Add(new DBParameter("@procAmount", procAmount));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@effectiveDate", effectiveDate, DbType.DateTime));

            string Query = "INSERT INTO ProcedureMaster (ProcedureCode,  ProcedureName , " +
                " ProcedureAmount,  EntryUser ,  EffectiveDate ,  " +
                " IsActive ) "  +
                "VALUES (@procCode, @procName, @procAmount, " +
                "@entryBy, @effectiveDate, 1)";

            bool retValue = _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;

            //if (retValue)
            //{
            //    paramCollection.RemoveAll();
            //    paramCollection.Add(new DBParameter("@procCode", procCode, DbType.String));
            //    paramCollection.Add(new DBParameter("@procName", procName, DbType.String));

            //    string sqlCommand = "SELECT * FROM ProcedureMaster WHERE ProcedureCode=@procCode AND ProcedureName=@procName AND IsActive=1";

            //    int procId = 0;

            //    DataTable data = _dbHelper.ExecuteDataTable(sqlCommand, paramCollection);

            //    if (data.Rows.Count > 0)
            //    {

            //        procId = DataFormat.GetInteger(data.Rows[0]["TranId"]);
            //        paramCollection.RemoveAll();

            //        paramCollection.Add(new DBParameter("@procId", procId));

            //        Query = "UPDATE ProcedureMaster SET ProcedureId = @procId WHERE TranId = @procId";
            //        _dbHelper.ExecuteNonQuery(Query, paramCollection);
            //    }

            //}

            return retValue;                       
        }
        public bool UpdateProcId(string procCode, string procName)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
          
            paramCollection.Add(new DBParameter("@procCode", procCode, DbType.String));
            paramCollection.Add(new DBParameter("@procName", procName, DbType.String));

            string sqlCommand = "SELECT * FROM ProcedureMaster WHERE ProcedureCode=@procCode AND ProcedureName=@procName AND IsActive=1";
             

            int procId = 0;
            bool retVal = false;
            DataTable data = _dbHelper.ExecuteDataTable(sqlCommand, paramCollection);

            if (data.Rows.Count > 0)
            {

                procId = DataFormat.GetInteger(data.Rows[0]["TranId"]);
                paramCollection.RemoveAll();
                paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("@procId", procId));
                paramCollection.Add(new DBParameter("@tranId", procId));
               string Query = "UPDATE ProcedureMaster SET ProcedureId = @procId WHERE TranId = @tranId";
               retVal = _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;
            }
            return retVal;
        }
        public bool ModifyInvoices(int procId, string procCode, string procName, string procAmount, int entryBy, string effectiveDate)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@procId", procId));
            paramCollection.Add(new DBParameter("@procCode", procCode));
            paramCollection.Add(new DBParameter("@procName", procName));
            paramCollection.Add(new DBParameter("@procAmount", procAmount));
            paramCollection.Add(new DBParameter("@entryBy", entryBy));
            paramCollection.Add(new DBParameter("@effectiveDate", effectiveDate, DbType.DateTime));

            string Query = "UPDATE ProcedureMaster SET ProcedureName = @procName , " +
            "ProcedureAmount = @procAmount, " +
            "EffectiveDate =@effectiveDate WHERE ProcedureId=@procId";

            return _dbHelper.ExecuteNonQuery(Query, paramCollection) > 0;             
        }

        public bool DeleteInvoices(int procId)
        {
            string Query = "UPDATE ProcedureMaster SET IsActive = 2 WHERE ProcedureId=" + procId.ToString();
            return _dbHelper.ExecuteNonQuery(Query) > 0;                
        }

        public DataTable GetDisplayData(int procId)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT ProcedureMaster.ProcedureId,ProcedureMaster.ProcedureCode,ProcedureMaster.ProcedureName,ProcedureMaster.ProcedureAmount, " +
                            "ProcedureMaster.EffectiveDate from ProcedureMaster " +
                            "where ProcedureMaster.ProcedureId=" + procId.ToString();

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }

        public string GetExpensedBy(int procId)
        {
            string Query = "SELECT FirstName + ' ' + LastName from UserDetails where UserId IN (Select EntryUser from ProcedureMaster Where ProcedureId= " + procId.ToString() + ")";
            return _dbHelper.ExecuteScalar(Query).ToString();
        }

        /// <summary>
        /// Returns User_Id
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public string GetExpBy(int procId)
        {
            string Query = "Select EntryUser from ProcedureMaster Where ProcedureId= " + procId.ToString();
            return _dbHelper.ExecuteScalar(Query).ToString();
        }
    }
}
