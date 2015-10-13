using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {

        [HttpGet]
        [Route("action1/{x}/{y}/{z}")]
        public DemoModel Action1(int x, int y, int z)
        {
            return new DemoModel { X = x, Y = y, Z = z };
        }

        [HttpGet]
        [Route("action2/{x}/{y}/{z}")]
        public DemoModel Action2(DemoModel model)
        {
            return model;
        }

        [HttpGet]
        [Route("action3/{x}/{y}/{z}")]
        public IEnumerable<DemoModel> Action3(DemoModel model1, DemoModel model2)
        {
            yield return model1;
            yield return model2;
        }

        [HttpGet]
        [Route("action4/{model1.x}/{model1.y}/{model1.z}/{model2.x}/{model2.y}/{model2.z}")]
        public IEnumerable<DemoModel> Action4(DemoModel model1, DemoModel model2)
        {
            yield return model1;
            yield return model2;
        }
    }
}
