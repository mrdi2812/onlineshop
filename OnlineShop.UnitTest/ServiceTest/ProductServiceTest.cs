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
    public class ProductServiceTest
    {
        private Mock<IProductRepository> _moqRepository;
        private Mock<IProductTagRepository> _moqProductTagRepository;
        private Mock<ITagRepository> _moqTagRepository;
        private Mock<IUnitOfWork> _moqUnitOfWork;
        private IProductService _productService;
        List<Product> listProduct;

        [TestInitialize]
        public void Initialize()
        {
            _moqRepository = new Mock<IProductRepository>();
            _moqProductTagRepository = new Mock<IProductTagRepository>();
            _moqTagRepository = new Mock<ITagRepository>();
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _productService = new ProductService(_moqRepository.Object, _moqProductTagRepository.Object, _moqTagRepository.Object, _moqUnitOfWork.Object);

            listProduct = new List<Product>
            {
                new Product() {ID=1,Name="1",Status=true },
                new Product() {ID=2,Name="2",Status=true },
                new Product() {ID=3,Name="3",Status=true }
            };
        }
        [TestMethod]
        public void Product_Service_GetAll()
        {
            _moqRepository.Setup(m => m.GetAll(null)).Returns(listProduct);
            var result = _productService.GetAll() as List<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
    }
}
