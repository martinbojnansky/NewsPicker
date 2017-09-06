using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.Models;
using NewsPicker.Database.Controllers;
using NewsPicker.Shared.Constants;

namespace NewsPicker.Database.Controllers
{
    public class ArticlesController
    {
        private NewsPickerDatabase _db = new NewsPickerDatabase();

        public IList<Article> GetAll()
        {
            return _db.Articles.ToList();
        }

        public void AddRange(IEnumerable<Article> articles, int sourceId)
        {
            var source = _db.Sources.FirstOrDefault(s => s.Id == sourceId);

            foreach (var article in articles)
            {
                var existingArticle = _db.Articles.FirstOrDefault(a => a.UrlHash == article.UrlHash && a.Url == article.Url);

                if (existingArticle == null)
                {
                    source.Articles.Add(article);
                }
                else if (!existingArticle.Sources.Contains(source))
                {
                    existingArticle.Sources.Add(source);
                }
            }

            _db.SaveChanges();
        }

        public void UpdateEngagementCount(long articleId, int engagementCount)
        {
            var article = _db.Articles.Find(articleId);

            if (article != null)
            {
                article.EngagementCount = engagementCount;
                _db.SaveChanges();
            }
        }

        public void DeleteOld()
        {
            DateTime? startDate = ArticleConstants.MinArticleCreatedDate;
            var articles = _db.Articles.Where(a => startDate > a.CreatedDate);

            if (articles.Count() > 0)
            {
                _db.Articles.RemoveRange(articles);
                _db.SaveChanges();
            }
        }
    }
}