using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Utils
{
    public class DbUtil
    {
        
        public static SqlConnection getConn(String connStr)
        {
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }
        public static SqlCommand getCommand(String sql)
        {
            String connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = getConn(connStr);
            conn.Open();
            SqlCommand myCmd = new SqlCommand(sql, conn);
            return myCmd;
        }
        public static void close(SqlCommand cmd)
        {
            
            cmd.Connection.Close();
        }
    }
}
