# asp-net-web-api-2-samples
《ASP.NET Web API 2 框架揭秘》中的示例

##第3章
###S301 HttpServer对消息处理管道的构建
该示例，自定义MessageHandler，利用HttpConfiguration注册自定义的DelegatingHandler，比较创建自定义的HttpServer和初始化后的HttpServer的整个消息处理管道的结构。
 ![](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_QQ%E6%88%AA%E5%9B%BE20151208183522.png)
###S302 匿名Principal的设置
 Principal可以简单看成是身份（Identity）与权限（Permission）的封装，它是当前现成安全上下文的一部分。对于HttpServer来说，当它的SendAsync方法被执行的时候，如果当前现成的CurrentPrincipal属性表示的Principal不存在，它会为止创建一个代表“匿名身份的Principal”。
该示例在两种情况下调用调用自定义的HttpServer对象的SendAsync方法。
第一次调用发生在当前当前线程的Principal为Null的情况下,第二次调用之前创建了一个用户名为“Artech”的CenericPrincipal对象作为当前线程的Principal。在这两次sendAsync方法调用之后,我们获取当前线程 的 Principal并将其转换成CenericPrincipal对象。
 ![S302图片](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_2.png)
###S303 验证HttpRoutingDispatcher的路由功能
     MyHttpRoutingDispatcher dispatcher =
    new MyHttpRoutingDispatcher(configuration);
    await dispatcher.SendAsync(request, CancellationToken.None);//await方法等待异步方法SendAsync执行结束
![S303](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_s303.png)
###S304 验证HttpControllerHandler的路由功能
![s304](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_S304.png)
###S305 自定义HttpMessageHandler实现HTTP方法重写
请求头自定义“X-HTTP-Method-Override”，覆盖或者重写请求自身的HTTP方法,实现PUT和Delete的支持。
![](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_S305.png)
###S306 直接利用HttpBinding进行请求的接收和响应
			Uri listenUri = new Uri("http://127.0.0.1:3721");
            Binding binding = new HttpBinding();

            //创建、开启信道监听器
            IChannelListener<IReplyChannel> channelListener = binding.BuildChannelListener<IReplyChannel>(listenUri);   //调用BuildChannelListener<IReplyChannel>方法创建一个channelListener管道
            channelListener.Open();

            //创建、开启回复信道
            IReplyChannel channel = channelListener.AcceptChannel(TimeSpan.MaxValue);
            channel.Open();

            //开始监听
            while (true)
            {
                //接收输出请求消息
                RequestContext requestContext =
                    channel.ReceiveRequest(TimeSpan.MaxValue);
                PrintRequestMessage(requestContext.RequestMessage);
                //消息回复
                requestContext.Reply(CreateResponseMessage());
            }
![S306](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_S306.png)
###S307 创建自定义HttpServer模拟HttpSelfHostServer的工作原理
自定义一个MyHttpSelfHostServer继承自HttpServer用以模拟HttpSelfHostServer的工作原理。
Main函数中的代码：

    	static void Main(string[] args)
	    {
	    using (MyHttpSelfHostServer httpServer = new MyHttpSelfHostServer(new HttpConfiguration(), new Uri("http://127.0.0.1:3721")))
	    {
	    httpServer.Configuration.Routes.MapHttpRoute(
	    name: "DefaultApi",
	    routeTemplate: "api/{controller}/{id}",
	    defaults: new { id = RouteParameter.Optional });
	    
	    httpServer.Open();
	    Console.Read();
	    }
	    } 
![s307](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_S307.png)
