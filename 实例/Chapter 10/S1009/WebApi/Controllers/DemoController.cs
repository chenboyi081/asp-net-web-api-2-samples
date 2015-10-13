using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;
using System.Xml.Linq;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public XElement Get()
        {
            HttpActionContext actionContext = new HttpActionContext
            {
                ControllerContext = this.ControllerContext
            };
            ModelMetadataProvider metadataProvider = this.Configuration.Services.GetModelMetadataProvider();
            ModelMetadata metadata = metadataProvider.GetMetadataForType(null, typeof(IEnumerable<Contact>));
            IValueProvider valueProvider = this.CreateValueProvider();
            ModelBindingContext bindingContext = new ModelBindingContext
            {
                ModelMetadata = metadata,
                ValueProvider = valueProvider,
                ModelState = actionContext.ModelState
            };

            CompositeModelBinder modelBinder = (CompositeModelBinder)new ModelBinderAttribute().GetModelBinder(this.Configuration, typeof(IEnumerable<Contact>));
            modelBinder.BindModel(actionContext, bindingContext);

            XElement root = new XElement("Root");
            this.AddChildNodes(root, bindingContext.ValidationNode);
            return root;
        }

        private IValueProvider CreateValueProvider()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("[0].name", "张三");
            dictionary.Add("[0].phoneNo", "123456789");
            dictionary.Add("[0].emailAddress", "zhangsan@gmail.com");
            dictionary.Add("[0].address.province", "江苏");
            dictionary.Add("[0].address.city", "苏州");
            dictionary.Add("[0].address.district", "工业园区");
            dictionary.Add("[0].address.street", "星湖街328号");

            dictionary.Add("[1].name", "李四");
            dictionary.Add("[1].phoneNo", "987654321");
            dictionary.Add("[1].emailAddress", "lisi@gmail.com");
            dictionary.Add("[1].address.province", "江苏");
            dictionary.Add("[1].address.city", "苏州");
            dictionary.Add("[1].address.district", "工业园区");
            dictionary.Add("[1].address.street", "金鸡湖大道328号");
            return new NameValuePairsValueProvider(dictionary, CultureInfo.InvariantCulture);

        }

        private void AddChildNodes(XElement parent, ModelValidationNode child)
        {
            XElement childElement = new XElement("ModelValidationNode");
            childElement.Add(new XAttribute("ModelStateKey", child.ModelStateKey));
            childElement.Add(new XAttribute("Value", child.ModelMetadata.Model));
            foreach (ModelValidationNode childNode in child.ChildNodes)
            {
                AddChildNodes(childElement, childNode);
            }
            parent.Add(childElement);
        }
    }
}
