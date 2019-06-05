using Dao.Utils;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class IMDao
    {
        public Boolean insertContent(IM im)
        {
            String sql = "INSERT INTO tb_IM(send_user_id,receive_user_id,send_user_name,receive_user_name,content,send_time) VALUES(@sendUserId,@receiveUserId,@sendUserName,@receiveUserName,@content,@sendTime";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@sendUserId", im.SendUserId));
            cmd.Parameters.Add(new SqlParameter("@receiveUserId", im.ReceiveUserId));
            cmd.Parameters.Add(new SqlParameter("@sendUserName", im.SendUserName));
            cmd.Parameters.Add(new SqlParameter("@receiveUserName", im.ReceiveUserName));
            cmd.Parameters.Add(new SqlParameter("@content", im.Content));
            cmd.Parameters.Add(new SqlParameter("@sendTime", im.SendTime));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public List<IM> queryIMByReceiveUserId(int receiveUserId)
        {
            String sql = "SELECT * FROM tb_IM WHERE receive_user_id = @receiveUserId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@receiveUserId", receiveUserId));
            SqlDataReader sdr = cmd.ExecuteReader();
            List<IM> list = new List<IM>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    IM im = new IM();
                    im.SendUserId = sdr.GetInt32(0);
                    im.ReceiveUserId = sdr.GetInt32(1);
                    im.SendUserName = sdr.GetString(2);
                    im.ReceiveUserName = sdr.GetString(3);
                    im.Content = sdr.GetString(4);
                    im.SendTime = sdr.GetDateTime(5);
                    list.Add(im);
                }
            }
            DbUtil.close(cmd);
            return list;
        }
        public Boolean deleteIMByReceiveUserId(int receiveUserId)
        {
            String sql = "DELETE FROM tb_IM WHERE receive_user_id = @receiveUserId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@receiveUserId", receiveUserId));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int queryCountOfMessage(int receiveUserId)
        {
            String sql = "SELECT COUNT(receive_user_id) FROM tb_IM WHERE receive_user_id = @receiveUserId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@receiveUserId", receiveUserId));
            SqlDataReader sdr = cmd.ExecuteReader();
            int count = 0;
            if (sdr.HasRows)
            {
                sdr.Read();
                count = sdr.GetInt32(0);
            }
            DbUtil.close(cmd);
            return count;
        }
    }
}
