using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public class HomePaymentReport
    {
        private DBHelper _dbHelper = new DBHelper();

        public DataTable HomeCustomerDetails()
        {            
            DataTable data = new DataTable();
            string Query = "SELECT PaymentMaster.PaymentId,PaymentMaster.PaymentMode AS PaymentMode, PaymentMaster.PaymentDesc AS Description, UserDetails.FirstName AS EnteredBy, PaymentMaster.EntryUser as UserId " +
                           "FROM PaymentMaster, UserDetails " +
                           "WHERE PaymentMaster.EntryUser=UserDetails.UserId And PaymentMaster.IsActive<>0 ";
         
            
            data = _dbHelper.ExecuteDataTable(Query);                   
            return data;
        }

        public DataTable HomeCustomerReportData()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT Distinct(EntryUser) as UserId,UserDetails.FirstName as EnteredBy from " +
            "PaymentMaster,UserDetails Where " +
            "PaymentMaster.EntryUser=UserDetails.UserId AND " +
            "PaymentMaster.IsActive<>0 " +
            "Group by EntryUser, UserDetails.FirstName";

            dt = _dbHelper.ExecuteDataTable(Query);
            return dt;
        }
    }
}
