using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
//using LostTimeWeb.DAL;
using LostTimeDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LostTimeWeb.WebApp.Authentication
{
    public class ExternalAuthenticationEvents
    {
        readonly IExternalAuthenticationManager _authenticationManager;

        public ExternalAuthenticationEvents( IExternalAuthenticationManager authenticationManager )
        {
            _authenticationManager = authenticationManager;
        }

        public Task OnCreatingTicket( OAuthCreatingTicketContext context )
        {
            _authenticationManager.CreateOrUpdateUser( context );
            UserAccount user = _authenticationManager.FindUser( context );
            ClaimsPrincipal principal = CreatePrincipal( user );
            context.Ticket = new AuthenticationTicket( principal, context.Ticket.Properties, CookieAuthentication.AuthenticationScheme );
            return Task.CompletedTask;
        }

        ClaimsPrincipal CreatePrincipal( UserAccount user )
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier, user.UserID.ToString(), ClaimValueTypes.String ),
                new Claim( ClaimTypes.Email, user.UserEmail )
            };
            ClaimsPrincipal principal = new ClaimsPrincipal( new ClaimsIdentity( claims, CookieAuthentication.AuthenticationType, ClaimTypes.Email, string.Empty ) );
            return principal;
        }
    }
}
