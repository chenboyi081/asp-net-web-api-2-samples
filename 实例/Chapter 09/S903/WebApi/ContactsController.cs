using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

namespace WebApi
{
    public class ContactsController : ApiController
    {
        [Route("api/contacts")]
        public void Post()
        {
            IEnumerable<MediaTypeFormatter> formatters = new MediaTypeFormatter[] { new FormUrlEncodedMediaTypeFormatter() };
            FormDataCollection formData = this.Request.Content.ReadAsAsync<FormDataCollection>(formatters).Result;
            foreach (var item in formData)
            {
                Console.WriteLine("{0,-12}: {1}", item.Key, item.Value);
            }
        }
    }
}