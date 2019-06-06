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
    public class RequirementImgDao
    {
        public Boolean insertRequirementImg(RequirementImg requirementImg)
        {
            String sql = "INSERT INTO tb_requirement_img(img_path,img_status,requirement_id) VALUES(@img_path,@img_status,@requirement_id)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@img_path", requirementImg.ImgPath));
            cmd.Parameters.Add(new SqlParameter("@img_status", requirementImg.ImgStatus));//0缩略图,1详情图
            cmd.Parameters.Add(new SqlParameter("@requirement_id", requirementImg.Requirement.Id));
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
        public Boolean deleteRequirementImgByRequirementId(int requirementId)
        {
            String sql = "DELETE FROM tb_requirement_img WHERE requirement_id = @requirement_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_id", requirementId));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        public List<RequirementImg> queryRequirementImgByRequirementId(int requirementId)
        {
            String sql = "SELECT * FROM tb_requirement_img WHERE requirement_id = @requirement_id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_id", requirementId));
            List<RequirementImg> list = new List<RequirementImg>();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    RequirementImg requirementImg = new RequirementImg();
                    requirementImg.Id = sdr.GetInt32(0);
                    requirementImg.ImgPath = sdr.GetString(1);
                    requirementImg.ImgStatus = sdr.GetInt32(2);
                    requirementImg.Requirement.Id = sdr.GetInt32(3);
                    list.Add(requirementImg);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
            
        }
        public RequirementImg queryRequirementFirstImgByRequirementId(int requirementId)
        {
            String sql = "SELECT * FROM tb_requirement_img WHERE requirement_id = @requirement_id AND img_status = 0";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_id", requirementId));
            SqlDataReader sdr = cmd.ExecuteReader();
            RequirementImg requirementImg = new RequirementImg();
            if (sdr.HasRows)
            {
                sdr.Read();
                requirementImg.Id = sdr.GetInt32(0);
                requirementImg.ImgPath = sdr.GetString(1);
                requirementImg.ImgStatus = sdr.GetInt32(2);
                requirementImg.Requirement.Id = sdr.GetInt32(3);
            }
            sdr.Close();
            DbUtil.close(cmd);
            return requirementImg;
        }
        public List<RequirementImg> queryRequirementDescImgByRequirementId(int requirementId)
        {
            String sql = "SELECT * FROM tb_requirement_img WHERE requirement_id = @requirement_id AND img_status = 1";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@requirement_id", requirementId));
            List<RequirementImg> list = new List<RequirementImg>();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    RequirementImg requirementImg = new RequirementImg();
                    requirementImg.Id = sdr.GetInt32(0);
                    requirementImg.ImgPath = sdr.GetString(1);
                    requirementImg.ImgStatus = sdr.GetInt32(2);
                    requirementImg.Requirement.Id = sdr.GetInt32(3);
                    list.Add(requirementImg);
                }
            }
            sdr.Close();
            DbUtil.close(cmd);
            return list;
        }
    }
}
