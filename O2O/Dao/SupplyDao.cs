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
    public class SupplyDao
    {
        public Boolean updateSupplyById(Supply supply)
        {
            String sql = "UPDATE tb_supply SET supply_name = @supplyName,supply_desc = @supplyDesc,category_id = @categoryId,modify_time = @modifyTime WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@supplyName", supply.SupplyName));
            cmd.Parameters.Add(new SqlParameter("@supplyDesc", supply.SupplyDesc));
            cmd.Parameters.Add(new SqlParameter("@categoryId", supply.SupplyCategory.Id));
            cmd.Parameters.Add(new SqlParameter("@modifyTime", supply.ModifyTime));
            cmd.Parameters.Add(new SqlParameter("@id", supply.Id));
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
        public Boolean deleteSupplyById(int id)
        {
            String sql = "DELETE FROM tb_supply WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
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
        public List<Supply> querySupplyByUserId(int userId)
        {
            List<Supply> list = new List<Supply>();
            String sql = "SELECT * FROM tb_supply WHERE user_id = @userId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@userId", userId));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Supply supply = new Supply();
                    supply.Id = sdr.GetInt32(0);
                    supply.SupplyName = sdr.GetString(1);
                    supply.Priority = sdr.GetInt32(4);
                    supply.CreateTime = sdr.GetDateTime(6);
                    supply.ModifyTime = sdr.GetDateTime(7);
                    supply.SupplyStatus = sdr.GetInt32(8);
                    list.Add(supply);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        public String queryCategoryNameById(int id)
        {
            String sql = "SELECT category_name FROM tb_category WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            String categoryName = sdr.GetString(0);
            DbUtil.close(cmd);
            sdr.Close();
            return categoryName;
        }
        public Supply querySupplyById(int id)
        {
            String sql = "SELECT * FROM tb_supply WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader sdr = cmd.ExecuteReader();
            Supply supply = new Supply();
            if (sdr.HasRows)
            {
                sdr.Read();
                supply.Id = sdr.GetInt32(0);
                supply.SupplyName = sdr.GetString(1);
                supply.SupplyDesc = sdr.GetString(2);
                supply.SupplyCategory.Id = sdr.GetInt32(3);
                supply.Priority = sdr.GetInt32(4);
                supply.User.Id = sdr.GetInt32(5);
                supply.CreateTime = sdr.GetDateTime(6);
                supply.ModifyTime = sdr.GetDateTime(7);
                supply.SupplyStatus = sdr.GetInt32(8);
            }
            sdr.Close();
            DbUtil.close(cmd);
            return supply;
        }
        public int insertSupply(Supply supply)
        {
            String sql = "INSERT tb_supply(supply_name,supply_desc,category_id,priority,user_id,create_time,modify_time,supply_status) VALUES(@supply_name,@supply_desc,@category_id,@priority,@user_id,@create_time,@modify_time,@supply_status);SELECT @@Identity";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@supply_name", supply.SupplyName));
            cmd.Parameters.Add(new SqlParameter("@supply_desc",supply.SupplyDesc));
            cmd.Parameters.Add(new SqlParameter("@category_id", supply.SupplyCategory.Id));
            cmd.Parameters.Add(new SqlParameter("@priority", supply.Priority));
            cmd.Parameters.Add(new SqlParameter("@user_id", supply.User.Id));
            cmd.Parameters.Add(new SqlParameter("@create_time", supply.CreateTime));
            cmd.Parameters.Add(new SqlParameter("@modify_time", supply.ModifyTime));
            cmd.Parameters.Add(new SqlParameter("@supply_status", supply.SupplyStatus));
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            DbUtil.close(cmd);
            return i;
        }
        public Boolean updateSupplyByManager(Supply supply)
        {
            String sql = "UPDATE tb_supply SET priority=@priority,modify_time=@modify_time,supply_status=@supply_status WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@priority", supply.Priority));
            cmd.Parameters.Add(new SqlParameter("@modify_time", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@supply_status", supply.SupplyStatus));
            cmd.Parameters.Add(new SqlParameter("@id", supply.Id));
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
        public List<Supply> querySupplyByName(String supplyName) {
            List<Supply> list = new List<Supply>();
            String sql = "SELECT * FROM tb_supply WHERE supply_name LIKE N'%" + supplyName + "%' AND supply_status = 1";
            SqlCommand cmd = DbUtil.getCommand(sql);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows) {
                while (sdr.Read())
                {
                    Supply supply = new Supply();
                    supply.Id = sdr.GetInt32(0);
                    supply.SupplyName = sdr.GetString(1);
                    supply.SupplyDesc = sdr.GetString(2);
                    supply.SupplyCategory.Id = sdr.GetInt32(3);
                    supply.Priority = sdr.GetInt32(4);
                    supply.User.Id = sdr.GetInt32(5);
                    supply.CreateTime = sdr.GetDateTime(6);
                    supply.ModifyTime = sdr.GetDateTime(7);
                    supply.SupplyStatus = sdr.GetInt32(8);
                    list.Add(supply);
                }
            }

            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        public List<Supply> querySupplyByCategory(int categoryId)
        {
            List<Supply> list = new List<Supply>();
            String sql = "SELECT * FROM tb_supply WHERE category_id = @category_id AND supply_status = 1";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@category_id", categoryId));
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Supply supply = new Supply();
                    supply.Id = sdr.GetInt32(0);
                    supply.SupplyName = sdr.GetString(1);
                    supply.Id = sdr.GetInt32(0);
                    supply.SupplyName = sdr.GetString(1);
                    supply.SupplyDesc = sdr.GetString(2);
                    supply.SupplyCategory.Id = sdr.GetInt32(3);
                    supply.Priority = sdr.GetInt32(4);
                    supply.User.Id = sdr.GetInt32(5);
                    supply.CreateTime = sdr.GetDateTime(6);
                    supply.ModifyTime = sdr.GetDateTime(7);
                    supply.SupplyStatus = sdr.GetInt32(8);
                    //sdr.NextResult();
                    list.Add(supply);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        public List<Supply> queryAllSupplyWithoutBanned()
        {
            List<Supply> list = new List<Supply>();
            String sql = "SELECT * FROM tb_supply WHERE supply_status = 1";
            SqlCommand cmd = DbUtil.getCommand(sql);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Supply supply = new Supply();
                    supply.Id = sdr.GetInt32(0);
                    supply.SupplyName = sdr.GetString(1);
                    supply.SupplyDesc = sdr.GetString(2);
                    supply.SupplyCategory.Id = sdr.GetInt32(3);
                    supply.Priority = sdr.GetInt32(4);
                    supply.User.Id = sdr.GetInt32(5);
                    supply.CreateTime = sdr.GetDateTime(6);
                    supply.ModifyTime = sdr.GetDateTime(7);
                    supply.SupplyStatus = sdr.GetInt32(8);
                    //sdr.NextResult();
                    list.Add(supply);
                }
            }

            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
        public List<Supply> queryAllSupply()
        {
            List<Supply> list = new List<Supply>();
            String sql = "SELECT * FROM tb_supply";
            SqlCommand cmd = DbUtil.getCommand(sql);
            
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Supply supply = new Supply();
                    supply.Id = sdr.GetInt32(0);
                    supply.SupplyName = sdr.GetString(1);
                    supply.SupplyDesc = sdr.GetString(2);
                    supply.SupplyCategory.Id = sdr.GetInt32(3);
                    supply.Priority = sdr.GetInt32(4);
                    supply.User.Id = sdr.GetInt32(5);
                    supply.CreateTime = sdr.GetDateTime(6);
                    supply.ModifyTime = sdr.GetDateTime(7);
                    supply.SupplyStatus = sdr.GetInt32(8);
                    //sdr.NextResult();
                    list.Add(supply);
                }
            }

            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
    }
}
