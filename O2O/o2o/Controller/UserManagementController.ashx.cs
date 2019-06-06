using Model;
using o2o.Utils;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace o2o.Controller
{
    /// <summary>
    /// Summary description for UserManagementController
    /// </summary>
    public class UserManagementController : IHttpHandler
    {
        UserService userService = new UserService();
        public void ProcessRequest(HttpContext context)
        {
            String mapping = context.Request["action"];
            switch (mapping)
            {
                case "listUsers": listUsers(context); break;
                case "updateUserStatus": updateUserStatus(context); break;
                default: break;
            }
        }
        public void updateUserStatus(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            User user = new User();
            user.Id = Convert.ToInt32((context.Request["Id"]));
            user.UserStatus = Convert.ToInt32(context.Request["status"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            if (userService.updateUserStatus(user))
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            StringBuilder jsonString = JsonUtil.toJson(dictionary);
            context.Response.Write(jsonString.ToString());
        }
        public void listUsers(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<User> list = userService.listAllUsers();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            jsonString.Append("[");
            int i = 1;
            foreach (User user in list)
            {
                dictionary.Add("Id", user.Id);
                dictionary.Add("username", user.Username);
                dictionary.Add("gender", user.Gender);
                dictionary.Add("userStatus", user.UserStatus);
                dictionary.Add("registerTime", user.RegisterTime);
                dictionary.Add("realName", user.RealName);
                dictionary.Add("teleNumber", user.TeleNumber);
                dictionary.Add("nickName", user.NickName);
                dictionary.Add("success", "true");
                jsonString.Append(JsonUtil.toJson(dictionary));
                if (i < list.Count)
                {
                    jsonString.Append(",");
                }
                i++;
                dictionary.Clear();
            }
            jsonString.Append("]");
            context.Response.Write(jsonString.ToString());
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}