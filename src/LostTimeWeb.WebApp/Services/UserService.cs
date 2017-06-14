using System;
using System.Collections.Generic;
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
                _userAccountGateway.AddGoogleToken( user.UserID, googleId, refreshToken );
                return false;
            }
            _userAccountGateway.CreateGoogleUser( email, googleId, refreshToken );
            return true;
        }

        public UserAccount FindUser( string email, string password )
        {
            UserAccount user = _userAccountGateway.FindByEmail( email );
            if( user != null && _passwordHasher.VerifyHashedPassword( user.UserPassword, password ) == PasswordVerificationResult.Success )
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
            return _userAccountGateway.FindByGoogleID( googleId );
        }

        public IEnumerable<string> GetAuthenticationProviders( string userId )
        {
            return _userAccountGateway.GetAuthenticationProviders( userId );
        }
    }
}
