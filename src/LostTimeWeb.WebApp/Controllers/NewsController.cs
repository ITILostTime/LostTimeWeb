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
    //[Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class NewsController : Controller
    {
        readonly NewsService _newsServices;

        public NewsController(NewsService newsServices)
        {
            _newsServices = newsServices;
        }

        [HttpGet]
        public IActionResult GetNewsList()
        {
            Console.WriteLine("NEWSLIST CALLED");
            Result<IEnumerable<Article>> result = _newsServices.GetAll();
            return this.CreateResult<IEnumerable<Article>, IEnumerable<ArticleViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }
    }
}