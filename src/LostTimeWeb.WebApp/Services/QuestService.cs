using System;
using System.Linq;
using System.Collections.Generic;
using LostTimeDB;
//using LostTimeWeb.WebApp.Models.NewsViewModels;

namespace LostTimeWeb.WebApp.Services
{
    public class QuestService
    {
        readonly  QuestGateway _questGateway;

        public QuestService(QuestGateway questGateway)
        {
            _questGateway = questGateway;
        }

        public Result<IEnumerable<Quest>> GetAllNews()
        {
            IEnumerable<Quest> allNews = _questGateway.GetAll();
            return Result.Success( Status.Ok, allNews );
        }

        public Result<Quest> GetById( int id )
        {
            Quest quest = new Quest();
            if( ( quest = _questGateway.FindByID( id ) ) == null ) return Result.Failure<Quest>( Status.NotFound, "Quest not found." );
            return Result.Success( Status.Ok, quest );
        }

        public Result<IEnumerable<Quest>> GetAllQuestByAuthor(int id)
        {
            IEnumerable<Quest> allNews = _questGateway.GetAllByAuthorID(id);
            return Result.Success( Status.Ok, allNews );
        }

        public Result<Quest> Create(string title, string data, int authorId)
        {
            Quest quest = new Quest();
            if (!IsNameValid(title)) return Result.Failure<Quest>(Status.BadRequest, "The Title is not valid.");
            if (!IsNameValid(data)) return Result.Failure<Quest>(Status.BadRequest, "The Data is not valid.");
            if( ( _questGateway.FindByTitle( title ) != null ) ) return Result.FailureQuest>( Status.BadRequest, " this Quest already exists.");

            _questGateway.CreateQuest( title, data, DateTime.Now, authorId);
            quest = _newsGateway.FindByTitle( title );

            return Result.Success( Status.Created, quest );
        }

        public Result<News> Update(int Id, string title,  string data, int  authorId)
        {
            if( !IsNameValid( title ) ) return Result.Failure<Quest>( Status.BadRequest, "The title is not valid." );
            if( !IsNameValid( content ) ) return Result.Failure<Quest>( Status.BadRequest, "The content is not valid." );
            
            Quest quest = new Quest();
            if( ( quest = _questGateway.FindByID( Id ) ) == null )
            {
                return Result.Failure<News>( Status.NotFound, "Quest not found." );
            }
            
            {
                News s = _questGateway.FindByTitle( title);
                if( s != null && s.QuestID != quest.QuestID ) return Result.Failure<Quest>( Status.BadRequest, "this Article already exists." );
            }
            _questGateway.UpdateQuest( Id, title, data, DateTime.Now, authorId );
            quest = _questGateway.FindByID( Id );
            return Result.Success( Status.Ok, quest );
        }

        public Result<int> Delete( int Id )
        {
            Quest quest = new Quest();
            if( ( quest = _questGateway.FindByID( Id ) ) == null ) return Result.Failure<int>( Status.NotFound, "Quest not found." );
            _questGateway.DeleteNews( Id );
            return Result.Success( Status.Ok,  Id);
        }
    }
}