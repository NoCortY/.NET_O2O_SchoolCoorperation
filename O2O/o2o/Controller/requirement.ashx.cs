using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace o2o.Controller
{
    /// <summary>
    /// Summary description for requirement
    /// </summary>
    public class requirement : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

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