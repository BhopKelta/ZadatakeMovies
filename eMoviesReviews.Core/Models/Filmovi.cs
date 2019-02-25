using System;
using System.Collections.Generic;
using System.Text;

namespace eMoviesReviews.Core.Models
{
    public class Filmovi
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public byte[] Slika { get; set; }
        public string Opis { get; set; }
        public DateTime DatumRealizacije { get; set; }
    }
}
