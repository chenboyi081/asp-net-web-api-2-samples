using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

namespace WebApi
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private List<IDisposable> disposableServices = new List<IDisposable>();
        public IKernel Kernel { get; private set; }

        public NinjectDependencyResolver(NinjectDependencyResolver parent)
        {
            this.Kernel = parent.Kernel;
        }

        public NinjectDependencyResolver()
        {
            this.Kernel = new StandardKernel();
        }

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            this.Kernel.Bind<TFrom>().To<TTo>();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyResolver(this);
        }

        public object GetService(Type serviceType)
        {
            return this.Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            foreach (var service in this.Kernel.GetAll(serviceType))
            {
                this.AddDisposableService(service);
                yield return service;
            }
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in disposableServices)
            {
                disposable.Dispose();
            }
        }

        private void AddDisposableService(object servie)
        {
            IDisposable disposable = servie as IDisposable;
            if (null != disposable && !disposableServices.Contains(disposable))
            {
                disposableServices.Add(disposable);
            }
        }
    }
}