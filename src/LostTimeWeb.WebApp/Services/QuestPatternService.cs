using System;
using System.Linq;
using System.Collections.Generic;
using LostTimeDB;
using LostTimeWeb.WebApp.Models.QuestManagerViewModel;

namespace LostTimeWeb.WebApp.Services
{
    public class QuestPatternService
    {
        readonly  QuestPatternGateway _questPatternGateway;

        public QuestPatternService(QuestPatternGateway questPatternGateway)
        {
            _questPatternGateway = questPatternGateway;
        }

        public Result<IEnumerable<QuestPattern>> GetAllNews()
        {
            IEnumerable<QuestPattern> allNews = _questPatternGateway.GetAll();
            return Result.Success( Status.Ok, allNews );
        }

        public Result<QuestPattern> GetById( int id )
        {
            QuestPattern questPattern = new QuestPattern();
            if( ( questPattern = _questPatternGateway.FindByID( id ) ) == null ) return Result.Failure<QuestPattern>( Status.NotFound, "QuestPattern not found." );
            return Result.Success( Status.Ok, questPattern );
        }

        public Result<IEnumerable<QuestPattern>> GetAllQuestPatternByAuthor(int id)
        {
            IEnumerable<QuestPattern> allNews = _questPatternGateway.GetAllByAuthorID(id);
            return Result.Success( Status.Ok, allNews );
        }

        public Result<QuestPattern> Create(string title, string data, int authorId)
        {
            QuestPattern questPattern = new QuestPattern();
            if (!IsNameValid(title)) return Result.Failure<QuestPattern>(Status.BadRequest, "The Title is not valid.");
            if (!IsNameValid(data)) return Result.Failure<QuestPattern>(Status.BadRequest, "The Data is not valid.");
            if( ( _questPatternGateway.FindByTitle( title ) != null ) ) return Result.FailureQuest>( Status.BadRequest, " this QuestPattern already exists.");

            _questPatternGateway.CreateQuestPattern( title, data);
            questPattern = _newsGateway.FindByTitle( title );

            return Result.Success( Status.Created, questPattern );
        }

        public Result<News> Update(int Id, string title,  string data, int  authorId)
        {
            if( !IsNameValid( title ) ) return Result.Failure<QuestPattern>( Status.BadRequest, "The title is not valid." );
            if( !IsNameValid( content ) ) return Result.Failure<QuestPattern>( Status.BadRequest, "The content is not valid." );
            
            QuestPattern questPattern = new QuestPattern();
            if( ( questPattern = _questPatternGateway.FindByID( Id ) ) == null )
            {
                return Result.Failure<News>( Status.NotFound, "QuestPattern not found." );
            }
            
            {
                News s = _questPatternGateway.FindByTitle( title);
                if( s != null && s.QuestPatternID != questPattern.QuestPatternID ) return Result.Failure<QuestPattern>( Status.BadRequest, "this Article already exists." );
            }
            _questPatternGateway.UpdateQuestPattern( Id, title, data);
            questPattern = _questPatternGateway.FindByID( Id );
            return Result.Success( Status.Ok, questPattern );
        }

        public Result<int> Delete( int Id )
        {
            QuestPattern questPattern = new QuestPattern();
            if( ( questPattern = _questPatternGateway.FindByID( Id ) ) == null ) return Result.Failure<int>( Status.NotFound, "QuestPattern not found." );
            _questPatternGateway.DeleteNews( Id );
            return Result.Success( Status.Ok,  Id);
        }
    }
}