using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.WebAPI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineShop.WebAPI.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        IContactDetailService _contactDetailService;
        public ContactController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }
        public ActionResult Index()
        {
            var model = _contactDetailService.GetDefaultContact();
            var viewModel = Mapper.Map<ContactDetail,ContactDetailViewModel>(model);
            return View(viewModel);
        }
    }
}