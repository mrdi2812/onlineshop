using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineShop.Common;
using OnlineShop.Model.Models;
using OnlineShop.Web.App_Start;
using OnlineShop.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.WebAPI.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        public AccountController()
        {

        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var userEmail =await _userManager.FindByEmailAsync(model.Email);
            if (userEmail != null)
            {
                ModelState.AddModelError("email","Email đã tồn tại");
                return View(model);         
            }
            var userName =await _userManager.FindByNameAsync(model.UserName);
            if (userName != null)
            {
                ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                return View(model);
            }
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = false,
                BirthDay = DateTime.Now,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName
            };
            await _userManager.CreateAsync(user, model.Password);
            var useraccount = await _userManager.FindByEmailAsync(model.Email);
            if (useraccount != null)
            {
                await _userManager.AddToRolesAsync(useraccount.Id, new string[] { "User" });

                string confirmemail = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/confirm_mail.html"));
                confirmemail = confirmemail.Replace("{{UserName}}", useraccount.FullName);
                var callbackUrl = Url.Action("SentEmailConfirm", "Account", new { Token = useraccount.Id, Email= useraccount.Email},Request.Url.Scheme);
                //confirmemail = confirmemail.Replace("{{Link}}",ConfigHelper.GetByKey("CurrentLink")+ "sent-confirm-email.html?Token="+ useraccount.Id+"&Email="+ useraccount.Email);
                confirmemail = confirmemail.Replace("{{Link}}", callbackUrl);
                MailHelper.SendMail(useraccount.Email, "Xác thực tài khoản", confirmemail);
                
               

                //return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                TempData["SuccessMsg"] = "Đăng ký thành công";
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> SentEmailConfirm (string Token, string Email)
        {
            ApplicationUser user =await _userManager.FindByIdAsync(Token);

            if (Token!= null && Email == user.Email)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"));
                content = content.Replace("{{UserName}}", user.FullName);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");
                MailHelper.SendMail(user.Email, "Đăng ký thành công", content);
                await SignInAsync(user, isPersistent: false);
                return RedirectToAction("ConfirmEmail", "Account", new { Email = user.Email });

                
            }
            else
            {
                ModelState.AddModelError("email", "Tài khoản chưa được xác minh");
                return RedirectToAction("Confirm","Account",new {Email=user.Email});
            }  

        }
        [Authorize]
        public ActionResult ConfirmEmail(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }
    }
}