using System;
using System.Collections.Generic;
using System.Text;

namespace eMoviesReviews.Core.Models
{
    public class Glumci
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime datumRodjenja { get; set; }
        public int GodineRada { get; set; }
    }
}
