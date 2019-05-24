using o2o.entity;
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
    /// Summary description for register
    /// </summary>
    public class register : IHttpHandler
    {
        UserService userService = new UserService();
        public void ProcessRequest(HttpContext context)
        {

            /*键值对用来存储要转换成JSON数据串的数据*/
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            /*判定用户是否存在*/
            Boolean userExist = userService.isUserExist(context.Request["username"]);
            /*将来用来存放Json数据的容器*/
            StringBuilder sb = new StringBuilder();
            context.Response.ContentType = "text/plain";
            if (userExist == true)
            {
                /*如果用户已存在*/
                dictionary.Add("success", false);
                sb = JsonUtil.toJson(dictionary);
                context.Response.Write(sb.ToString());
            }
            else
            {
                /*如果用户不存在*/
                User user = new User();
                user.Username = context.Request["username"];
                user.Password = context.Request["password"];
                user.NickName = context.Request["nickname"];
                user.RealName = context.Request["realname"];
                user.TeleNumber = context.Request["telenumber"];
                user.Gender = context.Request["gender"];
                user.UserHeader = context.Request["userheader"];
                Boolean flag = userService.register(user);

                /*存放注册是否成功的标识 success*/
                if (flag == true)
                {
                    dictionary.Add("success", "true");
                }
                else
                {
                    dictionary.Add("success", "false");
                }


                /*将收到的请求通过自己设置的工具类转换成JSON字符串*/
                sb = JsonUtil.toJson(dictionary);
                context.Response.Write(sb.ToString());
            }
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