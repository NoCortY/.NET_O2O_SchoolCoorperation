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
    public class UserDao
    {

        public List<User> queryAllUser()
        {
            List<User> list = new List<User>();
            String sql = "SELECT * FROM tb_user";
            SqlCommand cmd = DbUtil.getCommand(sql);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    User user = new User();
                    user.Id = sdr.GetInt32(0);
                    user.Username = sdr.GetString(1);
                    user.Password = sdr.GetString(2);
                    user.UserHeader = sdr.GetString(3);
                    user.Gender = sdr.GetString(4);
                    user.UserStatus = sdr.GetInt32(5);
                    user.RegisterTime = sdr.GetDateTime(6);
                    user.RealName = sdr.GetString(7);
                    user.TeleNumber = sdr.GetString(8);
                    user.NickName = sdr.GetString(9);
                    list.Add(user);
                }
            }
            DbUtil.close(cmd);
            return list;
        }
        //查询
        public User queryUserById(int id)
        {
            User user = new User();
            String sql = "SELECT * FROM tb_user WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id",id));
            SqlDataReader sdr =cmd.ExecuteReader();
            /*HasRows是看结果集中是否有数据,nextResult是跳转到结果集的下一行*/
            if (sdr.HasRows) {
                sdr.Read();
                user.Id = sdr.GetInt32(0);
                user.Username = sdr.GetString(1);
                user.Password = sdr.GetString(2);
                user.UserHeader = sdr.GetString(3);
                user.Gender = sdr.GetString(4);
                user.UserStatus = sdr.GetInt32(5);
                user.RegisterTime = sdr.GetDateTime(6);
                user.RealName = sdr.GetString(7);
                user.TeleNumber = sdr.GetString(8);
                user.NickName = sdr.GetString(9);
            }

            sdr.Close();
            DbUtil.close(cmd);
            return user;
        }
        //查找用户是否存在
        public Boolean userExist(String username)
        {
            String sql = "SELECT * FROM tb_user WHERE username = @username";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", username));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Close();
                DbUtil.close(cmd);
                return true;
            }
            else
            {
                sdr.Close();
                DbUtil.close(cmd);
                return false;
            }
        }
        //根据用户名查找用户
        public User queryUserByUsername(String username)
        {
            User user = new User();
            StringBuilder password = new StringBuilder();
            String sql = "SELECT id,password,nickname,userstatus FROM tb_user WHERE username = @username";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", username));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                user.Id = sdr.GetInt32(0);
                user.Password = sdr.GetString(1);
                user.NickName = sdr.GetString(2);
                user.UserStatus = sdr.GetInt32(3);
                sdr.Close();
                DbUtil.close(cmd);
                return user;
            }
            else
            {
                sdr.Close();
                DbUtil.close(cmd);
                return null;
            }
        }
        //更新
        public Boolean updateUser(User user)
        {
            String sql = "UPDATE tb_user SET username = @username,password=@password,userheader = @userheader, gender = @gender, usrestaus = @userstaus, registertime = @registertime, realname = @realname, telenumber = @telenumber nickname = @nickname where id = @id ";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", user.Username));
            cmd.Parameters.Add(new SqlParameter("@password",user.Password));
            cmd.Parameters.Add(new SqlParameter("@userheader", user.UserHeader));
            cmd.Parameters.Add(new SqlParameter("@gender", user.Gender));
            cmd.Parameters.Add(new SqlParameter("@usrestaus", user.UserStatus));
            cmd.Parameters.Add(new SqlParameter("@registertime", user.RegisterTime));
            cmd.Parameters.Add(new SqlParameter("@realname", user.RealName));
            cmd.Parameters.Add(new SqlParameter("@telenumber", user.TeleNumber));
            cmd.Parameters.Add(new SqlParameter("@nickname", user.NickName));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //插入
        public Boolean insertUser(User user)
        {
            String sql = "INSERT INTO tb_user(username,password,userheader,gender,userstatus,registertime,realname,telenumber,nickname) VALUES(@username,@password,@userheader,@gender, @userstaus,@registertime,@realname,@telenumber,@nickname)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", user.Username));
            cmd.Parameters.Add(new SqlParameter("@password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@userheader", user.UserHeader));
            cmd.Parameters.Add(new SqlParameter("@gender", user.Gender));
            cmd.Parameters.Add(new SqlParameter("@userstaus", user.UserStatus));
            cmd.Parameters.Add(new SqlParameter("@registertime", user.RegisterTime));
            cmd.Parameters.Add(new SqlParameter("@realname", user.RealName));
            cmd.Parameters.Add(new SqlParameter("@telenumber", user.TeleNumber));
            cmd.Parameters.Add(new SqlParameter("@nickname", user.NickName));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //删除
        /*public Boolean deleteUser(User user)
        {
            String sql = "DELETE FROM tb_user WHERE id = @id and username = @username";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", user.Id));
            cmd.Parameters.Add(new SqlParameter("@username", user.Username));
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                return true;
            else
                return false;
        }*/
    }
}
