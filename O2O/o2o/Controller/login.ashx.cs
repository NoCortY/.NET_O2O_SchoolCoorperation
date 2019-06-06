using Model;
using o2o.Utils;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace o2o.Controller
{
    /// <summary>
    /// Summary description for index
    /// </summary>
    public class index : IHttpHandler,IRequiresSessionState
    {
        UserService userService = new UserService();
        public void ProcessRequest(HttpContext context)
        {
            User user = new User();
            Dictionary<String,Object> dictionary = new Dictionary<string,object>();
            String username = context.Request["username"];
            String password = context.Request["password"];
            user = userService.userLogin(username, password);
            if (user.UserStatus == -1)
            {
                dictionary.Add("success", "banned");
            }
            else if (user != null && user.UserStatus != -1)
            {
                context.Session["userId"] = user.Id;
                context.Session["nickname"] = user.NickName;
                context.Session["userStatus"] = user.UserStatus;
                dictionary.Add("userStatus", user.UserStatus);
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            StringBuilder sb = JsonUtil.toJson(dictionary);
            context.Response.Write(sb.ToString());
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