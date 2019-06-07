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
    /// Summary description for EvaluateController
    /// </summary>
    public class EvaluateController : IHttpHandler, IRequiresSessionState
    {
        EvaluateService evaluateService = new EvaluateService();
        UserService userService = new UserService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String mapping = context.Request["action"];
            switch(mapping){
                case "createuserevaluate":createUserEvaluate(context);break;
                case "getalluserevaluate": getAllEvaluate(context); break;
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
                 userEvaluate.ReceiveUser.Id = Convert.ToInt32(context.Request["receiveUserId"]);
                 userEvaluate.EvaluateContent = context.Request["evaluateContext"];
                 if (evaluateService.addEvaluate(userEvaluate) && context.Session["userId"] != null)
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
            else
            {
                context.Response.Write("<script>alert('请先登录')</script>");
            }
        }
        public void getAllEvaluate(HttpContext context)
        {
            int receiveUserId = Convert.ToInt32(context.Request["receiveUserId"]);
            List<UserEvaluate> list = evaluateService.getAllEvaluate(receiveUserId);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            int i = 1;
            foreach (UserEvaluate userEvaluate in list)
            {

                dictionary.Add("sendUserName", userService.getUserById(userEvaluate.SendUser.Id).NickName);
                dictionary.Add("content", userEvaluate.EvaluateContent);
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