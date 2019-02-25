using System;
using System.Collections.Generic;
using System.Text;

namespace eMoviesReviews.Core.Models
{
    public class RecenzijeFilmova
    {
        public int Id { get; set; }
        public virtual Korisnici Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public virtual Filmovi Film { get; set; }
        public int FilmId { get; set; }
        public int Ocjena { get; set; }
    }
}
