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
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _moqRepository;
        private Mock<IUnitOfWork> _moqUnitOfWork;
        private IPostCategoryService _categoryService;
        List<PostCategory> listCategory;
        [TestInitialize]
        public void Initialize()
        {
            _moqRepository = new Mock<IPostCategoryRepository>();
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_moqRepository.Object, _moqUnitOfWork.Object);
            listCategory = new List<PostCategory>
            {
                new PostCategory() {ID=1,Name="1",Status=true },
                new PostCategory() {ID=2,Name="2",Status=true },
                new PostCategory() {ID=3,Name="3",Status=true }
            };
        }
        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            _moqRepository.Setup(m => m.GetAll(null)).Returns(listCategory);
            var result = _categoryService.GetAll() as List<PostCategory>;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test 1";
            postCategory.Alias = "Test Alias";
            postCategory.Status = true;
            _moqRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _categoryService.Add(postCategory);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
