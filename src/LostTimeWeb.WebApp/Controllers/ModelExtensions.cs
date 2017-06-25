using LostTimeDB;
using LostTimeWeb.WebApp.Models.NewsViewModels;

namespace LostTimeWeb.WebApp.Controllers
{
    public static class ModelExtensions
    {
        public static ArticleViewModel ToArticleViewModel(this News @this)
        {
            return new ArticleViewModel
            {
                ArticleId = @this.NewsID,
                Title = @this.NewsTitle,
                Content = @this.NewsContent,
                DateLastEdit = @this.NewsLastUpdate,
                DatePost = @this.NewsCreationDate,
                AuthorId = @this.NewsAuthorID,
                UpVote = @this.NewsGoodVote,
                DownVote = @this.NewsBadVote,
                Editions = @this.NewsEditionNb
            };
        }
    }
}
