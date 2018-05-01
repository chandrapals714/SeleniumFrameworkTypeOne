using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Dynamic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Personify.Helpers
{
   
    public class SqlHelper
    {
        public static string ConnectionString = "";

        public static DataSet GetData(string query, string connectionString = "")
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                connectionString = ConnectionString;
            }

            var retData = new DataSet();
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                WaitHelpers.ExplicitWait();
                var adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(retData);
            }

            return retData;
        }
    }
}