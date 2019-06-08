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
    /// Summary description for requirement
    /// </summary>
    public class requirement : IHttpHandler, IRequiresSessionState
    {
        RequirementService requirementService = new RequirementService();
        UserService userService = new UserService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"].ToString();
            switch(action)
            {
                case "all": allRequirementList(context); break;
                case "category": listByCategory(context); break;
                case "name": listByName(context); break;
                case "listcategory": listCategory(context); break;
                case "addrequirement": addRequirement(context); break;
                case "addrequirementimg": addRequirementImg(context); break;
                case "addrequirementdescimg": addRequirementDescImg(context); break;
                case "getrequirementdetail": getRequirementDetail(context); break;
                case "getrequirementdescimg": getRequirementDescImg(context); break;
                default: break;
            }

        }

        public void getRequirementDescImg(HttpContext context)
        {
            List<RequirementImg> list = requirementService.getRequirementDescImg(Convert.ToInt32(context.Request["Id"]));
            StringBuilder sb = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<String, Object>();
            string imgPath = "defaultImg.jpg";
            sb.Append("[");
            int i = 1;
            foreach (RequirementImg requirementImg in list)
            {
                if (requirementImg.ImgPath != null)
                {
                    imgPath = System.IO.Path.GetFileName(requirementImg.ImgPath);
                }
                dictionary.Add("DescImg", imgPath);
                sb.Append(JsonUtil.toJson(dictionary));
                if(i<list.Count)
                {
                    sb.Append(",");
                }
                dictionary.Clear();
                i++;
            }
            sb.Append("]");
            context.Response.Write(sb.ToString());
        }
        public void getRequirementDetail(HttpContext context)
        {
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            Requirement requirement = requirementService.getRequirementById(Convert.ToInt32(context.Request["Id"]));
            User user = userService.getUserById(requirement.User.Id);
            string imgPath = "defaultImg.jpg";
            if(user.UserHeader != null){
                imgPath = System.IO.Path.GetFileName(user.UserHeader);
            }
            dictionary.Add("Id",requirement.Id);
            dictionary.Add("Name",requirement.RequirementName);
            dictionary.Add("Desc",requirement.RequirementDesc);
            dictionary.Add("categoryName",requirementService.getCategoryName(requirement.RequirementCategory.Id));
            dictionary.Add("priority",requirement.Priority);
            dictionary.Add("userId",requirement.User.Id);
            dictionary.Add("userName",user.NickName);
            dictionary.Add("userTeleNum",user.TeleNumber);
            dictionary.Add("userEMail",user.Username);
            dictionary.Add("userHeader",imgPath);
            dictionary.Add("createTime",requirement.CreateTime);
            dictionary.Add("modifyTime",requirement.ModifyTime);
            dictionary.Add("Status",requirement.RequirementStatus);
            StringBuilder sb = new StringBuilder();
            sb.Append(JsonUtil.toJson(dictionary));
            dictionary.Clear();
            context.Response.Write(sb.ToString());
        }
        public void addRequirementDescImg(HttpContext context)
        {
            RequirementImg requirementImg = new RequirementImg();
            requirementImg.Requirement.Id = Convert.ToInt32(context.Request["Id"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string savepath = "";
            Boolean flag = false;
            if (context.Request.Files.Count > 0)
            {
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    HttpPostedFile file1 = context.Request.Files["descImg" + i];
                    savepath = FileUtil.uploadImg(file1, "../images/");
                    requirementImg.ImgPath = savepath;
                    requirementImg.ImgStatus = 1;
                    flag = requirementService.addRequirementImg(requirementImg);
                }
            }
            if(flag)
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            context.Response.Write(JsonUtil.toJson(dictionary).ToString());
        }
        public void addRequirementImg(HttpContext context)
        {
            RequirementImg requirementImg = new RequirementImg();
            requirementImg.Requirement.Id = Convert.ToInt32(context.Request["Id"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string savepath = "";
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file1 = context.Request.Files["smallImg"];
                savepath = FileUtil.uploadImg(file1, "../images/");
            }
            requirementImg.ImgPath = savepath;
            requirementImg.ImgStatus = 0;
            Boolean flag = requirementService.addRequirementImg(requirementImg);
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
        public void addRequirement(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Requirement requirement = new Requirement();
            requirement.RequirementName = context.Request["Name"];
            requirement.RequirementDesc = context.Request["Desc"];
            requirement.RequirementCategory.Id = Convert.ToInt32(context.Request["CategoryId"]);
            requirement.User.Id = Convert.ToInt32(context.Session["userId"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            int id = requirementService.addRequirement(requirement);
            if (id > 0)
            {
                dictionary.Add("Id", id);
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            context.Response.Write(JsonUtil.toJson(dictionary).ToString());
        }
        public void allRequirementList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<Requirement> list = requirementService.getAllRequirementListWithoutBanned();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();

            jsonString.Append("[");
            int i = 1;
            foreach (Requirement requirement in list)
            {
                string imgPath = "defaultImg.jpg";
                RequirementImg requirementImg = requirementService.getRequirementFirstImg(requirement.Id);
                if (requirementImg.ImgPath != null&&requirementImg.ImgPath!="")
                {
                    imgPath = System.IO.Path.GetFileName(requirementImg.ImgPath);
                }
                dictionary.Add("Id", requirement.Id);
                dictionary.Add("Name", requirement.RequirementName);
                dictionary.Add("Desc", requirement.RequirementDesc);
                dictionary.Add("categoryId", requirement.RequirementCategory.Id);
                dictionary.Add("priority", requirement.Priority);
                dictionary.Add("userId", requirement.User.Id);
                dictionary.Add("nickName", requirement.User.NickName);
                dictionary.Add("teleNumber", requirement.User.TeleNumber);
                dictionary.Add("createTime", requirement.CreateTime);
                dictionary.Add("modifyTime", requirement.ModifyTime);
                dictionary.Add("Status", requirement.RequirementStatus);
                dictionary.Add("FirstImgPath", imgPath);
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
        public void listByCategory(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<Requirement> list = requirementService.getRequirementListByCategory(Convert.ToInt32(context.Request["categoryId"].ToString()));
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string imgPath = "hotel1.jpg";
            if (list.Count == 0)
            {
                context.Response.Write("{\"success\":\"false\"}");
                return;
            }
            jsonString.Append("[");
            int i = 1;
            foreach (Requirement requirement in list)
            {
                RequirementImg requirementImg = requirementService.getRequirementFirstImg(requirement.Id);
                if (requirementImg.ImgPath != null)
                {
                    imgPath = System.IO.Path.GetFileName(requirementImg.ImgPath);
                }
                dictionary.Add("Id", requirement.Id);
                dictionary.Add("Name", requirement.RequirementName);
                dictionary.Add("Desc", requirement.RequirementDesc);
                dictionary.Add("categoryId", requirement.RequirementCategory.Id);
                dictionary.Add("priority", requirement.Priority);
                dictionary.Add("userId", requirement.User.Id);
                dictionary.Add("nickName", requirement.User.NickName);
                dictionary.Add("teleNumber", requirement.User.TeleNumber);
                dictionary.Add("createTime", requirement.CreateTime);
                dictionary.Add("modifyTime", requirement.ModifyTime);
                dictionary.Add("Status", requirement.RequirementStatus);
                dictionary.Add("FirstImgPath", imgPath);
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
        public void listByName(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.UTF8;
            context.Response.ContentEncoding = Encoding.UTF8;
            List<Requirement> list = requirementService.getRequirementListByName(context.Request["searchname"].ToString());
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            //jsonString.Append("\"supply\":");
            string imgPath = "hotel1.jpg";
            if (list.Count == 0)
            {
                context.Response.Write("{\"success\":\"false\"}");
                return;
            }
            jsonString.Append("[");
            int i = 1;
            foreach (Requirement requirement in list)
            {
                RequirementImg requirementImg = requirementService.getRequirementFirstImg(requirement.Id);
                if (requirementImg.ImgPath != null)
                {
                    imgPath = System.IO.Path.GetFileName(requirementImg.ImgPath);
                }

                dictionary.Add("Id", requirement.Id);
                dictionary.Add("Name", requirement.RequirementName);
                dictionary.Add("Desc", requirement.RequirementDesc);
                dictionary.Add("categoryId", requirement.RequirementCategory.Id);
                dictionary.Add("priority", requirement.Priority);
                dictionary.Add("userId", requirement.User.Id);
                dictionary.Add("nickName", requirement.User.NickName);
                dictionary.Add("teleNumber", requirement.User.TeleNumber);
                dictionary.Add("createTime", requirement.CreateTime);
                dictionary.Add("modifyTime", requirement.ModifyTime);
                dictionary.Add("Status", requirement.RequirementStatus);
                dictionary.Add("FirstImgPath", imgPath);
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
        public void listCategory(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.UTF8;
            context.Response.ContentEncoding = Encoding.UTF8;
            List<Category> list = requirementService.getAllCategory();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            jsonString.Append("[");
            int i = 1;
            foreach (Category category in list)
            {
                dictionary.Add("categoryId", category.Id);
                dictionary.Add("categoryName", category.CategoryName);
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