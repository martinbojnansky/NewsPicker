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
                sources = _sources.GetAll();
            });

            foreach (var source in sources)
            {
                IList<RssItem> items = await GetItems(source);
                IEnumerable<Article> articles = GetArticles(items, source);

                SaveArticles(articles);
                UpdateDate(source.Id);
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
                    CreatedDate = item.PubDate,
                    Image = item.Image,
                    SourceId = source.Id                  
                };

                //var image = GetArticleImage(item);
                //if (!string.IsNullOrWhiteSpace(image))
                //{
                //    article.Image = image;
                //}
            });

            return article;
        }

        //private string GetArticleImage(SyndicationItem item)
        //{
        //    string image = null;

        //    _errors.Log($"{nameof(ArticlesClient)}.{nameof(GetArticleImage)}({nameof(item)} = {item.Links.FirstOrDefault().Uri.AbsoluteUri})", () =>
        //    {
        //        image = item.Links.Where(l => l.Uri.AbsoluteUri.Contains(".png") || l.Uri.AbsoluteUri.Contains(".jpg"))
        //                .FirstOrDefault()?.Uri.AbsoluteUri;
        //    });

        //    return image;
        //}

        private void SaveArticles(IEnumerable<Article> articles)
        {
            if(articles == null)
            {
                return;
            }

            _errors.Log($"{nameof(ArticlesClient)}.{nameof(SaveArticles)}()", () =>
            {
                _articles.AddRange(articles);
            });
        }


        public void UpdateDate(int sourceId)
        {
            _errors.Log($"{nameof(ArticlesClient)}.{nameof(UpdateDate)}({nameof(sourceId)} = {sourceId})", () =>
            {
                _sources.UpdateDate(sourceId);
            });
        }
    }
}
