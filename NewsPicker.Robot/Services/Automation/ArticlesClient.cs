using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Robot.Services.Analytics;
using NewsPicker.Database.Models;
using NewsPicker.Database.Controllers;
using NewsPicker.Robot.Services.Rss;
using NewsPicker.Robot.Models.Rss;

namespace NewsPicker.Robot.Services.Automation
{
    public class ArticlesClient
    {
        private ErrorsClient _errors = new ErrorsClient();
        private RssClient _rss = new RssClient();

        private SourcesController _sources = new SourcesController();
        private ArticlesController _articles = new ArticlesController();

        public async Task UpdateAsync()
        {
            IList<Source> sources = null;

            _errors.Log($"{nameof(ArticlesClient)}.{nameof(UpdateAsync)}()", () =>
            {
                sources = _sources.GetActive();
            });

            foreach (var source in sources)
            {
                IList<RssItem> items = await GetItems(source);
                IEnumerable<Article> articles = GetArticles(items, source);

                SaveArticles(articles, source);
                UpdateDate(source);
            }
        }

        private async Task<IList<RssItem>> GetItems(Source source)
        {
            IList<RssItem> feed = null;

            await _errors.LogAsync($"{nameof(ArticlesClient)}.{nameof(GetItems)}({nameof(source)} = {source.Url})", async () =>
            {
                feed = await _rss.GetItems(source.Url);
            });

            return feed;
        }

        private IEnumerable<Article> GetArticles(IList<RssItem> items, Source source)
        {
            if (items == null)
            {
                return null;
            }

            List<Article> articles = new List<Article>();

            foreach (var item in items)
            {
                Article article = GetArticle(item, source);

                if (article != null)
                {
                    articles.Add(article);
                }
            }

            return articles;
        }

        private Article GetArticle(RssItem item, Source source)
        {
            Article article = null;

            _errors.Log($"{nameof(ArticlesClient)}.{nameof(GetArticle)}({nameof(item)} = {item.Link}, {nameof(source)} = {source.Url})", () =>
            {
                article = new Article()
                {
                    Title = item.Title,
                    Description = item.Description,
                    Url = item.Link,
                    UrlHash = item.Link.GetHashCode(),
                    CreatedDate = item.PubDate,
                    Image = item.Image
                };
            });

            return article;
        }

        private void SaveArticles(IEnumerable<Article> articles, Source source)
        {
            if (articles == null)
            {
                return;
            }

            _errors.Log($"{nameof(ArticlesClient)}.{nameof(SaveArticles)}()", () =>
            {
                _articles.AddRange(articles, source.Id);
            });
        }

        public void UpdateDate(Source source)
        {
            _errors.Log($"{nameof(ArticlesClient)}.{nameof(UpdateDate)}({nameof(source)} = {source})", () =>
            {
                _sources.UpdateDate(source.Id);
            });
        }
    }
}