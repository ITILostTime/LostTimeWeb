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
                DateLastEdit = @this.DateLastEdit,
                DatePost = @this.NewsDate,
                AuthorId = @this.AuthorID,
                UpVote = @this.UpVote,
                DownVote = @this.DownVote,
                Editions = @this.Editions
            };
        }
    }
}
