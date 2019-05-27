using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserEvaluate
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private User receiveUser = new User();

        public User ReceiveUser
        {
            get { return receiveUser; }
            set { receiveUser = value; }
        }
        private User sendUser = new User();

        public User SendUser
        {
            get { return sendUser; }
            set { sendUser = value; }
        }
        private String evaluateContent;

        public String EvaluateContent
        {
            get { return evaluateContent; }
            set { evaluateContent = value; }
        }
    }
}
