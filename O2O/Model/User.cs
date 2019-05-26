using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class User
    {

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String username;
        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        private String password;
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        private String nickName;

        public String NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        private String userHeader;
        public String UserHeader
        {
            get { return userHeader; }
            set { userHeader = value; }
        }

        private String gender;

        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }
       

        private int userStatus;
        public int UserStatus
        {
            get { return userStatus; }
            set { userStatus = value; }
        }

        private DateTime registerTime;
        public DateTime RegisterTime
        {
            get { return registerTime; }
            set { registerTime = value; }
        }

        private String realName;
        public String RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        private String teleNumber;
        public String TeleNumber
        {
            get { return teleNumber; }
            set { teleNumber = value; }
        }

    }
}