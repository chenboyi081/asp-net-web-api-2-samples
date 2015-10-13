using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost.Channels;

namespace WebApi
{
    public class MyHttpSelfHostServer : HttpServer
    {
        public Uri BaseAddress { get; private set; }
        public IChannelListener<IReplyChannel> ChannelListener { get; private set; }

        public MyHttpSelfHostServer(HttpConfiguration configuration, Uri baseAddress)
            : base(configuration)
        {
            this.BaseAddress = baseAddress;
        }

        public void Open()
        {
            HttpBinding binding = new HttpBinding();
            this.ChannelListener = binding.BuildChannelListener<IReplyChannel>(this.BaseAddress);
            this.ChannelListener.Open();

            IReplyChannel channnel = this.ChannelListener.AcceptChannel();
            channnel.Open();

            while (true)
            {
                RequestContext requestContext = channnel.ReceiveRequest(TimeSpan.MaxValue);
                Message message = requestContext.RequestMessage;
                MethodInfo method = message.GetType().GetMethod("GetHttpRequestMessage");
                HttpRequestMessage request = (HttpRequestMessage)method.Invoke(message, new object[] { true });
                Task<HttpResponseMessage> processResponse = base.SendAsync(request, new CancellationTokenSource().Token);
                processResponse.ContinueWith(task =>
                    {
                        string httpMessageTypeName = "System.Web.Http.SelfHost.Channels.HttpMessage, System.Web.Http.SelfHost";
                        Type httpMessageType = Type.GetType(httpMessageTypeName);
                        Message reply = (Message)Activator.CreateInstance(httpMessageType, new object[] { task.Result });
                        requestContext.Reply(reply);
                    });
            }
        }

        public void Close()
        {
            if (null != this.ChannelListener && this.ChannelListener.State == CommunicationState.Opened)
            {
                this.ChannelListener.Close();
            }
        }
    }
}
