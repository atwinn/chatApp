using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace connectSQL
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"GodLong\SQLEXPRESS";

            string database = "chat";
            string username = "sa";
            string password = "sa";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }

    }
}
