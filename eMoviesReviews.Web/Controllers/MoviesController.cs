using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eMoviesReviews.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eMoviesReviews.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        public MoviesController(IHostingEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49487/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Logging:Api:Token"]);
        }
        
        public IActionResult DodajFilm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DodajFilm(string name,string description,DateTime realeaseDate,IFormFile picture)
        {
            string webRoot = _env.WebRootPath;
            var imgArray = new MemoryStream();

            picture.CopyTo(imgArray);

            Filmovi film = new Filmovi
            {
                Naslov = name,
                Opis = description,
                DatumRealizacije = realeaseDate,
                Slika = imgArray.ToArray()
            };
            //Post result
            var content = new StringContent(JsonConvert.SerializeObject(film), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/Movies/AddMovie", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Content("Uspjesno dodan film novi");
            }
            else
            {
                return Content(response.Content.ToString());
            }
        }
        public IActionResult OcijeniFilm()
        {
            List<esp_GetAllMovies_Result> viewmodel = new List<esp_GetAllMovies_Result>();

            UriBuilder uri = new UriBuilder(string.Concat(client.BaseAddress, "api/Movies/AllMovies"));
            HttpResponseMessage response = client.GetAsync(uri.Uri).Result;

            if (response.IsSuccessStatusCode)
            {
                viewmodel = response.Content.ReadAsAsync<List<esp_GetAllMovies_Result>>().Result;
            }
            return View(viewmodel);
        }
    }
}