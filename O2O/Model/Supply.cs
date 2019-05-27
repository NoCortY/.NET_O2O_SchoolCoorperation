using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Supply
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String supplyName;

        public String SupplyName
        {
            get { return supplyName; }
            set { supplyName = value; }
        }
        private Category supplyCategory = new Category();

        public Category SupplyCategory
        {
            get { return supplyCategory; }
            set { supplyCategory = value; }
        }
        private String supplyDesc;

        public String SupplyDesc
        {
            get { return supplyDesc; }
            set { supplyDesc = value; }
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
        private int supplyStatus;

        public int SupplyStatus
        {
            get { return supplyStatus; }
            set { supplyStatus = value; }
        }
    }
}
