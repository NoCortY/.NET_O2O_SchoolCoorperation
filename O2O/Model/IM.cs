using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class IM
    {
        private int sendUserId;

        public int SendUserId
        {
            get { return sendUserId; }
            set { sendUserId = value; }
        }
        private int receiveUserId;

        public int ReceiveUserId
        {
            get { return receiveUserId; }
            set { receiveUserId = value; }
        }
        private String sendUserName;

        public String SendUserName
        {
            get { return sendUserName; }
            set { sendUserName = value; }
        }
        private String receiveUserName;

        public String ReceiveUserName
        {
            get { return receiveUserName; }
            set { receiveUserName = value; }
        }
        private String content;

        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private DateTime sendTime;

        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }
    }
}
