using System;
using System.Collections.Generic;
using LostTimeWeb.DAL;

namespace LostTimeWeb.WebApp.Services
{
    public class NewsService
    {
        readonly StudentGateway _studentGateway;

        public NewsService(StudentGateway studentGateway)
        {
            _studentGateway = studentGateway;
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
        public Result<IEnumerable<Article>> GetAll()
        {
            //BIG POCO !!!
            List<Article> pocoArticle = new List<Article>();

            Article a1 = new Article();
            a1.ArticleId = 0;
            a1.Title = "Next-gen of title";
            a1.Content = "Ex proident elit ullamco consectetur tempor consectetur id. Sit aliquip deserunt nostrud excepteur occaecat commodo non dolore cupidatat est. Velit id sunt amet duis magna magna amet exercitation consequat sit nisi. Ex consequat elit culpa ullamco adipisicing reprehenderit dolore aliqua nisi proident magna mollit ad.";
            a1.DateLastEdit = DateTime.Now;
            a1.DatePost = DateTime.Now;
            a1.AuthorId = 1;
            a1.Popularity = 0;
            a1.Editions = 0;
            pocoArticle.Add(a1);

            Article a2 = new Article();
            a2.ArticleId = 1;
            a2.Title = "Another title";
            a2.Content = "Cillum esse ea Lorem non veniam voluptate. Culpa velit magna ullamco velit ad anim aliqua incididunt aute veniam ut. Adipisicing do ut fugiat magna ad cupidatat cupidatat qui do. Culpa exercitation veniam esse nulla ut eiusmod sint ad duis minim ipsum deserunt Lorem. Nostrud cillum labore esse ullamco do pariatur ad proident. Anim anim non dolore commodo ad commodo amet.";
            a2.DateLastEdit = DateTime.Now;
            a2.DatePost = DateTime.Now;
            a2.AuthorId = 1;
            a2.Popularity = 10;
            a2.Editions = 0;
            pocoArticle.Add(a2);

            IEnumerable<Article> poco = pocoArticle;

            return Result.Success( Status.Ok, poco);
        }

        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
