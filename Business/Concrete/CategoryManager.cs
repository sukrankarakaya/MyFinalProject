using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _caregoryDal;

        public CategoryManager(ICategoryDal caregoryDal)
        {
            _caregoryDal = caregoryDal;
        }

        public List<Category> GetAll()
        {
            // iş kodları
            return _caregoryDal.GetAll();
        }

        public Category GetById(int categoryId)
        {
            return _caregoryDal.Get(c=> c.CategoryId == categoryId);
        }
    }
}
