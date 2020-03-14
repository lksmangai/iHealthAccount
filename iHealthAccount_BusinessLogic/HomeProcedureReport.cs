using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomeProcedureReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomeProcedureDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT ProcedureMaster.ProcedureId,ProcedureMaster.ProcedureCode AS ProcedureCode, ProcedureMaster.ProcedureName AS Description, ProcedureMaster.ProcedureAmount AS Amount, UserDetails.FirstName AS EnteredBy, ProcedureMaster.EffectiveDate AS EffectiveDate, ProcedureMaster.EntryUser as UserId " +
                           "FROM ProcedureMaster, UserDetails " +
                           "WHERE ProcedureMaster.EntryUser=UserDetails.UserId And ProcedureMaster.IsActive<>0 ";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }

        public DataTable HomeProcedureReportData()
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
