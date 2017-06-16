using System;

namespace LostTimeWeb.WebApp.Models.NewsViewModels
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateLastEdit { get; set; }

        public DateTime DatePost { get; set; }

        public int AuthorId { get; set; }

        public int Popularity { get; set; }

        public int Editions { get; set; }
    }
}
