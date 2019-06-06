using Dao.Utils;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class UserCompleteRequirementDao
    {
        //插入需求
        public Boolean insertUserCompleteRequirement(UserCompleteRequirement completerequirement)
        {
            String sql = "INSERT INTO tb_complete_requirement(user_id,requirement_id,complete_time) VALUES(@user_id,@requirement_id,@complete_time)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@user_id", completerequirement.User));
            cmd.Parameters.Add(new SqlParameter("@requirement_id", completerequirement.Requirement));
            cmd.Parameters.Add(new SqlParameter("@complete_time", completerequirement.CompleteTime));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //查询某个用户（ID）的所有完成的需求
        public List<UserCompleteRequirement> queryUserCompleteRequirementByUserId(int userId)
        {
            List<UserCompleteRequirement> list = new List<UserCompleteRequirement>();
            String sql = "SELECT * FROM tb_complete_requirement WHERE user_id = @user_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@user_id", userId));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    UserCompleteRequirement completerequirement = new UserCompleteRequirement();
                    completerequirement.User.Id = sdr.GetInt32(0);
                    completerequirement.Requirement.Id = sdr.GetInt32(1);
                    completerequirement.CompleteTime = sdr.GetDateTime(2);
                    list.Add(completerequirement);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
    }
}