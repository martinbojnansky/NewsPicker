using NewsPicker.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Shared.Models;

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
            DateTime? startDate = DateTime.UtcNow.AddHours(-(int)TimePeriodValue.WEEK);
            IQueryable<Article> articles = _db.Articles.Where(a => startDate > a.CreatedDate);

            if (articles.Count() > 0)
            {
                _db.Articles.RemoveRange(articles);
                _db.SaveChanges();
            }
        }
    }
}