using LostTimeDB;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LostTimeWeb.WebApp.Authentication
{
    /*
    public class GoogleExternalAuthenticationManager : IExternalAuthenticationManager
    {
        readonly UserService _userService;

        public GoogleExternalAuthenticationManager( UserService userService )
        {
            _userService = userService;
        }

        public void CreateOrUpdateUser( OAuthCreatingTicketContext context )
        {
            if( context.RefreshToken != null )
            {
                _userService.CreateOrUpdateGoogleUser( context.GetEmail(), context.GetGoogleId(), context.RefreshToken );
            }
        }

        public UserAccount FindUser( OAuthCreatingTicketContext context )
        {
            return _userService.FindGoogleUser( context.GetGoogleId() );
        }
    }
    */
}
