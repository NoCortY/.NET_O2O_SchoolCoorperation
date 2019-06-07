using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SupplyService
    {
        SupplyDao supplyDao = new SupplyDao();
        CategoryDao categoryDao = new CategoryDao();
        UserDao userDao = new UserDao();
        SupplyImgDao supplyImgDao = new SupplyImgDao();
        public Boolean updateSupply(Supply supply,List<SupplyImg> list)
        {
            supply.ModifyTime = DateTime.Now;
            if (list.Count != 0) {
                //删除原有的图片
                SupplyImg supplyImg = supplyImgDao.querySupplyFirstImgBySupplyId(supply.Id);
                List<SupplyImg> listExsit = supplyImgDao.querySupplyDescImgBySupplyId(supply.Id);
                listExsit.Add(supplyImg);
                foreach (SupplyImg s in listExsit)
                {
                    if (s.ImgPath != null)
                    {
                        File.Delete(s.ImgPath);
                    }
                }
                supplyImgDao.deleteSupplyImgBySupplyId(supply.Id);
                
                //插入新图片
                foreach (SupplyImg s in list)
                {
                    supplyImgDao.insertSupplyImg(s);
                }
            }
            return supplyDao.updateSupplyById(supply);
        }
        public Boolean removeSupplyById(int supplyId)
        {
            SupplyImg supplyImg = supplyImgDao.querySupplyFirstImgBySupplyId(supplyId);
            List<SupplyImg> list = supplyImgDao.querySupplyDescImgBySupplyId(supplyId);
            list.Add(supplyImg);
            foreach (SupplyImg s in list)
            {
                if (s.ImgPath != null&&s.ImgPath!="")
                {
                    File.Delete(s.ImgPath);
                }
            }
            Boolean flagSupplyImg = supplyImgDao.deleteSupplyImgBySupplyId(supplyId);
            
            Boolean flagSupply = supplyDao.deleteSupplyById(supplyId);
            return flagSupply;
        }
        public List<Supply> getMySupply(int userId)
        {
            List<Supply> list = supplyDao.querySupplyByUserId(userId);
            return list;
        }
        public String getCategoryName(int id)
        {
            return supplyDao.queryCategoryNameById(id);
        }
        public Supply getSupplyById(int id)
        {
            return supplyDao.querySupplyById(id);
        }
        public List<SupplyImg> getSupplyDescImg(int supplyId)
        {
            return supplyImgDao.querySupplyDescImgBySupplyId(supplyId);
        }

        public SupplyImg getSupplyFirstImg(int supplyId)
        {
            return supplyImgDao.querySupplyFirstImgBySupplyId(supplyId);
        }
        public Boolean addSupplyImg(SupplyImg supplyImg)
        {
            return supplyImgDao.insertSupplyImg(supplyImg);
        }
        public int addSupply(Supply supply)
        {
            supply.Priority = 1;
            supply.CreateTime = DateTime.Now;
            supply.ModifyTime = DateTime.Now;
            supply.SupplyStatus = 1;
            return supplyDao.insertSupply(supply);

        }
        public Boolean updateSupplyByManager(Supply supply)
        {
            return supplyDao.updateSupplyByManager(supply);
        }
        public List<Supply> getAllSupplyListWithoutBanned()
        {

            List<Supply> listSupply = supplyDao.queryAllSupplyWithoutBanned();

            for (int i = 0; i < listSupply.Count; i++)
            {
                User user = userDao.queryUserById(listSupply[i].User.Id);
                listSupply[i].User.NickName = user.NickName;
                listSupply[i].User.TeleNumber = user.TeleNumber;

            }
            return listSupply;
        }
        public List<Supply> getAllSupplyList()
        {
            
            List<Supply> listSupply = supplyDao.queryAllSupply();
            
            for (int i = 0; i < listSupply.Count; i++)
            {
                User user = userDao.queryUserById(listSupply[i].User.Id);
                listSupply[i].User.NickName = user.NickName;
                listSupply[i].User.TeleNumber = user.TeleNumber;
                
            }
            return listSupply;
        }
        public List<Supply> getSupplyListByCategory(int categoryId)
        {
            List<Supply> listSupply = supplyDao.querySupplyByCategory(categoryId);
            for (int i = 0; i < listSupply.Count; i++)
            {
                User user = userDao.queryUserById(listSupply[i].User.Id);
                listSupply[i].User.NickName = user.NickName;
                listSupply[i].User.TeleNumber = user.TeleNumber;
            }
            return listSupply;
        }
        public List<Supply> getSupplyListByName(String name)
        {
            List<Supply> listSupply = supplyDao.querySupplyByName(name);
            for (int i = 0; i < listSupply.Count; i++)
            {
                User user = userDao.queryUserById(listSupply[i].User.Id);
                listSupply[i].User.NickName = user.NickName;
                listSupply[i].User.TeleNumber = user.TeleNumber;
            }
            return listSupply;
        }
        public List<Category> getAllCategory()
        {
            List<Category> list = categoryDao.queryAllCategory();
            if (list.Count > 0) {
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
