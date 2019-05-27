using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Requirement
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String requirementName;

        public String RequirementName
        {
            get { return requirementName; }
            set { requirementName = value; }
        }

        private String requirementDesc;

        public String RequirementDesc
        {
            get { return requirementDesc; }
            set { requirementDesc = value; }
        }

        private int priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private User user = new User();

        public User User
        {
            get { return user; }
            set { user = value; }
        }
        private Category requirementCategory = new Category();

        public Category RequirementCategory
        {
            get { return requirementCategory; }
            set { requirementCategory = value; }
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

        private int requirementStatus;

        public int RequirementStatus
        {
            get { return requirementStatus; }
            set { requirementStatus = value; }
        }

    }
}