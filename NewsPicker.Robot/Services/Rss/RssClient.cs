using NewsPicker.Robot.Models.Rss;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XamarinToolkit.Http;
using NewsPicker.Robot.Extensions;
using System.Net;

namespace NewsPicker.Robot.Services.Rss
{
    public class RssClient : RestClient
    {
        public RssClient() : base()
        {
            DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }

        public async Task<IList<RssItem>> GetItems(string url)
        {
            XDocument feed = await GetFeed(url);
            IList<RssItem> items = ParseFeed(feed);

            return items;
        }

        private async Task<XDocument> GetFeed(string url)
        {
            var result = await GetAsync(url);
            result.EnsureSuccessStatusCode();

            string content = await result.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(content))
            {
                XDocument feed = XDocument.Parse(content);
                return feed;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private IList<RssItem> ParseFeed(XDocument feed)
        {
            IList<RssItem> items = new List<RssItem>();

            foreach (var i in feed.Descendants("item"))
            {
                RssItem item = new RssItem()
                {
                    Title = i.Element("title")?.Value.IgnoreHtmlCharacters(),
                    Description = i.Element("description")?.Value.IgnoreHtmlCharacters(),
                    Link = i.Element("link")?.Value,
                    PubDate = i.Element("pubDate").Value.ToDateTime(),
                    Image = GetImageUrl(i)
                };

                items.Add(item);
            }

            return items;
        }

        private string GetImageUrl(XElement element)
        {
            try
            {
                return element.Descendants("enclosure")
                    .Where(e => e.Attribute("type").Value.Contains("image"))
                    .Select(e => e.Attribute("url").Value)
                    .First();
            }
            catch { }

            try
            {
                return element.Element("image").Element("url").Value;
            }
            catch { }

            return null;
        }
    }
}