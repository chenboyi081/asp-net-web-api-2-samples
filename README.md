# asp-net-web-api-2-samples
《ASP.NET Web API 2 框架揭秘》中的示例

##第3章
<p>S301 HttpServer对消息处理管道的构建</p>
> 该示例，自定义MessageHandler，利用HttpConfiguration注册自定义的DelegatingHandler，比较创建自定义的HttpServer和初始化后的HttpServer的整个消息处理管道的结构。
> ![](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_QQ%E6%88%AA%E5%9B%BE20151208183522.png)
<p>S302 匿名Principal的设置</p>
> Principal可以简单看成是身份（Identity）与权限（Permission）的封装，它是当前现成安全上下文的一部分。对于HttpServer来说，当它的SendAsync方法被执行的时候，如果当前现成的CurrentPrincipal属性表示的Principal不存在，它会为止创建一个代表“匿名身份的Principal”。
该示例在两种情况下调用调用自定义的HttpServer对象的SendAsync方法。
第一次调用发生在当前当前线程的Principal为Null的情况下,第二次调用之前创建了一个用户名为“Artech”的CenericPrincipal对象作为当前线程的Principal。在这两次sendAsync方法调用之后,我们获取当前线程 的 Principal并将其转换成CenericPrincipal对象。
> ![S302图片](http://images.cnblogs.com/cnblogs_com/chenboyi081/764976/o_2.png)
<p>S303 验证HttpRoutingDispatcher的路由功能</p>
<p>S304 验证HttpControllerHandler的路由功能</p>
<p>S305 自定义HttpMessageHandler实现HTTP方法重写</p>
<p>S306 直接利用HttpBinding进行请求的接收和响应</p>
<p>S307 创建自定义HttpServer模拟HttpSelfHostServer的工作原理</p>
