using System;
using System.Collections.Generic;
using LostTimeDB;

namespace LostTimeWeb.WebApp.Services
{
    public class NewsService
    {
        readonly  NewsGateway _newsGateway;

        public NewsService(NewsGateway newsGateway)
        {
            _newsGateway = newsGateway;
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
        public Result<IEnumerable<News>> GetAll()
        {
            //BIG POCO !!!
            List<News> pocoArticle = new List<News>();

            News a1 = new News();
            a1.NewsID = 0;
            a1.NewsTitle = "Next-gen of title";
            a1.NewsContent = "**Ex proident** elit ullamco consectetur tempor consectetur id. Sit aliquip deserunt nostrud excepteur occaecat commodo non dolore cupidatat est. Velit id sunt amet duis magna magna amet exercitation consequat sit nisi. Ex consequat elit culpa ullamco adipisicing reprehenderit dolore aliqua nisi proident magna mollit ad.";
            a1.NewsDate = DateTime.Now;
            a1.AuthorID = 1;
            a1.UpVote = 0;
            a1.DownVote = 0;
            a1.Editions = 0;
            pocoArticle.Add(a1);

            News a2 = new News();
            a2.NewsID = 1;
            a2.NewsTitle  = "Another title";
            a2.NewsContent = "Cillum esse ea Lorem non veniam voluptate. Culpa velit magna ullamco velit ad anim aliqua incididunt aute veniam ut. Adipisicing do ut fugiat magna ad cupidatat cupidatat qui do. Culpa exercitation veniam esse nulla ut eiusmod sint ad duis minim ipsum deserunt Lorem. Nostrud cillum labore esse ullamco do pariatur ad proident. Anim anim non dolore commodo ad commodo amet.";
            a2.NewsDate = DateTime.Now;
            a2.AuthorID = 1;
            a2.Popularity = 10;
            a2.Editions = 0;
            pocoArticle.Add(a2);

            IEnumerable<News> poco = pocoArticle;

            return Result.Success( Status.Ok, poco);
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
