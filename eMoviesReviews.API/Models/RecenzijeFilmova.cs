//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eMoviesReviews.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecenzijeFilmova
    {
        public int Id { get; set; }
        public int KorisnikId { get; set; }
        public int FilmId { get; set; }
        public int Ocjena { get; set; }
    
        public virtual Filmovi Filmovi { get; set; }
        public virtual Korisnici Korisnici { get; set; }
    }
}
