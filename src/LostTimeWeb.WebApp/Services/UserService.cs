using System;
using System.Collections.Generic;
using LostTimeDB;

namespace LostTimeWeb.WebApp.Services
{
    public class UserService
    {
        readonly UserAccountGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public UserService(UserAccountGateway userGateway, PasswordHasher passwordHasher )
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public bool CreatePasswordUser( string email, string password )
        {
            if( _userGateway.FindByEmail( email ) != null ) return false;
            _userGateway.CreatePasswordUser( email, _passwordHasher.HashPassword( password ) );
            return true;
        }

        

        public bool CreateOrUpdateGoogleUser( string email, string googleId, string refreshToken )
        {
            if( _userGateway.FindByGoogleId( googleId ) != null )
            {
                _userGateway.UpdateGoogleToken( googleId, refreshToken );
                return false;
            }
            User user = _userGateway.FindByEmail( email );
            if( user != null )
            {
                _userGateway.AddGoogleToken( user.UserId, googleId, refreshToken );
                return false;
            }
            _userGateway.CreateGoogleUser( email, googleId, refreshToken );
            return true;
        }

        public UserAccount FindUser( string email, string password )
        {
            UserAccount user = _userGateway.FindByEmail( email );
            if( user != null && _passwordHasher.VerifyHashedPassword( user.Password, password ) == PasswordVerificationResult.Success )
            {
                return user;
            }

            return null;
        }

        public UserAccount FindUser( string email )
        {
            return _userGateway.FindByEmail( email );
        }

        public UserAccount FindGoogleUser( string googleId )
        {
            return _userGateway.FindByGoogleId( googleId );
        }

        public UserAccount FindGithubUser( int githubId )
        {
            return _userGateway.FindByGithubId( githubId );
        }

        public IEnumerable<string> GetAuthenticationProviders( string userId )
        {
            return _userGateway.GetAuthenticationProviders( userId );
        }
    }
}
