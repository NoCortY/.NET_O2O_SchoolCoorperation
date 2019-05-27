using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace o2o.Utils
{
    public class JsonUtil
    {
        public static StringBuilder toJson(Dictionary<String, Object> dictionary)
        {
            StringBuilder jsonString = new StringBuilder();
            int i = 0;
            jsonString.Append("{");
            foreach(KeyValuePair<string,Object> kvp in dictionary)
            {
                if (i >= 1)
                {
                    jsonString.Append(",");
                }
                i++;
                jsonString.Append("\"" + kvp.Key + "\":"+"\""+kvp.Value+"\"");
            }
            jsonString.Append("}");
            return jsonString;
        }
        /*public static StringBuilder multiToJson(Dictionary<String, Object> dictionary)
        {
            StringBuilder jsonString = new StringBuilder();
            int i = 0;
            jsonString.Append("{{");
            foreach (KeyValuePair<string, Object> kvp in dictionary)
            {
                if (i >= 1)
                {
                    jsonString.Append(",");
                }
                if (i == dictionary.Count)
                {
                    jsonString.Append("},{");
                }
                i++;
                jsonString.Append("\"" + kvp.Key + "\":" + "\"" + kvp.Value + "\"");
            }
            jsonString.Append("}}");
            return jsonString;
        }*/
    }
}
