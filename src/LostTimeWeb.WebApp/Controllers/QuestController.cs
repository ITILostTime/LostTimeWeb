using System;
using System.Collections.Generic;
using System.Linq;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
//using LostTimeWeb.WebApp.Models.NewsViewModels;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class QuestController : Controller
    {
        readonly NewsService _questService;

        public NewsController(NewsService questService)
        {
            _questService = questService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            Result<IEnumerable<Quest>> result = _questService.GetAllQuest();
            return this.CreateResult<IEnumerable< Quest>, IEnumerable<QuestViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToQuestViewModel() );
            } );
        }

        [HttpGet("{id}", Name = "GetQuest")]
        public IActionResult GetById(int id)
        {
            Result<Quest> result  = _questServices.GetById(id);
            return this.CreateResult<Quest, QuestViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToQuestViewModel();
            } );
        }

        [HttpGet("{id}", Name = "GetQuestByAuthor")]
        [AllowAnonymous]
        public IActionResult GetAllByAuthor(int id)
        {
            Result<IEnumerable<Quest>> result = _questService.GetAllQuestByAuthor(id);
            return this.CreateResult<IEnumerable< Quest>, IEnumerable<QuestViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToQuestViewModel() );
            } );
        }

        [HttpPost]
        //[Authorize(Policy = "Permission")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create( [FromBody] QuestViewModel model )
        {
            Result<Quest> result  = _questServices.Create( model.QuestTitle, model.QuestData,model.QuestAuthorID);
            return this.CreateResult<Quest , QuestViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToQuestViewModel();
                o.RouteName = "GetQuest";
                o.RouteValues = s => new { id = s.QuestID };
            } );
        }

        [HttpPut( "{id}" )]
        //[Authorize(Policy = "Permission")]
        [ValidateAntiForgeryToken]
        public IActionResult Update( [FromBody] QuestViewModel model )
        {
            Result<Quest> result = _questServices.Update(model.QuestID, model.QuestTitle, model.QuestData,model.QuestAuthorID);
            return this.CreateResult<Quest, QuestViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToQuestViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        [Authorize(Policy = "Permission")]
        public IActionResult Delete( int id )
        {
            Result<int> result =  _questServices.Delete( id );
            return this.CreateResult( result );
        }
    }
}