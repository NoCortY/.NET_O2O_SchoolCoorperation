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
        public Boolean updateRequirementById(Requirement requirement)
        {
            String sql = "UPDATE tb_requirement SET requirement_name = @requirementName,requirement_desc = @requirementDesc,category_id = @categoryId,modify_time = @modifyTime WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirementName", requirement.RequirementName));
            cmd.Parameters.Add(new SqlParameter("@requirementDesc", requirement.RequirementDesc));
            cmd.Parameters.Add(new SqlParameter("@categoryId", requirement.RequirementCategory.Id));
            cmd.Parameters.Add(new SqlParameter("@modifyTime", requirement.ModifyTime));
            cmd.Parameters.Add(new SqlParameter("@id", requirement.Id));
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*public List<Requirement> queryRequirementByUserId(int userId)
        {
            String sql = "SELECT * FROM tb_requirement WHERE user_id = @userId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            List<Requirement> list = new List<Requirement>();
            cmd.Parameters.Add(new SqlParameter("@userId", userId));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Requirement requirement = new Requirement();
                    requirement.Id = sdr.GetInt32(0);
                    requirement.RequirementName = sdr.GetString(1);
                    requirement.Priority = sdr.GetInt32(3);
                    requirement.CreateTime = sdr.GetDateTime(6);
                    requirement.ModifyTime = sdr.GetDateTime(7);
                    requirement.RequirementStatus = sdr.GetInt32(8);
                    list.Add(requirement);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }*/
        public String queryCategoryNameById(int id)
        {
            String sql = "SELECT category_name FROM tb_category WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            String categoryName = sdr.GetString(0);
            sdr.Close();
            DbUtil.close(cmd);
            return categoryName;
        }
        public Requirement queryRequirementById(int id)
        {
            String sql = "SELECT * FROM tb_requirement WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader sdr = cmd.ExecuteReader();
            Requirement requirement = new Requirement();
            if (sdr.HasRows)
            {
                sdr.Read();
                requirement.Id = sdr.GetInt32(0);
                requirement.RequirementName = sdr.GetString(1);
                requirement.RequirementDesc = sdr.GetString(2);
                requirement.Priority = sdr.GetInt32(3);
                requirement.User.Id = sdr.GetInt32(4);
                requirement.RequirementCategory.Id = sdr.GetInt32(5);
                requirement.CreateTime = sdr.GetDateTime(6);
                requirement.ModifyTime = sdr.GetDateTime(7);
                requirement.RequirementStatus = sdr.GetInt32(8);
            }
            sdr.Close();
            DbUtil.close(cmd);
            return requirement;
        }
        //添加需求
        public int insertRequirement(Requirement requirement)
        {
            String sql = "INSERT INTO tb_requirement(requirement_name,requirement_desc,priority,user_id,category_id,create_time,modify_time,requirement_status) VALUES(@requirement_name,@requirement_desc,@priority,@user_id,@category_id,@create_time,@modify_time,@requirement_status);SELECT @@Identity";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_name", requirement.RequirementName));
            cmd.Parameters.Add(new SqlParameter("@requirement_desc", requirement.RequirementDesc));
            cmd.Parameters.Add(new SqlParameter("@priority", requirement.Priority));
            cmd.Parameters.Add(new SqlParameter("@user_id", requirement.User.Id));
            cmd.Parameters.Add(new SqlParameter("@category_id", requirement.RequirementCategory.Id));
            cmd.Parameters.Add(new SqlParameter("@create_time", requirement.CreateTime));
            cmd.Parameters.Add(new SqlParameter("@modify_time", requirement.ModifyTime));
            cmd.Parameters.Add(new SqlParameter("@requirement_status", requirement.RequirementStatus));
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            DbUtil.close(cmd); 
            return i;
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
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        //删除需求
        public Boolean deleteRequirementById(int id)
        {
            String sql = "DELETE FROM tb_requirement WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //根据需求名模糊查询需求
        public List<Requirement> queryRequirementByRequirementName(string RequirementName)
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement WHERE requirement_name LIKE N'%" + RequirementName + "%' AND requirement_status=1";
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
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        //根据需求id分类查询需求
        public List<Requirement> queryRequirementByRequirementCategory(int categoryId)
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement WHERE category_id = @category_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@category_id", categoryId));
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
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        public Boolean updateRequirementByManager(Requirement requirement)
        {
            String sql = "UPDATE tb_requirement SET priority=@priority,modify_time=@modify_time,requirement_status=@requirement_status WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@priority", requirement.Priority));
            cmd.Parameters.Add(new SqlParameter("@modify_time", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@requirement_status", requirement.RequirementStatus));
            cmd.Parameters.Add(new SqlParameter("@id", requirement.Id));
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
        public List<Requirement> queryAllRequirement()
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement";
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
                    //sdr.NextResult();
                    list.Add(requirement);
                }
            }

            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        public List<Requirement> queryAllRequirementWithoutBanned()
        {
            List<Requirement> list = new List<Requirement>();
            String sql = "SELECT * FROM tb_requirement WHERE requirement_status = 1";
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
                    //sdr.NextResult();
                    list.Add(requirement);
                }
            }

            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        //根据需求id修改需求
        public int updateRequirement(Requirement requirement)
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
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            DbUtil.close(cmd);
            return i;
        }
    }
}
