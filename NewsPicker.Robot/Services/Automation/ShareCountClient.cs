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
    public class ShareCountClient
    {
        private ErrorsClient _errors = new ErrorsClient();
        private FacebookClient _facebook = new FacebookClient();

        private ArticlesController _articles = new ArticlesController();

        public async Task UpdateAsync()
        {
            IEnumerable<Article> articles = null;

            _errors.Log($"{nameof(ShareCountClient)}.{nameof(UpdateAsync)}()", () =>
            {
                articles = _articles.GetAll();
            });

            if (articles != null)
            {
                foreach (var article in articles)
                {
                    int shareCount = await GetShareCountAsync(article.Url);
                    SaveShareCount(article.Id, shareCount);
                }
            }
        }

        private async Task<int> GetShareCountAsync(string url)
        {
            OpenGraphShare share = null;

            await _errors.LogAsync($"{nameof(ShareCountClient)}.{nameof(GetShareCountAsync)}({nameof(url)} = {url})", async () =>
            {
                share = await _facebook.GetShareAsync(url);
            });

            if (share != null && share.ShareCount.HasValue)
            {
                return share.ShareCount.Value;
            }
            else
            {
                return 0;
            }
        }

        private void SaveShareCount(int articleId, int shareCount)
        {
            if (shareCount > 0)
            {
                _errors.Log($"{nameof(ShareCountClient)}.{nameof(SaveShareCount)}({nameof(articleId)} = {articleId}, {nameof(shareCount)} = {shareCount})", () =>
                {
                    _articles.UpdateShareCount(articleId, shareCount);
                });
            }
        }
    }
}
