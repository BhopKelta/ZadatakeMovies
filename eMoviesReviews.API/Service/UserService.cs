using eMoviesReviews.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesReviews.API.Service
{
    public class UserService
    {
        eMovies2019Entities _db = new eMovies2019Entities();
        public Korisnici GetKorisnikByLogin(string username, string password)
        {
            Korisnici korisnik = _db.Korisnici.FirstOrDefault(x => x.username == username && x.password == password);

            return korisnik;
        }
    }
}