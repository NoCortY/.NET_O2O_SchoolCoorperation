using Dao.Utils;
using o2o.entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class UserDao
    {
       
        public User queryUserById(int id)
        {
            User user = new User();
            String sql = "SELECT * FROM tb_user WHERE id = @id and username = @username";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id",id));
            SqlDataReader sdr =cmd.ExecuteReader();
            user.Id = sdr.GetInt32(1);
            user.Username = sdr.GetString(2);
            DbUtil.close();
            return user;

        }
        public Boolean updateUser(User user)
        {
            String sql = "UPDATE tb_user SET username = @username,password=@password";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", user.Username));
            cmd.Parameters.Add(new SqlParameter("@password",user.Password));
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                return true;
            else
                return false;
        }
    }
}
