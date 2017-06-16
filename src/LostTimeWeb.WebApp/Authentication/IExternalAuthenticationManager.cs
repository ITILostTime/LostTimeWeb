using LostTimeWeb.DAL;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LostTimeWeb.WebApp.Authentication
{
    public interface IExternalAuthenticationManager
    {
        void CreateOrUpdateUser( OAuthCreatingTicketContext context );

        User FindUser( OAuthCreatingTicketContext context );
    }
}