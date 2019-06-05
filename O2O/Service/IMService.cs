using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class IMService
    {
        IMDao imDao = new IMDao();
        public List<IM> getMessage(int receiveUserId)
        {
            List<IM> list = new List<IM>();
            list = imDao.queryIMByReceiveUserId(receiveUserId);
            imDao.deleteIMByReceiveUserId(receiveUserId);
            return list;
        }
        public int getMessageCount(int receiveUserId)
        {
            return imDao.queryCountOfMessage(receiveUserId);
        }
        public Boolean sendMessage(IM im)
        {
            return imDao.insertContent(im);
        }

    }
}
