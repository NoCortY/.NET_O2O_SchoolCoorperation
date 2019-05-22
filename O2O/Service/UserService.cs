using Dao;
using o2o.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        UserDao userDao = new UserDao();
        public Boolean register(User user)
        {
            if (user.UserHeader == null)
            {
                user.UserHeader = "";
            }
            if (user.RealName == null)
            {
                user.RealName = "";
            }
            if (user.TeleNumber == null)
            {
                user.TeleNumber = "";
            }
            user.RegisterTime = DateTime.Now;
            user.UserStatus = 1;
            Boolean flag = userDao.insertUser(user);
            return flag;
        }
    }
}
