using Business.Abstract;
using Core.Utilities.Results;
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

        public IDataResult<List<Category>> GetAll()
        {
            // iş kodları
            return new SuccessDataResult<List<Category>> ( _caregoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_caregoryDal.Get(c=> c.CategoryId == categoryId));
        }
    }
}
