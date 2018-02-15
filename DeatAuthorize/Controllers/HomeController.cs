using DeatAuthorize.Common;
using DeatAuthorize.Models;
using DeatAuthorize.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeatAuthorize.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [DeatAuthorize(Order = 3)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [DeatAuthorize(Order = 2)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            List<AccountModel> ListAccount = new List<AccountModel>()
            {
            new AccountModel()
            {
                Username = "Member",
                Password = "123",
                Role = 1
            },
            new AccountModel()
            {
                Username = "Moderator",
                Password = "123",
                Role = 2
            },
            new AccountModel()
            {
                Username = "Admin",
                Password = "123",
                Role = 3
            }
            };
            var res = ListAccount.Count(x => x.Username == login.Username && x.Password == login.Password);

            if (res > 0)
            {
                var user = ListAccount.FirstOrDefault(x => x.Username == login.Username);
                var userSession = new User();
                userSession.Username = user.Username;
                userSession.Role = user.Role;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
            }
            return View();
        }
        public ActionResult Loguot()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Home/Login");
        }
        public ActionResult NotAuthorize()
        {
            return View();
        }
    }
}