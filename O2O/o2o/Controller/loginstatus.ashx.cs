using o2o.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace o2o.Controller
{
    /// <summary>
    /// Summary description for loginstatus
    /// </summary>
    public class loginstatus : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            context.Response.ContentType = "text/plain";
            if (context.Session["userId"]!= null)
            {
                //dictionary.Add("userId", context.Session["userId"].ToString());
                dictionary.Add("nickname", context.Session["nickname"].ToString());
                dictionary.Add("userStatus", context.Session["userStatus"].ToString());
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