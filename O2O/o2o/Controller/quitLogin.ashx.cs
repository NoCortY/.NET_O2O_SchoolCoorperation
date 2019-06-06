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
    /// Summary description for quitLogin
    /// </summary>
    public class quitLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Session.Clear();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            dictionary.Add("success", "true");
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append(JsonUtil.toJson(dictionary));
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