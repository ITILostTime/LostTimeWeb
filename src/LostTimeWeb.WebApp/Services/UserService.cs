using System;
using System.Collections.Generic;
//using LostTimeWeb.DAL;
using LostTimeDB;

namespace LostTimeWeb.WebApp.Services
{
    public class UserService
    {
        readonly UserAccountGateaway _userAccountGateway;
        readonly PasswordHasher _passwordHasher;
        public UserService( UserAccountGateaway userAccountGateway, PasswordHasher passwordHasher )
        {
            _userAccountGateway = userAccountGateway;
            _passwordHasher = passwordHasher;
        }

        public bool CreatePasswordUser( string email, string password )
        {
            if( _userAccountGateway.FindByEmail( email ) != null ) return false;
            _userAccountGateway.CreatePasswordUser( email, _passwordHasher.HashPassword( password ) );
            return true;
        }

        public bool CreateOrUpdateGithubUser( string email, int githubId, string accessToken )
        {
            if( _userAccountGateway.FindByGithubId( githubId ) != null )
            {
                _userAccountGateway.UpdateGithubToken( githubId, accessToken );
                return false;
            }
            UserAccount user = _userAccountGateway.FindByEmail( email );
            if( user != null )
            {
                _userAccountGateway.AddGithubToken( user.UserId, githubId, accessToken );
                return false;
            }
            _userAccountGateway.CreateGithubUser( email, githubId, accessToken );
            return true;
        }

        public bool CreateOrUpdateGoogleUser( string email, string googleId, string refreshToken )
        {
            if( _userAccountGateway.FindByGoogleID( googleId ) != null )
            {
                _userAccountGateway.UpdateGoogleToken( googleId, refreshToken );
                return false;
            }
            UserAccount user = _userAccountGateway.FindByEmail( email );
            if( user != null )
            {
                _userAccountGateway.AddGoogleToken( user.UserId, googleId, refreshToken );
                return false;
            }
            _userAccountGateway.CreateGoogleUser( email, googleId, refreshToken );
            return true;
        }

        public UserAccount FindUser( string email, string password )
        {
            UserAccount user = _userAccountGateway.FindByEmail( email );
            if( user != null && _passwordHasher.VerifyHashedPassword( user.Password, password ) == PasswordVerificationResult.Success )
            {
                return user;
            }

            return null;
        }

        public UserAccount FindUser( string email )
        {
            return _userAccountGateway.FindByEmail( email );
        }

        public UserAccount FindGoogleUser( string googleId )
        {
            return _userAccountGateway.FindByGoogleId( googleId );
        }

        /*public User FindGithubUser( int githubId )
        {
            return _userAccountGateway.FindByGithubId( githubId );
        }*/

        public IEnumerable<string> GetAuthenticationProviders( string userId )
        {
            return _userAccountGateway.GetAuthenticationProviders( userId );
        }
    }
}
