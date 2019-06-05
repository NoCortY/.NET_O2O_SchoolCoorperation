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
    /// Summary description for EvaluateController
    /// </summary>
    public class EvaluateController : IHttpHandler
    {
        EvaluateService evaluateService = new EvaluateService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String mapping = context.Request["action"];
            switch(mapping){
                case "createuserevaluate":createUserEvaluate(context);break;
                default:break;
            }
        }
        public void createUserEvaluate(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            UserEvaluate userEvaluate = new UserEvaluate();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            StringBuilder jsonString  = new StringBuilder();
            if (context.Session["userId"] != null) { 
                 userEvaluate.SendUser.Id = Convert.ToInt32(context.Session["userId"]);
            }
            userEvaluate.ReceiveUser.Id = Convert.ToInt32(context.Request["receiveUser"]);
            userEvaluate.EvaluateContent = context.Request["evaluateContent"];
            if (evaluateService.addEvaluate(userEvaluate) && context.Session["userId"]!=null)
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            jsonString = JsonUtil.toJson(dictionary);
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