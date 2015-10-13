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
            if (ModelState.IsValid)
            {
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);
            }
        }
    }
}