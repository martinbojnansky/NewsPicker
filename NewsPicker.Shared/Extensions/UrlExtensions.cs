using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Shared.Extensions
{
    public static class UrlExtensions
    {
        public static string ToHostName(this string value)
        {
            var uri = new Uri(value);
            return uri.Host.Replace("www.", "");
        }
    }
}
