using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XamarinToolkit.Http;
using NewsPicker.Robot.Models.Facebook;
using NewsPicker.Shared.Constants;

namespace NewsPicker.Robot.Services.Facebook
{
    public class FacebookClient : RestClient
    {
        public FacebookClient() : base()
        {
            Init("https://graph.facebook.com/v2.9/", $"OAuth {ApiConstants.FACEBOOK_KEY}");
        }

        public async Task<OpenGraphEngagement> GetEngagementAsync(string url)
        {
            OpenGraphEngagement engagement = (await GetAsync<OpenGraphUrl>($"?id={url}&fields=engagement")).Engagement;
            return engagement;
        }
    }
}