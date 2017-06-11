using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsPicker.Robot.Models.Facebook;
using NewsPicker.Robot.Services.Facebook;
using NewsPicker.Robot.Services.Analytics;
using NewsPicker.Database.Controllers;
using NewsPicker.Database.Models;

namespace NewsPicker.Robot.Services.Automation
{
    public class EngagementCountClient
    {
        private ErrorsClient _errors = new ErrorsClient();
        private FacebookClient _facebook = new FacebookClient();

        private ArticlesController _articles = new ArticlesController();

        public async Task UpdateAsync()
        {
            IEnumerable<Article> articles = null;

            _errors.Log($"{nameof(EngagementCountClient)}.{nameof(UpdateAsync)}()", () =>
            {
                articles = _articles.GetAll();
            });

            if (articles != null)
            {
                foreach (var article in articles)
                {
                    int engagementCount = await GetEngagementCountAsync(article.Url);
                    SaveEngagementCount(article.Id, engagementCount);
                }
            }
        }

        private async Task<int> GetEngagementCountAsync(string url)
        {
            OpenGraphEngagement engagement = null;

            await _errors.LogAsync($"{nameof(EngagementCountClient)}.{nameof(GetEngagementCountAsync)}({nameof(url)} = {url})", async () =>
            {
                engagement = await _facebook.GetEngagementAsync(url);
            });

            if (engagement != null)
            {
                return engagement.TotalEngagementCount;
            }
            else
            {
                return 0;
            }
        }

        private void SaveEngagementCount(int articleId, int engagementCount)
        {
            if (engagementCount > 0)
            {
                _errors.Log($"{nameof(EngagementCountClient)}.{nameof(SaveEngagementCount)}({nameof(articleId)} = {articleId}, {nameof(engagementCount)} = {engagementCount})", () =>
                {
                    _articles.UpdateEngagementCount(articleId, engagementCount);
                });
            }
        }
    }
}