using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eMoviesReviews.Web.Models;
using System.Net.Http;
using eMoviesReviews.API.Models;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace eMoviesReviews.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private readonly HttpClient client;
        private readonly IHostingEnvironment _env;

        public HomeController(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49487/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Logging:Api:Token"]);
        }

        //Get top 10 rated moveies (also tv shows)
        public IActionResult Index(string tvShow = "", double lastId = 0)
        {
            UriBuilder uri = new UriBuilder(string.Concat(client.BaseAddress, "api/Movies/Top10Movies"));
            if (!string.IsNullOrEmpty(tvShow))
                uri.Query = "tvShow=" + tvShow;

            //For loading ten more results
            if (lastId != 0)
                uri.Query= "lastId=" + lastId;

            if(lastId!=0 && !string.IsNullOrEmpty(tvShow))
            {
                uri.Query = null;
                uri.Query = "tvShow=" + tvShow + "&" + "lastId=" + lastId;
            }

            ViewBag.Bearer = _configuration["Logging:Api:Token"];

            List<esp_GetTop10RatedMovies_Result> viewmodel = new List<esp_GetTop10RatedMovies_Result>();
            
            //Get result
            HttpResponseMessage response = client.GetAsync(uri.Uri).Result;

            if (response.IsSuccessStatusCode)
            {
                viewmodel = response.Content.ReadAsAsync<List<esp_GetTop10RatedMovies_Result>>().Result;
                uri.Query = null;

                return View(viewmodel);
            }
            else
            {
                return Content("Dogodila se neka greska u uspostavljanju veze sa servisom ");
            }
        }
      
    }
}
