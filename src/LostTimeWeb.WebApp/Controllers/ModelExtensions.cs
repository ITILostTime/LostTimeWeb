using LostTimeDB;
using LostTimeWeb.WebApp.Models.NewsViewModels;
using LostTimeWeb.WebApp.Models.ManagerAccountViewModel;

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

        public static UserViewModel ToUserViewModel(this UserAccount @this)
        {
            return new UserViewModel
            {
                UserID = @this.UserID,
                UserPseudonym = @this.UserPseudonym,
                UserEmail = @this.UserEmail,
                UserPassword = @this.UserPassword,
                UserAccountCreationDate = @this.UserAccountCreationDate,
                UserLastConnectionDate = @this.UserLastConnectionDate,
                UserGoogleToken = @this.UserGoogleToken,
                UserGoogleID = @this.UserGoogleID,
                UserPermission = @this.UserPermission
            };
        }
        public static UserForDisplayViewModel ToUserForDisplayViewModel(this UserAccount @this)
        {
            return new UserForDisplayViewModel
            {
                UserID = @this.UserID,
                UserPseudonym = @this.UserPseudonym,
                UserEmail = @this.UserEmail,
                UserAccountCreationDate = @this.UserAccountCreationDate,
                UserLastConnectionDate = @this.UserLastConnectionDate,
                UserGoogleID = @this.UserGoogleID,
                UserPermission = @this.UserPermission
            };
        }
    }
}
