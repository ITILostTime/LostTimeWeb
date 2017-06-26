using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetAll()
        {
            //Console.WriteLine("NEWSLIST CALLED");
            Result<IEnumerable<News>> result = _newsServices.GetAllNews();
            return this.CreateResult<IEnumerable<News>, IEnumerable<ArticleViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Result<News> result  = _newsService.FindByID(id);
            return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create( [FromBody] ArticleViewModel model )
        {
            Result<News> result  = _newsServices.Create( model.Title, model.Content, DateTime.Now ,model.AuthorId);
        //    Result<News> result  = _newsGateway.CreateNews( model.Title, model.Content, DateTime.Now ,model.AuthorId);
            return this.CreateResult<News , ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
                o.RouteName = "GetArticle";
                o.RouteValues = s => new { id = s.NewsID };
            } );
        }

        [HttpPut( "{id}" )]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update( [FromBody] ArticleViewModel model )
        {
            Result<News> result = _newsServices.Update(model.ArticleId, model.Title, model.Content, DateTime.Now, model.AuthorId);
            //Result<News> result = _newsGateway.UpdateArticle(model.rticleId, model.Title, model.Content, DateTime.Now,model.AuthorId);
            return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToArticleViewModel();
            } );
        }

        [HttpPut( "{id}" )]
        public void UpdateNewsBadPopularity(int id)
        {
           Result<News> result = _newsServices.BadNewsVote(id);
           return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }
        
        [HttpPut( "{id}" )]
        public void UpdateNewsGoodPopularity(int id)
        {
            Result<News> result = _newsServices.GoodNewsVote(id);
            return this.CreateResult<News, ArticleViewModel>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToArticleViewModel() );
            } );
        }

        [HttpDelete( "{id}" )]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete( int id )
        {
            Result<int> result =  _newsServices.Delete( id );
            return this.CreateResult( result );
        }

        /*
        [HttpGet( "{id}", Name = "GetStudent" )]
        public IActionResult GetStudentById( int id )
        {
            Result<Student> result = _studentService.GetById( id );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
            } );
        }

        [HttpPost]
        public IActionResult CreateStudent( [FromBody] StudentViewModel model )
        {
            Result<Student> result = _studentService.CreateStudent( model.FirstName, model.LastName, model.BirthDate, model.Photo, model.GitHubLogin );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
                o.RouteName = "GetStudent";
                o.RouteValues = s => new { id = s.StudentId };
            } );
        }

        [HttpPut( "{id}" )]
        public IActionResult UpdateStudent( int id, [FromBody] StudentViewModel model )
        {
            Result<Student> result = _studentService.UpdateStudent( id, model.FirstName, model.LastName, model.BirthDate, model.Photo, model.GitHubLogin );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        public IActionResult DeleteStudent( int id )
        {
            Result<int> result = _studentService.Delete( id );
            return this.CreateResult( result );
        }
        */
    }
}