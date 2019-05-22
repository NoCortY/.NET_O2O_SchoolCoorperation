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
    /// Summary description for register
    /// </summary>
    public class register : IHttpHandler
    {
        UserService userService = new UserService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String name = context.Request["username"];
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            dictionary.Add("username", name);
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