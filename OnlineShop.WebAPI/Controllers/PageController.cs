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
    public class PageController : Controller
    {
        IPageService _pageService;
        // GET: Page
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public ActionResult Index(string alias)
        {
            var model = _pageService.GetByAlias(alias);
            var viewModel = Mapper.Map<Page,PageViewModel>(model);
            return View(viewModel);
        }
    }
}