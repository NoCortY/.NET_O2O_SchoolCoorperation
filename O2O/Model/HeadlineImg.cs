using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HeadlineImg
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String imgPath;
        public String ImgPath
        {
            get { return imgPath; }
            set { imgPath = value; }
        }
    }
}
