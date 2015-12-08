# asp-net-web-api-2-samples
《ASP.NET Web API 2 框架揭秘》中的示例

##第3章
<p>S301 HttpServer对消息处理管道的构建</p>
> 自定义MessageHandler
 
	public class FooHandler : DelegatingHandler{}

    public class BarHandler : DelegatingHandler{}

    public class BazHandler : DelegatingHandler{}

<p>S302 匿名Principal的设置</p>
<p>S303 验证HttpRoutingDispatcher的路由功能</p>
<p>S304 验证HttpControllerHandler的路由功能</p>
<p>S305 自定义HttpMessageHandler实现HTTP方法重写</p>
<p>S306 直接利用HttpBinding进行请求的接收和响应</p>
<p>S307 创建自定义HttpServer模拟HttpSelfHostServer的工作原理</p>
