//using LostTimeWeb.DAL;
using LostTimeDB;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LostTimeWeb.WebApp.Authentication
{
    public interface IExternalAuthenticationManager
    {
        void CreateOrUpdateUser( OAuthCreatingTicketContext context );

        UserAccount FindUser( OAuthCreatingTicketContext context );
    }
}