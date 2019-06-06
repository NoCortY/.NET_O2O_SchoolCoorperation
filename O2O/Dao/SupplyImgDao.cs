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
    public class SupplyImgDao
    {
        public Boolean insertSupplyImg(SupplyImg supplyImg)
        {
            String sql = "INSERT INTO tb_supply_img(img_path,img_status,supply_id) VALUES(@img_path,@img_status,@supply_id)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@img_path", supplyImg.ImgPath));
            cmd.Parameters.Add(new SqlParameter("@img_status", supplyImg.ImgStatus));//0缩略图,1详情图
            cmd.Parameters.Add(new SqlParameter("@supply_id", supplyImg.Supply.Id));
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
        public Boolean deleteSupplyImgBySupplyId(int supplyId)
        {
            String sql = "DELETE FROM tb_supply_img WHERE supply_id = @supplyId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@supplyId", supplyId));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        public List<SupplyImg> querySupplyImgBySupplyId(int supplyId)
        {
            String sql = "SELECT * FROM tb_supply_img WHERE supply_id = @supplyId";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@supplyId", supplyId));
            List<SupplyImg> list = new List<SupplyImg>();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    SupplyImg supplyImg = new SupplyImg();
                    supplyImg.Id = sdr.GetInt32(0);
                    supplyImg.ImgPath = sdr.GetString(1);
                    supplyImg.ImgStatus = sdr.GetInt32(2);
                    supplyImg.Supply.Id = sdr.GetInt32(3);
                    list.Add(supplyImg);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
            
        }
        public SupplyImg querySupplyFirstImgBySupplyId(int supplyId)
        {
            String sql = "SELECT * FROM tb_supply_img WHERE supply_id = @supplyId AND img_status = 0";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@supplyId", supplyId));
            SqlDataReader sdr = cmd.ExecuteReader();
            SupplyImg supplyImg = new SupplyImg();
            if (sdr.HasRows)
            {
                sdr.Read();
                supplyImg.Id = sdr.GetInt32(0);
                supplyImg.ImgPath = sdr.GetString(1);
                supplyImg.ImgStatus = sdr.GetInt32(2);
                supplyImg.Supply.Id = sdr.GetInt32(3);
            }
            sdr.Close();
            DbUtil.close(cmd);
            return supplyImg;
        }
        public List<SupplyImg> querySupplyDescImgBySupplyId(int supplyId)
        {
            String sql = "SELECT * FROM tb_supply_img WHERE supply_id = @supplyId AND img_status = 1";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@supplyId", supplyId));
            List<SupplyImg> list = new List<SupplyImg>();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    SupplyImg supplyImg = new SupplyImg();
                    supplyImg.Id = sdr.GetInt32(0);
                    supplyImg.ImgPath = sdr.GetString(1);
                    supplyImg.ImgStatus = sdr.GetInt32(2);
                    supplyImg.Supply.Id = sdr.GetInt32(3);
                    list.Add(supplyImg);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
    }
}
