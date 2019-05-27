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
        public List<Supply> getAllSupplyList()
        {
            List<Supply> list = supplyDao.queryAllSupply();
            return list;
        }
    }
}
