using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspacts.Autofac.Caching;
using Core.Aspacts.Autofac.Performance;
using Core.Aspacts.Autofac.Transaction;
using Core.Aspacts.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IPoductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }
        [SecuredOperation("product.add, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
           IResult result= BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
              CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExcaded());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            if (DateTime.Now.Hour == 11)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId ==id));
        }


        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product> (_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>> (_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }


        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }


        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();  //Any(): Datada böyle bir veri var mı yok mu kontrol eder.
            if (result )
            {
                return new ErrorResult(Messages.ProductNameAlreadyExistsr);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExcaded()
        {
            var result = _categoryService.GetAll(); 
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

         [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
