using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeesController : ApiControllerBase
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this.Request.GetRouteData().Values.Add("name", "张三");
            this.Request.GetRouteData().Values.Add("gender", "男");
            this.Request.GetRouteData().Values.Add("department", "IT部");
        }

        public Employee Get([FromUri]Employee employee)
        {
            return employee;
        }
    }
}
