using o2o.Utils;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace o2o
{
    public class IM : IHttpHandler
    {
        IMService imService = new IMService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String mapping = context.Request["action"];
            switch (mapping)
            {
                case "receivemessage": receiveMessage(context); break;
                case "sendmessage": sendMessage(context); break;
                case "messagecount": messageCount(context); break;
                default: break;
            }
        }
        /*接收消息*/
        public void receiveMessage(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<Model.IM> list = new List<Model.IM>();
            list = imService.getMessage(Convert.ToInt32(context.Session["userId"]));
            StringBuilder sb = new StringBuilder();
            Dictionary<String, Object> dictionary = new Dictionary<String, Object>();
            sb.Append("[");
            int i = 1;
            foreach (Model.IM im in list)
            {
                dictionary.Add("sendUserId", im.SendUserId);
                dictionary.Add("sendUserName", im.SendUserName);
                dictionary.Add("content", im.Content);
                dictionary.Add("sendTime", im.SendTime);
                sb.Append(JsonUtil.toJson(dictionary));
                if (i < list.Count)
                {
                    sb.Append(",");
                }
                dictionary.Clear();
                i++;
            }
            sb.Append("]");
            context.Response.Write(sb.ToString());
        }
        /*转发消息*/
        public void sendMessage(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Model.IM im = new Model.IM();
            im.SendUserId = Convert.ToInt32(context.Session["userId"]);
            im.ReceiveUserId = Convert.ToInt32(context.Request["receiveUserId"]);
            im.SendUserName = context.Request["sendUserName"];
            im.ReceiveUserName = context.Request["receiveUserName"];
            im.Content = context.Request["content"];
            im.SendTime = DateTime.Now;

            Boolean flag = imService.sendMessage(im);
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
        /*只显示消息数量*/
        public void messageCount(HttpContext context)
        {
            int count = imService.getMessageCount(Convert.ToInt32(context.Session["userId"]));
            Dictionary<String, Object> dictionary = new Dictionary<string, object>();
            dictionary.Add("count", count);
            StringBuilder sb = JsonUtil.toJson(dictionary);
            context.Response.Write(sb.ToString());
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