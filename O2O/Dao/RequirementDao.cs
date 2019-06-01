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
    public class RequirementDao
    {
        //添加需求
        public Boolean insertRequirement(Requirement requirement)
        {
            String sql = "INSERT INTO tb_requirement(requirement_name,requirement_desc,priority,user_id,category_id,creat_time,modify_time,requirement_status) VALUES(@requirement_name,@requirement_desc,@priority,@user_id,@category_id,@creat_time,@modify_time,@requirement_status)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_name", requirement.RequirementName));
            cmd.Parameters.Add(new SqlParameter("@requirement_desc", requirement.RequirementDesc));
            cmd.Parameters.Add(new SqlParameter("@priority", requirement.Priority));
            cmd.Parameters.Add(new SqlParameter("@user_id", requirement.User));
            cmd.Parameters.Add(new SqlParameter("@category_id", requirement.RequirementCategory));
            cmd.Parameters.Add(new SqlParameter("@creat_time", requirement.CreateTime));
            cmd.Parameters.Add(new SqlParameter("@modify_time", requirement.ModifyTime));
            cmd.Parameters.Add(new SqlParameter("@requirement_status", requirement.RequirementStatus));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //查询某个用户（ID）的所有需求
        public List<Requirement> queryRequirementByUserId(int userId)
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement WHERE user_id = @user_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@user_id", userId));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Requirement requirement = new Requirement();
                    requirement.Id = sdr.GetInt32(0);
                    requirement.RequirementName = sdr.GetString(1);
                    requirement.RequirementDesc = sdr.GetString(2);
                    requirement.Priority = sdr.GetInt32(3);
                    requirement.User.Id = sdr.GetInt32(4);
                    requirement.RequirementCategory.Id = sdr.GetInt32(5);
                    requirement.CreateTime = sdr.GetDateTime(6);
                    requirement.ModifyTime = sdr.GetDateTime(7);
                    requirement.RequirementStatus = sdr.GetInt32(8);
                    list.Add(requirement);
                }
            }
            return list;
        }
        //删除需求
        public Boolean deleteUser(User user)
        {
            String sql = "DELETE FROM tb_requirement WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", user.Id));
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                return true;
            else
                return false;
        }
        //根据需求名模糊查询需求
        public List<Requirement> queryRequirementByRequirementName(string RequirementName)
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement WHERE requirement_name like '%'" + RequirementName + '%';
            SqlCommand cmd = DbUtil.getCommand(sql);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Requirement requirement = new Requirement();
                    requirement.Id = sdr.GetInt32(0);
                    requirement.RequirementName = sdr.GetString(1);
                    requirement.RequirementDesc = sdr.GetString(2);
                    requirement.Priority = sdr.GetInt32(3);
                    requirement.User.Id = sdr.GetInt32(4);
                    requirement.RequirementCategory.Id = sdr.GetInt32(5);
                    requirement.CreateTime = sdr.GetDateTime(6);
                    requirement.ModifyTime = sdr.GetDateTime(7);
                    requirement.RequirementStatus = sdr.GetInt32(8);
                    list.Add(requirement);
                }
            }
            return list;
        }
        //根据需求id分类查询需求
        public List<Requirement> queryRequirementByRequirementCategory(int RequirementCategory)
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement WHERE category_id = @category_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Requirement requirement = new Requirement();
                    requirement.Id = sdr.GetInt32(0);
                    requirement.RequirementName = sdr.GetString(1);
                    requirement.RequirementDesc = sdr.GetString(2);
                    requirement.Priority = sdr.GetInt32(3);
                    requirement.User.Id = sdr.GetInt32(4);
                    requirement.RequirementCategory.Id = sdr.GetInt32(5);
                    requirement.CreateTime = sdr.GetDateTime(6);
                    requirement.ModifyTime = sdr.GetDateTime(7);
                    requirement.RequirementStatus = sdr.GetInt32(8);
                    list.Add(requirement);
                }
            }
            return list;
        }

        //根据需求id修改需求
        public Boolean updateRequirement(Requirement requirement)
        {
            String sql = "UPDATE tb_requirement SET requirement_name = @requirement_name,requirement_desc = @requirement_desc,priority = @priority,user_id = @user_id,category_id = @category_id,creat_time = @creat_time,modify_time = @modify_time,requirement_status = @requirement_status where id = @id ";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_name", requirement.RequirementName));
            cmd.Parameters.Add(new SqlParameter("@requirement_desc", requirement.RequirementDesc));
            cmd.Parameters.Add(new SqlParameter("@priority", requirement.Priority));
            cmd.Parameters.Add(new SqlParameter("@user_id", requirement.User));
            cmd.Parameters.Add(new SqlParameter("@category_id", requirement.RequirementCategory));
            cmd.Parameters.Add(new SqlParameter("@creat_time", requirement.CreateTime));
            cmd.Parameters.Add(new SqlParameter("@modify_time", requirement.ModifyTime));
            cmd.Parameters.Add(new SqlParameter("@requirement_status", requirement.RequirementStatus));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
    }
}
