using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi
{
    public class UnityHttpControllerActivator : IHttpControllerActivator
    {
        public IUnityContainer UnityContainer { get; private set; }

        public UnityHttpControllerActivator(IUnityContainer unityContainer)
        {
            this.UnityContainer = unityContainer;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController)this.UnityContainer.Resolve(controllerType);
        }
    }
}