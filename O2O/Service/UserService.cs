using Dao;
using Model;
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
        /*注册用户*/
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
        /*用户登录*/
        public User userLogin(String username, String password)
        {
            
            User user = userDao.queryUserByUsername(username);
            if (user == null||!(password.Equals(user.Password)))
            {
                return null;
            }
            return user;
        }


        /*查看用户是否已存在*/
        public Boolean isUserExist(String username)
        {
            Boolean userFlag = userDao.userExist(username);
            return userFlag;
        }
    }
}
