﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.AccountViewModels;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Mvc.Client.Extensions;
using LostTimeWeb.WebApp.Poco;

namespace LostTimeWeb.WebApp.Controllers
{
    public class AccountController : Controller
    {
        readonly UserService _userService;
        readonly TokenService _tokenService;
        readonly Random _random;

        public AccountController( UserService userService, TokenService tokenService )
        {
            _userService = userService;
            _tokenService = tokenService;
            _random = new Random();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login( LoginViewModel model )
        {
            if( ModelState.IsValid )
            {
                UserAccount user = _userService.FindUser( model.Email, model.Password );
                //ModelPoco poco = new ModelPoco();
                //UserAccount user = poco.checkModel(model.Email, model.Password);
                if( user == null )
                {
                    ModelState.AddModelError( string.Empty, "Invalid login attempt." );
                    return View( model );
                }
                await SignIn( user.UserEmail, user.UserID.ToString(), user.UserPermission );
                return RedirectToAction( nameof( Authenticated ) );
            }
            return View( model );
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( RegisterViewModel model )
        {
            if( ModelState.IsValid )
            {
                if( !_userService.CreatePasswordUser( model.Email, model.Password ) )
                {
                    ModelState.AddModelError( string.Empty, "An account with this email already exists." );
                    return View( model );
                }
                UserAccount user = _userService.FindUser( model.Email );
                await SignIn( user.UserEmail, user.UserID.ToString(), user.UserPermission );
                return RedirectToAction( nameof( Authenticated ) );
            }

            return View( model );
        }

        [HttpGet]
        [Authorize( ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme )]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync( CookieAuthentication.AuthenticationScheme );
            ViewData[ "NoLayout" ] = true;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ExternalLogin( [FromQuery] string provider )
        {
            // Note: the "provider" parameter corresponds to the external
            // authentication provider choosen by the user agent.
            if( string.IsNullOrWhiteSpace( provider ) )
            {
                return BadRequest();
            }

            if( !HttpContext.IsProviderSupported( provider ) )
            {
                return BadRequest();
            }

            // Instruct the middleware corresponding to the requested external identity
            // provider to redirect the user agent to its own authorization endpoint.
            // Note: the authenticationScheme parameter must match the value configured in Startup.cs
            string redirectUri = Url.Action( nameof( ExternalLoginCallback ), "Account" );
            return Challenge( new AuthenticationProperties { RedirectUri = redirectUri }, provider );
        }

        [HttpGet]
        [Authorize( ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme )]
        public IActionResult ExternalLoginCallback()
        {
            return RedirectToAction( nameof( Authenticated ) );
        }

        [HttpGet]
        [Authorize( ActiveAuthenticationSchemes = CookieAuthentication.AuthenticationScheme )]
        public IActionResult Authenticated()
        {
            string userId = User.FindFirst( ClaimTypes.NameIdentifier ).Value;
            string email = User.FindFirst( ClaimTypes.Email ).Value;

            string role = User.FindFirst(ClaimTypes.Role ).Value;
            Token token = _tokenService.GenerateToken( userId, email , role);

            //IEnumerable<string> providers = _userService.GetAuthenticationProviders( userId );
            string providers = "PrimarySchool";
            ViewData[ "BreachPadding" ] = GetBreachPadding(); // Mitigate BREACH attack. See http://www.breachattack.com/
            ViewData[ "Token" ] = token;
            ViewData[ "Email" ] = email;
            ViewData[ "Id" ] = userId;
            ViewData[ "Role" ] = role;
            ViewData[ "NoLayout" ] = true;
            ViewData["Providers"] = providers;
            Console.WriteLine(userId);
            return View();
        }
        async Task SignIn( string email, string userId, string permission )
        { 
            List<Claim> claims = new List<Claim>
            {
                new Claim( ClaimTypes.Email, email, ClaimValueTypes.String ),
                new Claim( ClaimTypes.NameIdentifier, userId.ToString(), ClaimValueTypes.String ),
                new Claim( ClaimTypes.Role, permission, ClaimValueTypes.String)
            };
            ClaimsIdentity identity = new ClaimsIdentity( claims, CookieAuthentication.AuthenticationType, ClaimTypes.Email, string.Empty );
            ClaimsPrincipal principal = new ClaimsPrincipal( identity );
            await HttpContext.Authentication.SignInAsync( CookieAuthentication.AuthenticationScheme, principal );
        }
        string GetBreachPadding()
        {
            byte[] data = new byte[ _random.Next( 64, 256 ) ];
            _random.NextBytes( data );
            return Convert.ToBase64String( data );
        }
    }
}
