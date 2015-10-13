using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bar
{
    public class BarController: ApiController
    {
        public string Get()
        {
            return this.GetType().AssemblyQualifiedName;
        }
    }
}
