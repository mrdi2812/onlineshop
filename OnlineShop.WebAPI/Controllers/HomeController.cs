using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCatgoryService;
        IComonService _commonService;
        IProductService _productService;
        public HomeController(IProductCategoryService productCategoryService,IComonService commonService,IProductService productService)
        {
            this._productCatgoryService = productCategoryService;
            this._commonService = commonService;
            this._productService = productService;
        }
        [OutputCache(Duration =60,Location =System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlide();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>,IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;
            var lastestProductModel = _productService.GetLastest(3);
            var topSaleProductModel = _productService.GetTopSale(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer,FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Category()
        {
            var model = _productCatgoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}