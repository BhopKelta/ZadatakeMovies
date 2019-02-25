using System;
using System.Collections.Generic;
using System.Text;

namespace eMoviesReviews.Core.Models
{
    public class Korisnici
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime datumRodjenja { get; set; }
        public string Email { get; set; }
    }
}
