using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public HttpResponseMessage Get()
        {
            Address address = new Address
            {
                Province = "江苏省",
                City = "苏州市",
                District = "工业园区",
                Street = "星湖街328号"
            };
            Contact contact = new Contact
            {
                Name = "张三",
                PhoneNo = "123456789",
                EmailAddress = "zhansan@gmail.com",
                Address = address
            };
            IBodyModelValidator validator = new DefaultBodyModelValidator();
            HttpActionContext actionContext = new HttpActionContext
            {
                ControllerContext = this.ControllerContext
            };
            ModelMetadataProvider metadataProvider =actionContext.GetMetadataProvider();
            validator.Validate(contact, typeof(Contact), metadataProvider,actionContext, "contact");
            return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest,actionContext.ModelState);
        }
    }
}
