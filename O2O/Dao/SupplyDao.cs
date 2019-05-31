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
                    //supply.CreateTime = sdr.GetDateTime(6);
                    // supply.ModifyTime = sdr.GetDateTime(7);
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
                    //supply.CreateTime = sdr.GetDateTime(6);
                    //supply.ModifyTime = sdr.GetDateTime(7);
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
                    //supply.CreateTime = sdr.GetDateTime(6);
                    //supply.ModifyTime = sdr.GetDateTime(7);
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
                    //supply.CreateTime = sdr.GetDateTime(6);
                    //supply.ModifyTime = sdr.GetDateTime(7);
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
