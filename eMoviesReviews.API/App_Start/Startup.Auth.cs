using eMoviesReviews.API.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesReviews.API
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new OAuthAppProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(10),
                AllowInsecureHttp = true //Don't do this in production ONLY FOR DEVELOPING: ALLOW INSECURE HTTP!  
            };
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            app.UseOAuthAuthorizationServer(OAuthOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}