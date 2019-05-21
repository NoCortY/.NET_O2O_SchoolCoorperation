using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace o2o.entity
{
    public class RequirementCategory
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String categoryName;
        public String CategoryName
        {
          get { return categoryName; }
          set { categoryName = value; }
        }

        private String categoryDesc;

        public String CategoryDesc
        {
            get { return categoryDesc; }
            set { categoryDesc = value; }
        }

        private String categoryImg;

        public String CategoryImg
        {
            get { return categoryImg; }
            set { categoryImg = value; }
        }

        private int priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        private int parentId;

        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        private DateTime modifyTime;

        public DateTime ModifyTime
        {
            get { return modifyTime; }
            set { modifyTime = value; }
        }
    }
}