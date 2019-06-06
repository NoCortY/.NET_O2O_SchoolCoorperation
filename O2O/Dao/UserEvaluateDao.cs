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
    public class UserEvaluateDao
    {
        //添加用户评价
        public Boolean insertUserEvaluate(UserEvaluate userEvaluate)
        {
            String sql = "INSERT INTO tb_user_evaluate(receive_user_id,send_user_id,evaluate_content) VALUES(@receive_user_id,@send_user_id,@evaluate_content)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@receive_user_id", userEvaluate.ReceiveUser));
            cmd.Parameters.Add(new SqlParameter("@send_user_id", userEvaluate.SendUser));
            cmd.Parameters.Add(new SqlParameter("@evaluate_content", userEvaluate.EvaluateContent));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //根据id查询用户评价
        public UserEvaluate queryUserEvaluateById(int id)
        {
            UserEvaluate userEvaluate = new UserEvaluate();
            String sql = "SELECT * FROM tb_user_evaluate WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader sdr = cmd.ExecuteReader();
            /*HasRows是看结果集中是否有数据,nextResult是跳转到结果集的下一行*/
            if (sdr.HasRows)
            {
                sdr.Read();
                userEvaluate.Id = sdr.GetInt32(0);
                userEvaluate.ReceiveUser.Id = sdr.GetInt32(1);
                userEvaluate.SendUser.Id = sdr.GetInt32(2);
                userEvaluate.EvaluateContent = sdr.GetString(3);
            }

            sdr.Close();
            DbUtil.close(cmd);
            return userEvaluate;
        }
        //根据接收方 id查询用户评价
        public List<UserEvaluate> queryUserEvaluateByReceiveId(int receiveId)
        {
            List<UserEvaluate> list = new List<UserEvaluate>();
            String sql = "SELECT * FROM tb_user_evaluate WHERE receive_user_id = @receive_user_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add("@receive_user_id", receiveId);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    UserEvaluate userEvaluate = new UserEvaluate();
                    userEvaluate.Id = sdr.GetInt32(0);
                    userEvaluate.ReceiveUser.Id = sdr.GetInt32(1);
                    userEvaluate.SendUser.Id = sdr.GetInt32(2);
                    userEvaluate.EvaluateContent = sdr.GetString(3);
                    list.Add(userEvaluate);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        //根据发送方 id查询用户评价
        public List<UserEvaluate> queryUserEvaluateBySendId(int sendId)
        {
            List<UserEvaluate> list = new List<UserEvaluate>();
            String sql = "SELECT * FROM tb_user_evaluate WHERE send_user_id = @send_user_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add("@send_user_id", sendId);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    UserEvaluate userEvaluate = new UserEvaluate();
                    userEvaluate.Id = sdr.GetInt32(0);
                    userEvaluate.ReceiveUser.Id = sdr.GetInt32(1);
                    userEvaluate.SendUser.Id = sdr.GetInt32(2);
                    userEvaluate.EvaluateContent = sdr.GetString(3);
                    list.Add(userEvaluate);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        //发出方修改评价
        public Boolean updateUserEvaluate(UserEvaluate userEvaluate)
        {
            String sql = "UPDATE tb_user_evaluate SET evaluate_content = @evaluate_content WHERE send_user_id = @send_user_id"; 
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@send_user_id", userEvaluate.SendUser));
            cmd.Parameters.Add(new SqlParameter("@evaluate_content", userEvaluate.EvaluateContent));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //删除用户评价
        public Boolean deleteUserEvaluate(UserEvaluate userEvaluate)
        {
            String sql = "DELETE FROM tb_user_evaluate WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", userEvaluate.Id));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
    }
}
