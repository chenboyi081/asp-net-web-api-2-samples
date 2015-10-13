using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi
{
    public class DemoController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public void GetXxx() { }

        [HttpGet]
        [HttpPost]
        public void PostXxx() { }

        [HttpGet]
        [HttpPost]
        public void PutXxx() { }

        [HttpGet]
        [HttpPost]
        public void DeleteXxx() { }

        [HttpGet]
        [HttpPost]
        public void HeadXxx() { }

        [HttpGet]
        [HttpPost]
        public void OptionsXxx() { }

        [HttpGet]
        [HttpPost]
        public void PatchXxx() { }

        [HttpGet]
        [HttpPost]
        public void Other() { }
    }
}