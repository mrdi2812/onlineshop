using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.UnitTest.ServiceTest
{
    [TestClass]
    public class ErrorServiceTest
    {
        private Mock<IErrorRepository> _moqRepository;
        private Mock<IUnitOfWork> _moqUnitOfWork;
        private IErrorService _categoryService;
        List<Error> listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _moqRepository = new Mock<IErrorRepository>();
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new ErrorService(_moqRepository.Object, _moqUnitOfWork.Object);
            listCategory = new List<Error>
            {
                new Error() {ID=1,Messeage="test",StackTrace="test"},
                new Error() {ID=2,Messeage="test2",StackTrace="test2"},

            };
        }
        [TestMethod]
        public void Error_Service_Create()
        {
            Error postCategory = new Error();
            postCategory.Messeage = "Test 1";
            postCategory.StackTrace = "Test Alias";
            postCategory.CreateDate = DateTime.Now;
            _moqRepository.Setup(m => m.Add(postCategory)).Returns((Error p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _categoryService.Create(postCategory);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
