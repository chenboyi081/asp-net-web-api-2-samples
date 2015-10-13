using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebApi.Properties;

namespace WebApi.Controllers
{
    public class ResourcesController : ApiController
    {
        [Route("api/resources/{name}/{culture:culture=zh-cn}")]
        public string GetString(string name, string culture)
        {
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                return Resources.ResourceManager.GetString(name.ToLower());
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = currentUICulture;
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }
    }
}
