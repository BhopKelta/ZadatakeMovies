using eMoviesReviews.API.Models;
using eMoviesReviews.API.Service;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;

namespace eMoviesReviews.API.Provider
{
    public class OAuthAppProvider: OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;

                var userService = new UserService();
                Korisnici user = userService.GetKorisnikByLogin(username, password);

                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.Ime),
                        new Claim("UserId",user.Id.ToString())
                    };
                    ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("invalid_grant", "error");
                }
            });
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.  
            if (context.ClientId == null)
            {
                // Validate Authoorization.  
                context.Validated();
            }

            // Return info.  
            return Task.FromResult<object>(null);
        }
    }
}