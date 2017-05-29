using LostTimeWeb.DAL;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace LostTimeWeb.WebApp.Authentication
{
    public class GithubExternalAuthenticationManager : IExternalAuthenticationManager
    {
        readonly UserService _userService;

        public GithubExternalAuthenticationManager( UserService userService )
        {
            _userService = userService;
        }

        public void CreateOrUpdateUser( OAuthCreatingTicketContext context )
        {
            _userService.CreateOrUpdateGithubUser( context.GetEmail(), context.GetGithubId(), context.AccessToken );
        }

        public User FindUser( OAuthCreatingTicketContext context )
        {
            return _userService.FindGithubUser( context.GetGithubId() );
        }
    }
}
