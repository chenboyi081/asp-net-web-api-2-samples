using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MoviesController : ApiController
    {
        [Route("api/movies/{genre}")]
        public IEnumerable<Movie> GetMoviesByGenre(string genre)
        {
            throw new NotImplementedException();
        }

        [Route("api/movies/{id:int}")]
        public Movie GetMovieById(int id)
        {
            throw new NotImplementedException();
        }
    }
}