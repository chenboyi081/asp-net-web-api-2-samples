using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace WebApi
{
    [AttributeUsage( AttributeTargets.Class| AttributeTargets.Method, AllowMultiple = false)]
    public class HandleErrorAttribute: ExceptionFilterAttribute
    {
        public string ExceptionPolicyName { get; private set; }
        public string DefaultErrorMessage { get; private set; }

        public HandleErrorAttribute(string exceptionPolicyName, string defaultErrorMessage = "服务端发生异常")
        {
            this.ExceptionPolicyName = exceptionPolicyName;
            this.DefaultErrorMessage = defaultErrorMessage;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (null != actionExecutedContext.Exception)
            { 
                Exception exceptionToThrow;
                ExceptionPolicy.HandleException(actionExecutedContext.Exception, this.ExceptionPolicyName, out exceptionToThrow);
                if (null != exceptionToThrow)
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exceptionToThrow);
                }
                else
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, this.DefaultErrorMessage);
                }
            }
        }
    }
}