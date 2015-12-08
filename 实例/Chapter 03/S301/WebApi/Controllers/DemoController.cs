using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public Tuple<IEnumerable<string>, IEnumerable<string>> Get()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.MessageHandlers.Add(new FooHandler());          //利用HttpConfiguration注册3个自定义的DelegatingHandler
            configuration.MessageHandlers.Add(new BarHandler());
            configuration.MessageHandlers.Add(new BazHandler());

            MyHttpServer httpServer = new MyHttpServer(configuration);       //创建自定义的HttpServer
            IEnumerable<string> chain1 = this.GetHandlerChain(httpServer).ToArray();
            httpServer.Initialize();                             //调用HttpServer的Initialize方法，从而初始化整个消息处理管道   
            IEnumerable<string> chain2 = this.GetHandlerChain(httpServer).ToArray();
            return new Tuple<IEnumerable<string>, IEnumerable<string>>(chain1, chain2);     //两个字符串集合组成一个Tuple<IEnumerable<string>, IEnumerable<string>>对象并将其作为Get方法的返回值，这个Tuple反映了HttpMessageHandler链在HttpServer初始化前后的结构
        }

        /// <summary>
        /// 获得MyHttpServer牵头的HttpMessageHandler链
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        private IEnumerable<string> GetHandlerChain(DelegatingHandler handler)
        {
            yield return handler.GetType().Name;
            while (null != handler.InnerHandler)
            {
                yield return handler.InnerHandler.GetType().Name;
                handler = handler.InnerHandler as DelegatingHandler;
                if (null == handler)
                {
                    break;
                }
            }
        }
    }
}
