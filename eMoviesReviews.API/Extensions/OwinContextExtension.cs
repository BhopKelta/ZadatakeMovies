using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesReviews.API.Extensions
{
    public static class OwinContextExtension
    {
        public static string GetUserId(this IOwinContext context)
        {
            var result = "-1";
            var claim = context.Authentication.User.Claims.FirstOrDefault(x => x.Type == "UserId");

            if (claim != null)
            {
                result = claim.Value;
            }
            return result;
        }
    }
}