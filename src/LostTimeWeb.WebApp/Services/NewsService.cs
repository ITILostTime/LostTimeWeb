using System;
using System.Linq;
using System.Collections.Generic;
using LostTimeDB;
using LostTimeWeb.WebApp.Models.NewsViewModels;

namespace LostTimeWeb.WebApp.Services
{
    public class NewsService
    {
        readonly  NewsGateway _newsGateway;

        public NewsService(NewsGateway newsGateway)
        {
            _newsGateway = newsGateway;
        }

        public Result<IEnumerable<News>> GetAllNews()
        {
            IEnumerable<News> allNews = _newsGateway.GetAll();
            eturn Result.Success( Status.Ok, allNews );
        }
        public Result<News> Create(string title, string content,DateTime time, int authorId)
        {
            News news = new News();
            if (!IsNameValid(title)) return Result.Failure<News>(Status.BadRequest, "The title is not valid.");
            if (!IsNameValid(content)) return Result.Failure<News>(Status.BadRequest, "The content is not valid.");
            if(if( _newsGateway.FindByTitle( title) != null ) ) return Result.Failure<News>( Status.BadRequest, " this Article already exists.");
            _newsGateway.CreateNews( title, content, time, authorId);
            News news = _newsGateway.FindByTitle( title ); 
            return Result.Success( Status.Created, news );
        }

        public Result<News> Update(int Id, string title,  string content, DateTime time, int  authorId)
        {
            if( !IsNameValid( title ) ) return Result.Failure<News>( Status.BadRequest, "The title is not valid." );
            if( !IsNameValid( content ) ) return Result.Failure<News>( Status.BadRequest, "The content is not valid." );
            
            News news = new News();
            if( ( news = _newsGateway.FindByID( Id ) ) == null )
            {
                return Result.Failure<News>( Status.NotFound, "News not found." );
            }
            
            {
                News s = _newsGateway.FindByTitle( title);
                if( s != null && s.NewsID != news.NewsID ) return Result.Failure<Student>( Status.BadRequest, "this Article already exists." );
            }
            _newsGateway.UpdateArticle( Id, title, content, time, authorId );
            news = _newsGateway.FindByID( Id );
            return Result.Success( Status.Ok, news );
        }

        public Result<int> Delete( int Id )
        {
            News news = new News();
            if( (  = _newsGateway.FindByID( Id ) ) == null ) return Result.Failure<int>( Status.NotFound, "News not found." );
            _newsGateway.Delete( Id );
            return Result.Success( Status.Ok,  Id);
        }

        public Result<News> GetById( int id )
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( id ) ) == null ) return Result.Failure<News>( Status.NotFound, "News not found." );
            return Result.Success( Status.Ok, news );
        }

        public  BadNewsVote(int id)
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( id ) ) == null ) return Result.Failure<News>( Status.NotFound, "News not found." );
            _newsGateway.UpdateNewsBadPopularity(id);
            news = _newsGateway.FindByID( Id );
            return Result.Success( Status.Ok, news );
        }

        public void GoodNewsVote(int id)
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( id ) ) == null ) return Result.Failure<News>( Status.NotFound, "News not found." );
            _newsServices.UpdateNewsGoodPopularity( id );
            news = _newsGateway.FindByID( Id );
            return Result.Success( Status.Ok, news );
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );

    }
}
