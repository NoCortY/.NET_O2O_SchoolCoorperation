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
       
        //查询
        public User queryUserById(int id)
        {
            User user = new User();
            String sql = "SELECT * FROM tb_user WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id",id));
            SqlDataReader sdr =cmd.ExecuteReader();
            sdr.Read();
            if (sdr.NextResult() != null) {
                user.Id = sdr.GetInt32(0);
                user.Username = sdr.GetString(1);
                user.Password = sdr.GetString(2);
                user.UserHeader = sdr.GetString(3);
                user.Gender = sdr.GetString(4);
                user.UserStatus = sdr.GetInt32(5);
                user.RegisterTime = sdr.GetDateTime(6);
                user.RealName = sdr.GetString(7);
                user.TeleNumber = sdr.GetString(8);

            }
            DbUtil.close();
            return user;

        }
        //更新
        public Boolean updateUser(User user)
        {
            String sql = "UPDATE tb_user SET username = @username,password=@password,userheader = @userheader, gender = @gender, usrestaus = @userstaus, registertime = @registertime, realname = @realname, telenumber = @telenumber where id = @id ";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", user.Username));
            cmd.Parameters.Add(new SqlParameter("@password",user.Password));
            cmd.Parameters.Add(new SqlParameter("@userheader", user.UserHeader));
            cmd.Parameters.Add(new SqlParameter("@gender", user.Gender));
            cmd.Parameters.Add(new SqlParameter("@usrestaus", user.UserStatus));
            cmd.Parameters.Add(new SqlParameter("@registertime", user.RegisterTime));
            cmd.Parameters.Add(new SqlParameter("@realname", user.RealName));
            cmd.Parameters.Add(new SqlParameter("@telenumber", user.TeleNumber));
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                return true;
            else
                return false;
        }
        //插入
        public Boolean insertUser(User user)
        {
            String sql = "INSERT INTO tb_user(username,password,userheader,gender,userstatus,registertime,realname,telenumber) VALUES(@username,@password,@userheader,@gender, @userstaus,@registertime,@realname,@telenumber)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", user.Username));
            cmd.Parameters.Add(new SqlParameter("@password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@userheader", user.UserHeader));
            cmd.Parameters.Add(new SqlParameter("@gender", user.Gender));
            cmd.Parameters.Add(new SqlParameter("@usrestaus", user.UserStatus));
            cmd.Parameters.Add(new SqlParameter("@registertime", user.RegisterTime));
            cmd.Parameters.Add(new SqlParameter("@realname", user.RealName));
            cmd.Parameters.Add(new SqlParameter("@telenumber", user.TeleNumber));
            int i = cmd.ExecuteNonQuery();
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
