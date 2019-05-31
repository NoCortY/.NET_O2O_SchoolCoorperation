using Dao;
using Model;
using System;
using System.Collections.Generic;
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
