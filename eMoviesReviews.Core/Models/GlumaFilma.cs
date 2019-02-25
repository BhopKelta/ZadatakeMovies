using System;
using System.Collections.Generic;
using System.Text;

namespace eMoviesReviews.Core.Models
{
    public class GlumaFilma
    {
        public int Id { get; set; }
        public virtual Glumci Glumac { get; set; }
        public int GlumacId { get; set; }
        public virtual Filmovi Film { get; set; }
        public int FilmId { get; set; }
    }
}
