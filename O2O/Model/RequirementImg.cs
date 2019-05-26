using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class RequirementImg
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
        private int imgStatus;

        public int ImgStatus
        {
            get { return imgStatus; }
            set { imgStatus = value; }
        }
        private Requirement requirement;

        public Requirement Requirement
        {
            get { return requirement; }
            set { requirement = value; }
        }
    }
}
