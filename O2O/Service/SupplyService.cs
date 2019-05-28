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
        UserDao userDao = new UserDao();
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
    }
}
