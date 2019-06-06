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
    /// Summary description for RequirementManagementController
    /// </summary>
    public class RequirementManagementController : IHttpHandler
    {
        RequirementService requirementService = new RequirementService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String mapping = context.Request["action"];
            switch (mapping)
            {
                case "requirementListManagement": requirementListManagement(context); break;
                case "updateRequirement": updateRequirement(context); break;
                default: break;
            }
        }
        public void updateRequirement(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Requirement requirement = new Requirement();
            requirement.Id = Convert.ToInt32(context.Request.Form["id"].ToString());
            requirement.Priority = Convert.ToInt32(context.Request.Form["priority"].ToString());
            requirement.RequirementStatus = Convert.ToInt32(context.Request.Form["status"].ToString());
            Boolean flag = requirementService.updateRequirementByManager(requirement);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            if (flag)
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            StringBuilder sb = JsonUtil.toJson(dictionary);
            context.Response.Write(sb.ToString());
        }
        public void requirementListManagement(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<Requirement> list = requirementService.getAllRequirementList();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            jsonString.Append("[");
            int i = 1;
            foreach (Requirement requirement in list)
            {
                dictionary.Add("Id", requirement.Id);
                dictionary.Add("requirementName", requirement.RequirementName);
                dictionary.Add("requirementDesc", requirement.RequirementDesc);
                dictionary.Add("categoryId", requirement.RequirementCategory.Id);
                dictionary.Add("priority", requirement.Priority);
                dictionary.Add("userId", requirement.User.Id);
                dictionary.Add("nickName", requirement.User.NickName);
                dictionary.Add("teleNumber", requirement.User.TeleNumber);
                dictionary.Add("createTime", requirement.CreateTime);
                dictionary.Add("modifyTime", requirement.ModifyTime);
                dictionary.Add("requirementStatus", requirement.RequirementStatus);
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