using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        //无参数
        public string Get()
        {
            return "DemoController.Get()";
        }
        [HttpGet]
        [ActionName("Get")]
        public string Retrieve()
        {
            return "DemoController.Retrieve()";
        }

        //一个参数
        public string Get(string x)
        {
            return "DemoController.Get(string x)";
        }

        //两个参数
        public string Get(string x, string y)
        {
            return "DemoController.Get(string x, string y)";
        }
        public string Get(int x, int y)
        {
            return "DemoController.Get(int x, int y)";
        }

        //Put, Post & Delete
        public string Put()
        {
            return "DemoController.Put()";
        }
        public string Post()
        {
            return "DemoController.Post()";
        }
        public string Delete()
        {
            return "DemoController.Delete()";
        }
    }
}
