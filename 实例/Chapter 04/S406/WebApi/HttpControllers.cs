using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
    public class DisposableObject : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("{0}.Dispose()", this.GetType().Name);
        }
    }

    public class Foo : DisposableObject
    { }

    public class Bar : DisposableObject
    { }

    public class Baz : DisposableObject
    { }

}



