using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class MoviesController : ApiController
    {
        [Route("api/movies/{genre}")]
        public IDictionary<string, object> GetMoviesByGenre(string genre)
        {
            return this.RequestContext.RouteData.Values;
        }

        [Route("api/movies/{id:int}")]
        public IDictionary<string, object> GetMovieById(int id)
        {
            return this.RequestContext.RouteData.Values;
        }
    }
}