using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RequirementService
    {
        RequirementDao requirementDao = new RequirementDao();
        CategoryDao categoryDao = new CategoryDao();
        UserDao userDao = new UserDao();
        RequirementImgDao requirementImgDao = new RequirementImgDao();
        public Boolean updateRequirement(Requirement requirement, List<RequirementImg> list)
        {
            requirement.ModifyTime = DateTime.Now;
            if (list.Count != 0)
            {
                //删除原有的图片
                RequirementImg requirementImg = requirementImgDao.queryRequirementFirstImgByRequirementId(requirement.Id);
                List<RequirementImg> listExsit = requirementImgDao.queryRequirementDescImgByRequirementId(requirement.Id);
                listExsit.Add(requirementImg);
                foreach (RequirementImg s in listExsit)
                {
                    if (s.ImgPath != null)
                    {
                        File.Delete(s.ImgPath);
                    }
                }
                requirementImgDao.deleteRequirementImgByRequirementId(requirement.Id);

                //插入新图片
                foreach (RequirementImg s in list)
                {
                    requirementImgDao.insertRequirementImg(s);
                }
            }
            return requirementDao.updateRequirementById(requirement);
        }
        public Boolean removeRequirementById(int requirementId)
        {
            RequirementImg requirementImg = requirementImgDao.queryRequirementFirstImgByRequirementId(requirementId);
            List<RequirementImg> list = requirementImgDao.queryRequirementDescImgByRequirementId(requirementId);
            list.Add(requirementImg);
            foreach (RequirementImg s in list)
            {
                if (s.ImgPath != null)
                {
                    File.Delete(s.ImgPath);
                }
            }
            Boolean flagRequirementImg = requirementImgDao.deleteRequirementImgByRequirementId(requirementId);
            Boolean flagRequirement = requirementDao.deleteRequirementById(requirementId);
            return flagRequirement;
        }
        public List<Requirement> getMyRequirement(int userId)
        {
            List<Requirement> list = requirementDao.queryRequirementByUserId(userId);
            return list;
        }
        public String getCategoryName(int id)
        {
            return requirementDao.queryCategoryNameById(id);

        }
        public Requirement getRequirementById(int id)
        {
            return requirementDao.queryRequirementById(id);
        }
        public List<RequirementImg> getRequirementDescImg(int requirementId)
        {
            return requirementImgDao.queryRequirementDescImgByRequirementId(requirementId);
        }
        public RequirementImg getRequirementFirstImg(int requirementId)
        {
            return requirementImgDao.queryRequirementFirstImgByRequirementId(requirementId);
        }
        public Boolean addRequirementImg(RequirementImg requirementImg)
        {
            return requirementImgDao.insertRequirementImg(requirementImg);
        }
        public int addRequirement(Requirement requirement)
        {
            requirement.Priority = 1;
            requirement.CreateTime = DateTime.Now;
            requirement.ModifyTime = DateTime.Now;
            requirement.RequirementStatus = 1;
            return requirementDao.insertRequirement(requirement);

        }
        public Boolean updateRequirementByManager(Requirement requirement)
        {
            return requirementDao.updateRequirementByManager(requirement);
        }
        public List<Requirement> getAllRequirementListWithoutBanned()
        {

            List<Requirement> listRequirement = requirementDao.queryAllRequirementWithoutBanned();

            for (int i = 0; i < listRequirement.Count; i++)
            {
                User user = userDao.queryUserById(listRequirement[i].User.Id);
                listRequirement[i].User.NickName = user.NickName;
                listRequirement[i].User.TeleNumber = user.TeleNumber;

            }
            return listRequirement;
        }
        public List<Requirement> getAllRequirementList()
        {

            List<Requirement> listRequirement = requirementDao.queryAllRequirement();

            for (int i = 0; i < listRequirement.Count; i++)
            {
                User user = userDao.queryUserById(listRequirement[i].User.Id);
                listRequirement[i].User.NickName = user.NickName;
                listRequirement[i].User.TeleNumber = user.TeleNumber;

            }
            return listRequirement;
        }
        public List<Requirement> getRequirementListByCategory(int categoryId)
        {
            List<Requirement> listRequirement = requirementDao.queryRequirementByRequirementCategory(categoryId);
            for (int i = 0; i < listRequirement.Count; i++)
            {
                User user = userDao.queryUserById(listRequirement[i].User.Id);
                listRequirement[i].User.NickName = user.NickName;
                listRequirement[i].User.TeleNumber = user.TeleNumber;
            }
            return listRequirement;
        }
        public List<Requirement> getRequirementListByName(String name)
        {
            List<Requirement> listRequirement = requirementDao.queryRequirementByRequirementName(name);
            for (int i = 0; i < listRequirement.Count; i++)
            {
                User user = userDao.queryUserById(listRequirement[i].User.Id);
                listRequirement[i].User.NickName = user.NickName;
                listRequirement[i].User.TeleNumber = user.TeleNumber;
            }
            return listRequirement;
        }
        public List<Category> getAllCategory()
        {
            List<Category> list = categoryDao.queryAllCategory();
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }


    }
}
