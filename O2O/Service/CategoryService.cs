using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService
    {
        CategoryDao categoryDao = new CategoryDao();
        public Boolean addCategory(Category category){
            category.CreateTime = DateTime.Now;
            category.ModifyTime = DateTime.Now;
            return categoryDao.insertCategory(category);
        }
        public Boolean deleteCategory(int categoryId)
        {
            return categoryDao.deleteCategory(categoryId);
        }
        public List<Category> getAllCategory()
        {
            return categoryDao.queryAllCategory();
        }
    }
}
