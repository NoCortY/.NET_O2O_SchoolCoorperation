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
    /// Summary description for supply
    /// </summary>
    public class supply : IHttpHandler
    {
        SupplyService supplyService = new SupplyService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";            
            string action = context.Request["action"].ToString();            
            switch (action)            
            {                
                case "all":allSupplyList(context);break;
                case "category": listByCategory(context); break;
                case "name": listByName(context); break;
                case "listcategory": listCategory(context); break;
                case "addsupply": addSupply(context); break;
                case "addsupplyimg": addSupplyImg(context); break;
                case "addsupplydescimg": addSupplyDescImg(context); break;
                default:break;            
            }

        }
        public void addSupplyDescImg(HttpContext context){
            SupplyImg supplyImg = new SupplyImg();
            supplyImg.Supply.Id = Convert.ToInt32(context.Request["supplyId"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string savepath = "";
            Boolean flag = false;
            if (context.Request.Files.Count > 0)
            {
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    HttpPostedFile file1 = context.Request.Files["descImg"+i];
                    savepath = FileUtil.uploadImg(file1, "../images/");
                    supplyImg.ImgPath = savepath;
                    supplyImg.ImgStatus = 1;
                    flag = supplyService.addSupplyImg(supplyImg);
                }
            }

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
        public void addSupplyImg(HttpContext context)
        {
            SupplyImg supplyImg = new SupplyImg();
            supplyImg.Supply.Id = Convert.ToInt32(context.Request["supplyId"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            string savepath = "";
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file1 = context.Request.Files["smallImg"];
                savepath = FileUtil.uploadImg(file1, "../images/");
            }
            supplyImg.ImgPath = savepath;
            supplyImg.ImgStatus = 0;
            Boolean flag = supplyService.addSupplyImg(supplyImg);


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
        public void addSupply(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Supply supply = new Supply();
            supply.SupplyName = context.Request["Name"];
            supply.SupplyDesc = context.Request["Desc"];
            supply.SupplyCategory.Id = Convert.ToInt32(context.Request["CategoryId"]);
            //supply.User.Id = Convert.ToInt32(context.Session["userId"]);
            supply.User.Id = 1;//暂时
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            int id = supplyService.addSupply(supply);
            if (id>0)
            {
                dictionary.Add("supplyId", id);
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            context.Response.Write(JsonUtil.toJson(dictionary).ToString());
            
        }
        public void allSupplyList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<Supply> list = supplyService.getAllSupplyListWithoutBanned();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            jsonString.Append("[");
            int i = 1;
            foreach(Supply supply in list){
                dictionary.Add("Id", supply.Id);
                dictionary.Add("supplyName", supply.SupplyName);
                dictionary.Add("supplyDesc",supply.SupplyDesc);
                dictionary.Add("categoryId", supply.SupplyCategory.Id);
                dictionary.Add("priority", supply.Priority);
                dictionary.Add("userId",supply.User.Id);
                dictionary.Add("nickName", supply.User.NickName);
                dictionary.Add("teleNumber", supply.User.TeleNumber);
                dictionary.Add("createTime",supply.CreateTime);
                dictionary.Add("modifyTime", supply.ModifyTime);
                dictionary.Add("supplyStatus", supply.SupplyStatus);
                dictionary.Add("success", "true");
                jsonString.Append(JsonUtil.toJson(dictionary));
                if (i < list.Count) {
                    jsonString.Append(",");
                }
                i++;
                dictionary.Clear();
            }
            jsonString.Append("]");
            context.Response.Write(jsonString.ToString());
        }
        public void listByCategory(HttpContext context){
            context.Response.ContentType = "text/plain";
            List<Supply> list = supplyService.getSupplyListByCategory(Convert.ToInt32(context.Request["categoryId"].ToString()));
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            //jsonString.Append("\"supply\":");
            if (list.Count == 0)
            {
                context.Response.Write("{\"success\":\"false\"}");
                return;
            }
            jsonString.Append("[");
            int i = 1;
            foreach(Supply supply in list){
                dictionary.Add("Id", supply.Id);
                dictionary.Add("supplyName", supply.SupplyName);
                dictionary.Add("supplyDesc",supply.SupplyDesc);
                dictionary.Add("categoryId", supply.SupplyCategory.Id);
                dictionary.Add("priority", supply.Priority);
                dictionary.Add("userId",supply.User.Id);
                dictionary.Add("nickName", supply.User.NickName);
                dictionary.Add("teleNumber", supply.User.TeleNumber);
                dictionary.Add("createTime",supply.CreateTime);
                dictionary.Add("modifyTime", supply.ModifyTime);
                dictionary.Add("supplyStatus", supply.SupplyStatus);
                dictionary.Add("success", "true");
                jsonString.Append(JsonUtil.toJson(dictionary));
                if (i < list.Count) {
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
            List<Supply> list = supplyService.getSupplyListByName(context.Request["searchname"].ToString());
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            //jsonString.Append("\"supply\":");
            if (list.Count == 0)
            {
                context.Response.Write("{\"success\":\"false\"}");
                return;
            }
            jsonString.Append("[");
            int i = 1;
            foreach (Supply supply in list)
            {
                dictionary.Add("Id", supply.Id);
                dictionary.Add("supplyName", supply.SupplyName);
                dictionary.Add("supplyDesc", supply.SupplyDesc);
                dictionary.Add("categoryId", supply.SupplyCategory.Id);
                dictionary.Add("priority", supply.Priority);
                dictionary.Add("userId", supply.User.Id);
                dictionary.Add("nickName", supply.User.NickName);
                dictionary.Add("teleNumber", supply.User.TeleNumber);
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
        public void listCategory(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Request.ContentEncoding = Encoding.UTF8;
            context.Response.ContentEncoding = Encoding.UTF8;
            List<Category> list = supplyService.getAllCategory();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String,Object> dictionary = new Dictionary<string,object>();
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