using System;
using System.Collections.Generic;
using System.Linq;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.QuestManagerViewModel;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class QuestPatternController
    {
        /*readonly QuestPatternService _questPatternService;

        public QuestPatternController(QuestPatternService questPatternService)
        {
            _questPatternService = questPatternService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            Result<IEnumerable<QuestPattern>> result = _questPatternService.GetAllQuestPattern();
            return this.CreateResult<IEnumerable< QuestPattern>, IEnumerable<QuestPatternViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToQuestPatternViewModel() );
            } );
        }

        [HttpGet("{id}", Name = "GetQuestPattern")]
        public IActionResult GetById(int id)
        {
            Result<QuestPattern> result  = _questPatternServices.GetById(id);
            return this.CreateResult<QuestPattern, QuestPatternViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToQuestPatternViewModel();
            } );
        }

        [HttpGet("{id}", Name = "GetQuestPatternByAuthor")]
        [AllowAnonymous]
        public IActionResult GetAllByAuthor(int id)
        {
            Result<IEnumerable<QuestPattern>> result = _questPatternService.GetAllQuestPatternByAuthor(id);
            return this.CreateResult<IEnumerable< QuestPattern>, IEnumerable<QuestPatternViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToQuestPatternViewModel() );
            } );
        }

        [HttpPost]
        //[Authorize(Policy = "Permission")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create( [FromBody] QuestPatternViewModel model )
        {
            Result<QuestPattern> result  = _questPatternServices.Create( model.QuestPatternTitle, model.QuestPatternData);
            return this.CreateResult<QuestPattern , QuestPatternViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToQuestPatternViewModel();
                o.RouteName = "GetQuestPattern";
                o.RouteValues = s => new { id = s.QuestPatternID };
            } );
        }

        [HttpPut( "{id}" )]
        //[Authorize(Policy = "Permission")]
        [ValidateAntiForgeryToken]
        public IActionResult Update( [FromBody] QuestPatternViewModel model )
        {
            Result<QuestPattern> result = _questPatternServices.Update(model.QuestPatternID, model.QuestPatternTitle, model.QuestPatternData);
            return this.CreateResult<QuestPattern, QuestPatternViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToQuestPatternViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        [Authorize(Policy = "Permission")]
        public IActionResult Delete( int id )
        {
            Result<int> result =  _questPatternServices.Delete( id );
            return this.CreateResult( result );
        }*/

    }
}