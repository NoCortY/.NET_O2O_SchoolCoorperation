using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace o2o.Controller
{
    /// <summary>
    /// Summary description for ordinaryUserManage
    /// </summary>
    public class ordinaryUserManage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String mapping = context.Request["action"];
            switch (mapping)
            {
                case "supplymanage": supplyManage(context); break;
                case "requirementmanage": requirementmanage(context); break;
                case "accountmanage": accountmanage(context); break;
                case "evaluatemanage": evaluateManage(context); break;
                default: break;
            }
        }
        public void supplyManage(HttpContext context)
        {

        }
        public void requirementmanage(HttpContext context)
        {

        }
        public void accountmanage(HttpContext context)
        {

        }
        public void evaluateManage(HttpContext context)
        {

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