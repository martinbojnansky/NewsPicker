using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsPicker.Robot.Extensions
{
    public static class HtmlExtensions
    {
        public static string IgnoreHtmlCharacters(this string value)
        {
            value = WebUtility.HtmlDecode(value);
            value = Regex.Replace(value, "<.*?>", string.Empty);
            value = Regex.Replace(value, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            value = value.Replace("&nbsp;", " ");
            value = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Replace(value, string.Empty);
            value = value.Trim();

            return value;
        }
    }
}