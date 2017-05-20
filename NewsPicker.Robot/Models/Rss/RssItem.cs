using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Robot.Models.Rss
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
        public string Image { get; set; }
    }
}
