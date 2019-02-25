using eMoviesReviews.API.JsonModel;
using eMoviesReviews.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace eMoviesReviews.API.Controllers
{
    [Authorize]
    public class MoviesController : ApiController
    {
        private readonly eMovies2019Entities _context = new eMovies2019Entities();

        [ActionName("AddMovie")]
        [HttpPost]
        public IHttpActionResult AddMovie([FromBody]Filmovi film)
        {
            //Add new movie to the db
            if (!ModelState.IsValid)
                return BadRequest();

            if (film != null)
            {
                _context.Filmovi.Add(film);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [Route("api/Movies/Top10Movies/{tvShow?}/{lastId?}")]
        [HttpGet]
        public List<esp_GetTop10RatedMovies_Result> GetTop10Movies(string tvShow = "", double lastId = 0)
        {
            //Load ten more results
            if (lastId != 0)
            {
                if (string.IsNullOrEmpty(tvShow))
                {
                    return _context.esp_LoadTenMoreMoviesResults(lastId).ToList();
                }
                return _context.esp_LoadTenMoreTvShowsResults(lastId, "TV SHOW").ToList();
            }
            string param = tvShow;
            //Get top 10 rated movies
            if (tvShow == "")
                return _context.esp_GetTop10RatedMovies().ToList();
            else
            {
                //Get top 10 rated tv shows
                return _context.esp_GetTop10RatedTVshows(param).ToList();
            }
        }
        [Route("api/Movies/AllMovies")]
        [HttpGet]
        public List<esp_GetAllMovies_Result> GetAllMovies()
        {
            return _context.esp_GetAllMovies().ToList();
        }
        [Route("api/Movies/SearchMovies")]
        [HttpPost]
        public List<esp_SearchMovie_Result> SearchMovies([FromBody]SearchRoot root)
        {
            Regex regexForExactRate = new Regex(@"^[0-9](?:\.[0-9])? stars$");
            Regex regexAtLeastRate = new Regex(@"^at least [0-9](?:\.[0-9])? stars$");
            Regex laterDate = new Regex(@"^after \d{4}$");
            Regex earlierDate = new Regex(@"^older than \d{4} years$");
            double number = 0;

            //Match a star number
            if (!Regex.IsMatch(root.podaci, @"^[A-Za-z]+$"))
            {
                 number = Convert.ToDouble(Regex.Match(root.podaci, @"\d+(?:\.[0-9])?").Value);
            }

            //Search for exact average rating
            if (regexForExactRate.IsMatch(root.podaci))
            {
                if (root.pageNumber == 0)
                    return _context.esp_SearchMovieByExactRating(number).ToList();
                return _context.esp_SearchMovieByExactRatingSkip10Rows(number, root.pageNumber * 10).ToList();
            }
            //Search for at least rating >=
            else if (regexAtLeastRate.IsMatch(root.podaci))
            {
                number = Convert.ToDouble(Regex.Match(root.podaci, @"\d+(?:\.[0-9])?").Value);
                if (root.pageNumber == 0)
                    return _context.esp_SearchMovieBetweenMinAndExact(number).ToList();
                return _context.esp_SearchMovieBetweenMinAndExactSkip10Rows(number, root.pageNumber * 10).ToList();
            }
            //Search after provided year
            else if (laterDate.IsMatch(root.podaci))
            {
                if (root.pageNumber == 0)
                    return _context.esp_SearchMovieByLaterDate(Convert.ToInt32(Regex.Match(root.podaci, @"\d{4}").Value)).ToList();
                return _context.esp_SearchMovieByLaterDateSkip10Rows(Convert.ToInt32(Regex.Match(root.podaci, @"\d{4}").Value), root.pageNumber * 10).ToList();
            }
            //Search before provided year
            else if (earlierDate.IsMatch(root.podaci))
            {
                if (root.pageNumber == 0)
                    return _context.esp_SearchMovieByEarlierDate(Convert.ToInt32(Regex.Match(root.podaci, @"\d{4}").Value)).ToList();
                return _context.esp_SearchMovieByEarlierDateSkip10Rows(Convert.ToInt32(Regex.Match(root.podaci, @"\d{4}").Value), root.pageNumber * 10).ToList();
            }
            if (root.pageNumber == 0)
                return _context.esp_SearchMovie(root.podaci).ToList();
            return _context.esp_SearchMovieSkip10Rows(root.podaci, root.pageNumber * 10).ToList();
        }
        [HttpPost]
        [Route("api/Movies/RateMovie")]
        public IHttpActionResult RateMovie([FromBody]Root root)
        {
            _context.esp_AddRatingToMovie(root.Ocjena, root.FilmId);
            return Ok(root.Ocjena);
        }
    }
}
