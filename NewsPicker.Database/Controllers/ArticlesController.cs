using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.Enums;

namespace NewsPicker.Database.Controllers
{
    public class ArticlesController
    {
        private NewsPickerDatabase _db = new NewsPickerDatabase();

        public IList<Article> GetAll()
        {
            return _db.Articles.ToList();
        }

        public void AddRange(IEnumerable<Article> articles)
        {
            foreach (var article in articles)
            {
                if (_db.Articles.Count(a => a.Url == article.Url && a.SourceId == article.SourceId) == 0)
                {
                    _db.Articles.Add(article);
                }
            }

            _db.SaveChanges();
        }

        public void UpdateShareCount(int articleId, int shareCount)
        {
            var article = _db.Articles.Find(articleId);

            if (article != null)
            {
                article.ShareCount = shareCount;
                _db.SaveChanges();
            }
        }

        public void DeleteOld()
        {
            DateTime? startDate = DateTime.UtcNow.AddHours(-(int)TimePeriod.MONTH);
            IQueryable<Article> articles = _db.Articles.Where(a => startDate > a.CreatedDate);

            if (articles.Count() > 0)
            {
                _db.Articles.RemoveRange(articles);
                _db.SaveChanges();
            }
        }
    }
}