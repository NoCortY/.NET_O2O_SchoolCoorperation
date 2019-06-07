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
    /// Summary description for ordinaryUserManage
    /// </summary>
    public class ordinaryUserManage : IHttpHandler, IRequiresSessionState
    {
        SupplyService supplyService = new SupplyService();
        RequirementService requirementService = new RequirementService();
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
                case "deletemysupply": deleteMySupply(context); break;
                case "deletemyrequirement": deleteMyRequirement(context); break;
                case "updatemysupply": updateMySupply(context); break;
                case "updatemyrequirement":updateMyRequirement(context); break;
                default: break;
            }
        }
        public void updateMyRequirement(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Requirement requirement = new Requirement();
            requirement.Id = Convert.ToInt32(context.Request["Id"]);
            requirement.RequirementName = context.Request["Name"];
            requirement.RequirementDesc = context.Request["Desc"];
            requirement.RequirementCategory.Id = Convert.ToInt32(context.Request["CategoryId"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string savepath = "";
            List<RequirementImg> list = new List<RequirementImg>();
            if (context.Request.Files.Count > 0)
            {
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    RequirementImg requirementImg = new RequirementImg();
                    requirementImg.Requirement.Id = Convert.ToInt32(context.Request["Id"]);
                    HttpPostedFile file1 = context.Request.Files["descImg" + i];
                    requirementImg.ImgStatus = Convert.ToInt32(context.Request["status" + i]);
                    savepath = FileUtil.uploadImg(file1, "../images/");
                    requirementImg.ImgPath = savepath;
                    list.Add(requirementImg);
                }
            }

            Boolean flag = requirementService.updateRequirement(requirement, list);
            if (flag)
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            context.Response.Write(JsonUtil.toJson(dictionary).ToString());
        }
        public void updateMySupply(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Supply supply = new Supply();
            supply.Id = Convert.ToInt32(context.Request["Id"]);
            supply.SupplyName = context.Request["Name"];
            supply.SupplyDesc = context.Request["Desc"];
            supply.SupplyCategory.Id = Convert.ToInt32(context.Request["CategoryId"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string savepath = "";
            List<SupplyImg> list = new List<SupplyImg>();
            if (context.Request.Files.Count > 0)
            {
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    SupplyImg supplyImg = new SupplyImg();
                    supplyImg.Supply.Id = Convert.ToInt32(context.Request["Id"]);
                    HttpPostedFile file1 = context.Request.Files["descImg" + i];
                    supplyImg.ImgStatus = Convert.ToInt32(context.Request["status"+i]);
                    savepath = FileUtil.uploadImg(file1, "../images/");
                    supplyImg.ImgPath = savepath;
                    list.Add(supplyImg);
                }
            }

            Boolean flag = supplyService.updateSupply(supply,list);
            if (flag)
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            context.Response.Write(JsonUtil.toJson(dictionary).ToString());
        }
        public void deleteMyRequirement(HttpContext context)
        {
            int requirementId = Convert.ToInt32(context.Request["Id"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            StringBuilder jsonString = new StringBuilder();
            if (requirementService.removeRequirementById(requirementId))
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            jsonString.Append(JsonUtil.toJson(dictionary));
            context.Response.Write(jsonString.ToString());
        }
        public void deleteMySupply(HttpContext context)
        {
            int supplyId = Convert.ToInt32(context.Request["Id"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            StringBuilder jsonString = new StringBuilder();
            if (supplyService.removeSupplyById(supplyId))
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            jsonString.Append(JsonUtil.toJson(dictionary));
            context.Response.Write(jsonString.ToString());
        }
        public void supplyManage(HttpContext context)
        {
            if (context.Session["userId"] != null)
            {
                int userId = Convert.ToInt32(context.Session["userId"].ToString());
                List<Supply> list = supplyService.getMySupply(userId);
                StringBuilder jsonString = new StringBuilder();
                Dictionary<String, Object> dictionary = new Dictionary<string, object>();
                //jsonString.Append("\"supply\":");
                jsonString.Append("[");
                int i = 1;
                foreach (Supply supply in list)
                {
                    dictionary.Add("Id", supply.Id);
                    dictionary.Add("supplyName", supply.SupplyName);
                    dictionary.Add("priority", supply.Priority);
                    dictionary.Add("createTime", supply.CreateTime);
                    dictionary.Add("modifyTime", supply.ModifyTime);
                    dictionary.Add("supplyStatus", supply.SupplyStatus);
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
        }
        public void requirementmanage(HttpContext context)
        {
            if (context.Session["userId"] != null)
            {
                int userId = Convert.ToInt32(context.Session["userId"].ToString());
                List<Requirement> list = requirementService.getMyRequirement(userId);
                StringBuilder jsonString = new StringBuilder();
                Dictionary<String, Object> dictionary = new Dictionary<string, object>();
                //jsonString.Append("\"supply\":");
                jsonString.Append("[");
                int i = 1;
                foreach (Requirement requirement in list)
                {
                    dictionary.Add("Id", requirement.Id);
                    dictionary.Add("requirementName", requirement.RequirementName);
                    dictionary.Add("priority", requirement.Priority);
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