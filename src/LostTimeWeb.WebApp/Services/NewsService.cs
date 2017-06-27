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
        readonly UserAccountGateway _userAccountGateway;

        public NewsService(NewsGateway newsGateway, UserAccountGateway userAccountGateway)
        {
            _newsGateway = newsGateway;
            _userAccountGateway = userAccountGateway;
        }

        public Result<IEnumerable<News>> GetAllNews()
        {
            IEnumerable<News> allNews = _newsGateway.GetAll();
            return Result.Success( Status.Ok, allNews );
        }
        public Result<News> Create(string title, string content,DateTime time, int authorId)
        {
            News news = new News();
            if (!IsNameValid(title)) return Result.Failure<News>(Status.BadRequest, "The title is not valid.");
            if (!IsNameValid(content)) return Result.Failure<News>(Status.BadRequest, "The content is not valid.");
            if( ( _newsGateway.FindByTitle( title ) != null ) ) return Result.Failure<News>( Status.BadRequest, " this Article already exists.");
            _newsGateway.CreateNews( title, content, time, authorId);
            news = _newsGateway.FindByTitle( title );

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
                if( s != null && s.NewsID != news.NewsID ) return Result.Failure<News>( Status.BadRequest, "this Article already exists." );
            }
            _newsGateway.UpdateArticle( Id, title, content, time, authorId );
            news = _newsGateway.FindByID( Id );
            return Result.Success( Status.Ok, news );
        }

        public Result<int> Delete( int Id )
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( Id ) ) == null ) return Result.Failure<int>( Status.NotFound, "News not found." );
            _newsGateway.DeleteNews( Id );
            return Result.Success( Status.Ok,  Id);
        }

        public Result<News> GetById( int id )
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( id ) ) == null ) return Result.Failure<News>( Status.NotFound, "News not found." );
            return Result.Success( Status.Ok, news );
        }

        public Result<News> BadNewsVote(int id)
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( id ) ) == null ) return Result.Failure<News>( Status.NotFound, "News not found." );
            _newsGateway.UpdateNewsBadPopularity(id);
            news = _newsGateway.FindByID( id );
            return Result.Success( Status.Ok, news );
        }

        public Result<News> GoodNewsVote(int id)
        {
            News news = new News();
            if( ( news = _newsGateway.FindByID( id ) ) == null ) return Result.Failure<News>( Status.NotFound, "News not found." );
            _newsGateway.UpdateNewsGoodPopularity( id );
            news = _newsGateway.FindByID( id );
            return Result.Success( Status.Ok, news );
        }

/*        public string AddAuthorName(int id)
        {
            News news = _newsGateway.FindByID( id );
            UserAccount user = _userAccountGateway.FindByID( news.AuthorID );
            return user.UserPseudonym;
        }*/

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );

    }
}
