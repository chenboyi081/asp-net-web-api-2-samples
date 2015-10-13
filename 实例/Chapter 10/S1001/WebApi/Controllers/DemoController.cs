using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public HttpResponseMessage Get([FromUri]Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                ModelState.AddModelError("Name", "'Name'是必需字段");
            }

            if (string.IsNullOrEmpty(person.Gender))
            {
                ModelState.AddModelError("Gender", "'Gender'是必需字段");
            }
            else if (!new string[] { "M", "F" }.Any(g => string.Compare(person.Gender, g, true) == 0))
            {
                ModelState.AddModelError("Gender", "有效'Gender'必须是'M','F'之一");
            }

            if (null == person.Age)
            {
                ModelState.AddModelError("Age", "'Age'是必需字段");
            }
            else if (person.Age > 25 || person.Age < 18)
            {
                ModelState.AddModelError("Age", "有效'Age'必须在18到25周岁之间");
            }

            if (ModelState.IsValid)
            {
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}