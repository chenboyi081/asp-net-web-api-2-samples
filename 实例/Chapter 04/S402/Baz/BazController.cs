using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Baz
{
    public class BazController: ApiController
    {
        public string Get()
        {
            return this.GetType().AssemblyQualifiedName;
        }
    }
}
