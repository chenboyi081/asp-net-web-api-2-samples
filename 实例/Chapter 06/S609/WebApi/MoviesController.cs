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

        [HttpGet]
        [Route("api/movies/starring/{starring}")]
        [Route("api/movies/starring/{starring}/{genre}")]
        [Route("api/movies/director/{director}/{genre}")]
        public IEnumerable<Movie> FindMovies(string starring, string director, string genre)
        {
            throw new NotImplementedException();
        }
    }
}