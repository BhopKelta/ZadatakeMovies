using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesReviews.API.JsonModel
{
    public class SearchRoot
    {
        public string podaci { get; set; }
        public int pageNumber { get; set; }
    }
}