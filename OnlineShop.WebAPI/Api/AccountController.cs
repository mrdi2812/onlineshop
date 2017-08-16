using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OnlineShop.Web.App_Start;
using System.Web.Http.Results;
using OnlineShop.Service;
using AutoMapper;
using System.Collections.Generic;
using OnlineShop.WebAPI.Models;
using OnlineShop.Model.Models;

namespace OnlineShop.Web.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ICommentService _commentService;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,ICommentService commentService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _commentService = commentService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string userName, string password, bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            var result = await SignInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout: false);
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
  
    }
}
