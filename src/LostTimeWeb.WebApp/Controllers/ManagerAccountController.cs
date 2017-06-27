using System.Collections.Generic;
using System.Linq;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.ManagerAccountViewModel;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class ManageAccountController : Controller
    {
        readonly ManagerAccountService _managerAccountServices;
    
        public ManageAccountController(ManagerAccountService managerAccountServices)
        {
            _managerAccountServices = managerAccountServices;
        }

        [HttpGet]
        public IActionResult Display(int id)
        {
            Result<UserAccount> result  = _managerAccountServices.Display(id);
            return this.CreateResult<UserAccount, UserViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToUserViewModel();
            } );
        }

        [HttpPut( "{id}" )]
        [Authorize(Policy = "Permission")]
        public IActionResult Edit( [FromBody] EditViewModel model )
        {
            Result<UserAccount> result = _managerAccountServices.Edit(model.UserID, model.UserPseudonym,model.UserEmail, model.UserOldPassword, model.UserNewPassword);
            return this.CreateResult<UserAccount, UserViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToUserViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        [Authorize(Policy = "Permission")]
        public IActionResult Delete( int id )
        {
            Result<int> result =  _managerAccountServices.Delete( id );
            return this.CreateResult( result );
        }
    }
}