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
            if( _userGateway.FindByUserEmail( email ) != null ) return false;
            _userGateway.CreateNewUserAccount("", email, _passwordHasher.HashPassword( password ),DateTime.Now );
            return true;
        }

        
        /*
        public bool CreateOrUpdateGoogleUser( string email, string googleId, string refreshToken )
        {
            if( _userGateway.FindByGoogleID( googleId ) != null )
            {
                _userGateway.UpdateGoogleToken( googleId, refreshToken );
                return false;
            }
            UserAccount user = _userGateway.FindByUserEmail( email );
            if( user != null )
            {
                _userGateway.AddGoogleToken( user.UserId, googleId, refreshToken );
                return false;
            }
            _userGateway.CreateGoogleUser( email, googleId, refreshToken );
            return true;
        }
        */
        public UserAccount FindUser( string email, string password )
        {
            UserAccount user = _userGateway.FindByUserEmail( email );
            if( user != null && _passwordHasher.VerifyHashedPassword( user.UserPassword, password ) == PasswordVerificationResult.Success )
            {
                return user;
            }

            return null;
        }

        public UserAccount FindUser( string email )
        {
            return _userGateway.FindByUserEmail( email );
        }
        /*
        public UserAccount FindGoogleUser( string googleId )
        {
            return _userGateway.FindByGoogleId( googleId );
        }
        

        public IEnumerable<string> GetAuthenticationProviders( string userId )
        {
            return _userGateway.GetAuthenticationProviders( userId );
        }
        */
    }
}
