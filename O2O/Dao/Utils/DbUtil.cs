using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Utils
{
    class DbUtil
    {
        private static SqlConnection conn;
        public static SqlCommand getCommand(String sql)
        {
            String connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand myCmd = new SqlCommand(sql, conn);
            return myCmd;
        }
        public static void close()
        {
            conn.Close();
        }
    }
}
