using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Net.Http;
using System.ServiceModel.Channels;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RegisterForDispose(new Foo());
            request.RegisterForDispose(new Bar());
            request.RegisterForDispose(new Baz());

            Type httpMessageType = Type.GetType("System.Web.Http.SelfHost.Channels.HttpMessage, System.Web.Http.SelfHost");
            Message httpMessage = (Message)Activator.CreateInstance(httpMessageType, new object[] { request });
            httpMessage.Close();
        }
    }
}
