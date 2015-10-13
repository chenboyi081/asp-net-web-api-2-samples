using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApi;

namespace WebApi.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult CaptureToken(string requestUri)
        {
            ViewBag.RequestUri = requestUri;
            return View();
        }
    }
}