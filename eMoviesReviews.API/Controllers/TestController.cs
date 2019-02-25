using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eMoviesReviews.API.Models;

namespace eMoviesReviews.API.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        eMovies2019Entities _context = new eMovies2019Entities();

        [HttpGet]
        public List<esp_SelectFromUsers_Result> GetTest()
        {
            return _context.esp_SelectFromUsers().ToList();
        }
        [Route("Nesto")]
        public HttpResponseMessage Nesto()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "hello");
        }
    }
}
