using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EaseLogMeIn.WebApp.Controllers
{
    public class AccountController : BaseController
    {
        readonly IWebUsersManager webUsers;
        public AccountController(IWebUsersManager _webUsers, IExceptionLoger loger) : base(loger)
        {
            webUsers = _webUsers;
        }
        [HttpGet]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            webUsers.Add(new Users { UserId = "admin" });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(Users user)
        {
            if (WebAppUtility.IsEmptyText(user.UserId) || WebAppUtility.IsEmptyText(user.Password))
            {
                ViewBag.Error = "Invalid UserId or Password!!";
            }
            else
            {
                Users users1 = webUsers.Authenticate(user.UserId, user.Password);
                if (users1 == null)
                {
                    ViewBag.Error = "Invalid UserId or Password!!";
                }
                else
                {
                    CreateCookies(users1);
                    return RedirectToAction("Index", "Website");
                }
            }
            return View();
        }

        private string CreateCookies(Users users1)
        {
            string data = users1.UniqueId.ToString();
            FormsAuthentication.SetAuthCookie(users1.Name, false);
            var authTicket = new FormsAuthenticationTicket(1, users1.Name, DateTime.Now, DateTime.Now.AddDays(1), true, data);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
            return authCookie.Value;
        }

        [HttpPost]
        public JsonResult LoginToAPI(Users users)
        {
            AuthResponse auth = new AuthResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    auth.Status = false;
                    return Json(auth);
                }
                Users users1 = webUsers.Authenticate(users.UserId, users.Password);
                if (users1 != null)
                {
                    auth.Status = true;
                    auth.IPAddress = Request.UserHostAddress;
                    auth.Token = CreateCookies(users1);
                }
            }
            catch (Exception ex)
            {
                auth.Status = false;
                auth.Token = ex.Message;
            }
            return Json(auth);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    ViewBag.UniqueId = authTicket.UserData;
                }
            }

            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ChangePassword(ChangePassword user)
        {
            ViewBag.UniqueId = user.UniqueId;
            if (ModelState.IsValid)
            {
                int result = webUsers.ChangePassword(user.UniqueId, user.NewPassword, user.OldPassword);
                if (result > 0)
                {
                    FormsAuthentication.SignOut();
                }
                ViewBag.Result = result;
            }
            return View(user);
        }
    }
}