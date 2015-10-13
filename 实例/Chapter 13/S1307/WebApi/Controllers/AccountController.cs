using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class AccountController : Controller
    {
        static Dictionary<string, string> userAccounts;

        static AccountController()
        {
            userAccounts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            userAccounts.Add("Foo", "Password");
            userAccounts.Add("Bar", "Password");
            userAccounts.Add("Baz", "Password");
        }

        [RequireHttps]
        public ActionResult Login()
        {
            return View(new Credential());
        }

        [HttpPost]
        public ActionResult Login(Credential loginInfo, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string password;
            if (userAccounts.TryGetValue(loginInfo.UserName, out password))
            {
                if (password == loginInfo.Password)
                {
                    FormsAuthentication.SetAuthCookie(loginInfo.UserName, false);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("Password", "密码不正确");
            }
            else
            {
                ModelState.AddModelError("UserName", "用户名不存在");
            }
            return View();
        }
    }
}
