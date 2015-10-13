using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Threading;

namespace WebApi
{
    public class ContactsController : ApiController
    {
        [Route("api/contacts")]
        public void Post()
        {
            this.Bind("Post1");
            this.Bind("Post2");
        }

        private void Bind(string methodName)
        {
            //创建FormatterParameterBinding对象
            MethodInfo method = typeof(ContactsController).GetMethod(methodName);
            HttpActionDescriptor actionDescriptor = new ReflectedHttpActionDescriptor(this.ControllerContext.ControllerDescriptor, method);
            HttpParameterDescriptor parameterDescriptor = actionDescriptor.GetParameters().First();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { new JsonMediaTypeFormatter() };
            FormatterParameterBinding parameterBinding = new FormatterParameterBinding(parameterDescriptor, formatters, null);

            //创建HttpActionBinding并执行
            HttpActionBinding actionBinding = new HttpActionBinding(actionDescriptor,new FormatterParameterBinding[] { parameterBinding });
            HttpActionContext actionContext =new HttpActionContext(this.ControllerContext, actionDescriptor);
            try
            {
                actionBinding.ExecuteBindingAsync(actionContext, CancellationToken.None).Wait();

                //获取绑定参数对象并打印相关数据
                Contact contact = (Contact)actionContext.ActionArguments["contact"];
                Console.WriteLine("{0,-12}: {1}", "Name", contact.Name);
                Console.WriteLine("{0,-12}: {1}", "Phone No.", contact.PhoneNo);
                Console.WriteLine("{0,-12}: {1}", "EmailAddress", contact.EmailAddress);
                Console.WriteLine("{0,-12}: {1}", "Address", contact.Address);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Post1(Contact contact)
        { }

        public void Post2(Contact contact = null)
        { }
    }
}
