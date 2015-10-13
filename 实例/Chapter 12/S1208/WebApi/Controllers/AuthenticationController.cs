using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [HandleError("defaultPolicy")]
    public class AuthenticationController : ApiController
    {
        static Dictionary<string, string> userAcccounts;
        static AuthenticationController()
        {
            userAcccounts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            userAcccounts.Add("foo", "password");
            userAcccounts.Add("bar", "password");
            userAcccounts.Add("baz", "password");
        }
        [HttpGet]
        [Route("api/auth/{username}/{password}")]
        public string Validate(string userName, string password)
        {
            string pwd;
            if (userAcccounts.TryGetValue(userName, out pwd))
            {
                if (password != pwd)
                {
                    throw new InvalidPasswordException();
                }
                return "认证成功";
            }
            throw new InvalidUserNameException();
        }
    }
}
