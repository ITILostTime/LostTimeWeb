using System;
using System.Linq;
using System.Collections.Generic;
using LostTimeWeb.DAL;
using LostTimeWeb.WebApp.Models.NewsViewModels;

namespace LostTimeWeb.WebApp.Services
{
    public class NewsService
    {
        readonly StudentGateway _studentGateway;
        public List<Article> _pocoArticles; 
        public NewsService(StudentGateway studentGateway)
        {
            _studentGateway = studentGateway; 
            _pocoArticles = SetPoco();
        }
        
        public List<Article> SetPoco()
        {
            List<Article> pocoArticles = new List<Article>();

            Article a1 = new Article();
            a1.ArticleId = 0;
            a1.Title = "Next gen of title";
            a1.Content = "Lorem Ipsum";
            a1.DateLastEdit = DateTime.Now;
            a1.DatePost = DateTime.Now;
            a1.AuthorId = 1;
            a1.Popularity = 0;
            a1.Editions = 0;
            pocoArticles.Add(a1);

            Article a2 = new Article();
            a2.ArticleId = 1;
            a2.Title = "Another title";
            a2.Content = "Lorem Ipsum  Again";
            a2.DateLastEdit = DateTime.Now;
            a2.DatePost = DateTime.Now;
            a2.AuthorId = 1;
            a2.Popularity = 10;
            a2.Editions = 0;
            pocoArticles.Add(a2);

            return pocoArticles;
        }
        /*
        public Result<Student> CreateStudent( string firstName, string lastName, DateTime birthDate, string photo, string gitHubLogin )
        {
            if( !IsNameValid( firstName ) ) return Result.Failure<Student>( Status.BadRequest, "The first name is not valid." );
            if( !IsNameValid( lastName ) ) return Result.Failure<Student>( Status.BadRequest, "The last name is not valid." );
            if( _studentGateway.FindByName( firstName, lastName ) != null ) return Result.Failure<Student>( Status.BadRequest, "A student with this name already exists." );
            if( !string.IsNullOrEmpty( gitHubLogin ) && _studentGateway.FindByGitHubLogin( gitHubLogin ) != null ) return Result.Failure<Student>( Status.BadRequest, "A student with GitHub login already exists." );
            _studentGateway.Create( firstName, lastName, birthDate, photo, gitHubLogin );
            Student student = _studentGateway.FindByName( firstName, lastName );
            return Result.Success( Status.Created, student );
        }

        public Result<Student> UpdateStudent( int studentId, string firstName, string lastName, DateTime birthDate, string photo, string gitHubLogin )
        {
            if( !IsNameValid( firstName ) ) return Result.Failure<Student>( Status.BadRequest, "The first name is not valid." );
            if( !IsNameValid( lastName ) ) return Result.Failure<Student>( Status.BadRequest, "The last name is not valid." );
            Student student;
            if( ( student = _studentGateway.FindById( studentId ) ) == null )
            {
                return Result.Failure<Student>( Status.NotFound, "Student not found." );
            }

            {
                Student s = _studentGateway.FindByName( firstName, lastName );
                if( s != null && s.StudentId != student.StudentId ) return Result.Failure<Student>( Status.BadRequest, "A student with this name already exists." );
            }

            if( !string.IsNullOrEmpty( gitHubLogin ) )
            {
                Student s = _studentGateway.FindByGitHubLogin( gitHubLogin );
                if( s != null && s.StudentId != student.StudentId ) return Result.Failure<Student>( Status.BadRequest, "A student with this GitHub login already exists." );
            }

            _studentGateway.Update( studentId, firstName, lastName, birthDate, photo, gitHubLogin );
            student = _studentGateway.FindById( studentId );
            return Result.Success( Status.Ok, student );
        }

        public Result<Student> GetById( int id )
        {
            Student student;
            if( ( student = _studentGateway.FindById( id ) ) == null ) return Result.Failure<Student>( Status.NotFound, "Student not found." );
            return Result.Success( Status.Ok, student );
        }

        public Result<int> Delete( int classId )
        {
            if( _studentGateway.FindById( classId ) == null ) return Result.Failure<int>( Status.NotFound, "Student not found." );
            _studentGateway.Delete( classId );
            return Result.Success( Status.Ok, classId );
        }
        */
        public Result<Article> Create( string title, int  authorId,  string content, DateTime datePost)
        {
            Article model = new Article();
            if( !IsNameValid( title ) ) return Result.Failure<Article>( Status.BadRequest, "The title is not valid." );
            if( !IsNameValid( content ) ) return Result.Failure<Article>( Status.BadRequest, "The content is not valid." );
            model.Title = title;
            model.AuthorId = authorId;
            model.Content = content;
            model.DatePost = model.DateLastEdit = datePost;
            model.ArticleId = _pocoArticles.Last().ArticleId++; 
            _pocoArticles.Add(model);
            //verifier si le contenue existe deja 
            
            return Result.Success( Status.Created, model);
        }

        public Result<Article> Create()
        {

            return null;
        }
        public Result<IEnumerable<Article>> GetAll()
        {
            //BIG POCO !!!
            IEnumerable<Article> poco = _pocoArticles;
            return Result.Success( Status.Ok, poco);
        }
        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
