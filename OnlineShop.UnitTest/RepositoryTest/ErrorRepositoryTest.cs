using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;

namespace OnlineShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class ErrorRepositoryTest
    {
        IDbFactory dbFactory;
        IErrorRepository objCategoryRepository;
        IUnitOfWork unitOfWork;
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objCategoryRepository = new ErrorRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void Error_Repository_Create()
        {
            Error category = new Error();
            category.Messeage = "test";
            category.StackTrace = "test";
            category.CreateDate = DateTime.Now;

            var result = objCategoryRepository.Add(category);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}