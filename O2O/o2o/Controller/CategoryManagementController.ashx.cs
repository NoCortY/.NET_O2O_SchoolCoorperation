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
    /// Summary description for CategoryManagementController
    /// </summary>
    public class CategoryManagementController : IHttpHandler
    {
        CategoryService categoryService = new CategoryService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String mapping = context.Request["action"];
            switch (mapping)
            {
                case "addcategory":addCategory(context);break;
                case "deletecategory": deleteCategory(context); break;
                case "getAllcategory": getAllCategory(context); break;
                default: break;
            }
        }
        public void addCategory(HttpContext context)
        {
            Category category = new Category();
            category.CategoryName = context.Request["categoryName"];
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            if (categoryService.addCategory(category))
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            StringBuilder jsonString = new StringBuilder();
            jsonString = JsonUtil.toJson(dictionary);
            context.Response.Write(jsonString.ToString());
        }
        public void deleteCategory(HttpContext context)
        {
            int categoryId = Convert.ToInt32(context.Request["categoryId"]);
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            if (categoryService.deleteCategory(categoryId))
            {
                dictionary.Add("success", "true");
            }
            else
            {
                dictionary.Add("success", "false");
            }
            StringBuilder jsonString = new StringBuilder();
            jsonString = JsonUtil.toJson(dictionary);
            context.Response.Write(jsonString.ToString());
        }
        public void getAllCategory(HttpContext context){
            context.Request.ContentEncoding = Encoding.UTF8;
            context.Response.ContentEncoding = Encoding.UTF8;
            List<Category> list = categoryService.getAllCategory();
            StringBuilder jsonString = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            jsonString.Append("[");
            int i = 1;
            foreach (Category category in list)
            {
                dictionary.Add("categoryId", category.Id);
                dictionary.Add("categoryName", category.CategoryName);
                dictionary.Add("createTime", category.CreateTime);
                dictionary.Add("modifyTime", category.ModifyTime);
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