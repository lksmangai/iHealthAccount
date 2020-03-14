using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

namespace iHealthAccount.DataAccess
{
    /// <summary>
    /// ConnectionManager takes care of establishing the connection to the database parameters specified into web.config or app.config file.
    /// </summary>
    internal class ConnectionManager
    {

        /// <summary>
        /// Establish Connection to the database and Return an open connection.
        /// </summary>
        /// <returns>Open connection to the database</returns>
        internal IDbConnection GetConnection()
        {
            IDbConnection connection = null;
            string connectionString = Configuration.ConnectionString;

            try
            {

                switch (Configuration.DBProvider.Trim().ToUpper())
                {
                    case Common.SQL_SERVER_DB_PROVIDER:
                        connection = new SqlConnection(connectionString);
                        break;
                    case Common.ORACLE_DB_PROVIDER:
                        connection = new OracleConnection(connectionString);
                        break;
                    case Common.EXCESS_DB_PROVIDER:
                        connection = new OleDbConnection(connectionString);
                        break;
                    case Common.ODBC_DB_PROVIDER:
                        connection = new OdbcConnection(connectionString);
                        break;
                    case Common.OLE_DB_PROVIDER:
                        connection = new OleDbConnection(connectionString);
                        break;
                }

                try
                {
                    connection.Open();
                }
                catch (Exception err)
                {
                    throw err;
                }

                return connection;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return null;
        }
    }
}
