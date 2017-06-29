using System.Collections.Generic;
using System.Linq;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.NewsViewModels;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class NewsController : Controller
    {     
        readonly NewsService _newsServices;

        public NewsController(NewsService newsServices)
        {
            _newsServices = newsServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            Result<IEnumerable<News>> result = _newsServices.GetAllNews();
            return this.CreateResult<IEnumerable<News>, IEnumerable<ArticleViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }

        [HttpGet("{id}", Name = "GetNews")]
        public IActionResult GetById(int id)
        {
            Result<News> result  = _newsServices.GetById(id);
            return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
            } );
        }

        [HttpPost]
        [Authorize(Policy = "Permission")]
        //[ValidateAntiForgeryToken]
        public IActionResult Create( [FromBody] ArticleCreateViewModel model )
        {
            Result<News> result  = _newsServices.Create( model.Title, model.Content, model.AuthorId);
            return this.CreateResult<News , ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
                o.RouteName = "GetNews";
                o.RouteValues = s => new { id = s.NewsID };
            } );
        }

        [HttpPut( "{id}" )]
        [Authorize(Policy = "Permission")]
        //[ValidateAntiForgeryToken]
        public IActionResult Update( [FromBody] ArticleViewModel model )
        {
            Result<News> result = _newsServices.Update(model.ArticleId, model.Title, model.Content, model.AuthorId);
            return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
            } );
        }
        [HttpPut("upvote/{id:int}")]
        public IActionResult UpdateNewsUpVote(int id)
        {
            Result<News> result =  _newsServices.GoodNewsVote(id);
            return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
            } );
        }
        [HttpPut("downvote/{id:int}")]
        public IActionResult UpdateNewsDownVote(int id)
        {
            Result<News> result = _newsServices.BadNewsVote(id);
            return this.CreateResult<News, ArticleViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
            });
        }
        [HttpDelete( "{id}" )]
        [Authorize(Policy = "Permission")]
        public IActionResult Delete( int id )
        {
            Result<int> result =  _newsServices.Delete( id );
            return this.CreateResult( result );
        }
    }
}