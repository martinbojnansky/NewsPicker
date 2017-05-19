using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NewsPicker.Robot.Services.Rss
{
    [Obsolete]
    public class SyndicationClient
    {
        private XmlReaderSettings _settings = new XmlReaderSettings
        {
        };

        private XmlReader GetXmlReader(string url)
        {
            XmlReader xmlReader = XmlReader.Create(url, _settings);
            return xmlReader;
        }

        public SyndicationFeed GetFeed(string url)
        {
            SyndicationFeed feed;

            using (var xmlReader = GetXmlReader(url))
            {
                feed = SyndicationFeed.Load(xmlReader);
            }

            if (feed == null)
            {
                throw new NullReferenceException();
            }

            return feed;
        }
    }
}
