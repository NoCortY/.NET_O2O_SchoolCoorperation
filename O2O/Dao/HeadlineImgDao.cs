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
    public class HeadlineImgDao{
        //添加
        public Boolean insertHeadlineImg(HeadlineImg headlineImg)
        {
            String sql = "INSERT INTO tb_headline_img(img_path) VALUES(@img_path)";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@img_path", headlineImg.ImgPath));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //删除
        public Boolean deleteHeadlineImg(HeadlineImg headlineImg)
        {
            String sql = "DELETE FROM tb_headline_img WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", headlineImg.Id));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //更新
        public Boolean updateHeadlineImg(HeadlineImg headlineImg)
        {
            String sql = "UPDATE tb_headline_img SET img_path = @img_path";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@img_path", headlineImg.ImgPath));
            int i = cmd.ExecuteNonQuery();
            DbUtil.close(cmd);
            if (i > 0)
                return true;
            else
                return false;
        }
        //查找
        public HeadlineImg queryHeadlineImgById(int id)
        {
            HeadlineImg headlineImg = new HeadlineImg();
            String sql = "SELECT * FROM tb_headline_img WHERE id = @id";
            SqlCommand cmd = DbUtil.getCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader sdr = cmd.ExecuteReader();
            /*HasRows是看结果集中是否有数据,nextResult是跳转到结果集的下一行*/
            if (sdr.HasRows)
            {
                sdr.Read();
                headlineImg.Id = sdr.GetInt32(0);
                headlineImg.ImgPath = sdr.GetString(1);
            }
            sdr.Close();
            DbUtil.close(cmd);
            return headlineImg;
        }
    }
}
