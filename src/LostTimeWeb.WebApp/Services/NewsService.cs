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
        public List<News> _pocoArticles; 

        public NewsService(NewsGateway newsGateway)
        {
            _newsGateway = newsGateway;
            _pocoArticles = SetPoco();
        }
        
        public List<News> SetPoco()
        {
            List<News> pocoArticles = new List<News>();

            News a1 = new News();
            a1.NewsID = 0;
            a1.NewsTitle = "Next gen of title";
            a1.NewsContent = "**Ex proident** elit ullamco consectetur tempor consectetur id. Sit aliquip deserunt nostrud excepteur occaecat commodo non dolore cupidatat est. Velit id sunt amet duis magna magna amet exercitation consequat sit nisi. Ex consequat elit culpa ullamco adipisicing reprehenderit dolore aliqua nisi proident magna mollit ad.";
            a1.NewsCreationDate = DateTime.Now;
            a1.NewsLastUpdate = DateTime.Now;
            a1.NewsAuthorID = 1;
            a1.NewsGoodVote = 1;
            a1.NewsBadVote = 31;
            a1.NewsEditionNb = 0;
            pocoArticles.Add(a1);

            News a2 = new News();
            a2.NewsID = 0;
            a2.NewsTitle = "Another title";
            a2.NewsContent = "Cillum esse ea Lorem non veniam voluptate. Culpa velit magna ullamco velit ad anim aliqua incididunt aute veniam ut. Adipisicing do ut fugiat magna ad cupidatat cupidatat qui do. Culpa exercitation veniam esse nulla ut eiusmod sint ad duis minim ipsum deserunt Lorem. Nostrud cillum labore esse ullamco do pariatur ad proident. Anim anim non dolore commodo ad commodo amet.";
            a2.NewsCreationDate = DateTime.Now;
            a2.NewsLastUpdate = DateTime.Now;
            a2.NewsAuthorID = 1;
            a2.NewsGoodVote = 25;
            a2.NewsBadVote = 1;
            a2.NewsEditionNb = 0;
            pocoArticles.Add(a2);

            return pocoArticles;
        }
        public Result<News> Create(string title, int authorId, string content)
        {
            News model = new News();
            if (!IsNameValid(title)) return Result.Failure<News>(Status.BadRequest, "The title is not valid.");
            if (!IsNameValid(content)) return Result.Failure<News>(Status.BadRequest, "The content is not valid.");
            model.NewsTitle = title;
            model.NewsAuthorID = authorId;
            model.NewsContent = content;
            model.NewsCreationDate = model.NewsLastUpdate = DateTime.Now;
            model.NewsID = _pocoArticles.Last().NewsID++;
            _pocoArticles.Add(model);
            //verifier si le contenue existe deja 

            return Result.Success(Status.Created, model);
        }
        public Result<IEnumerable<News>> GetAll()
        {
            //BIG POCO !!!
            IEnumerable<News> poco = _pocoArticles;
            return Result.Success(Status.Ok, poco);
        }
        bool IsNameValid( string name ) => !string.IsNullOrWhiteSpace( name );
    }
}
