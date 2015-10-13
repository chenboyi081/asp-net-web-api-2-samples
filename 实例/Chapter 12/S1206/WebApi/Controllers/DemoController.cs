using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        [Foobar]
        public string Get()
        {
            return "这是执行目标Action方法的结果";
        }
    }

    public class FoobarAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = null;
            actionExecutedContext.Exception = new Exception("在执行ActionFilter的OnActionExecuted方法过程中发生异常");
        }
    }
}