using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EvaluateService
    {
        UserEvaluateDao userEvaluateDao = new UserEvaluateDao();
        public Boolean addEvaluate(UserEvaluate userEvaluate)
        {
            return userEvaluateDao.insertUserEvaluate(userEvaluate);
        }
        public List<UserEvaluate> getAllEvaluate(int receiveUserId)
        {
            List<UserEvaluate> list = userEvaluateDao.queryUserEvaluateByReceiveId(receiveUserId);
            return list;
        }
    }
}
