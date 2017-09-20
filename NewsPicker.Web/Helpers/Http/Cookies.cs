using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPicker.Web.Helpers.Http
{
    public static class Cookies
    {
        public static string Get(string cookie, string key)
        {
            return HttpContext.Current.Request.Cookies[cookie]?[key];
        }

        public static void Set(string name, string key, object value, DateTime? expires = null)
        {
            var cookie = HttpContext.Current.Response.Cookies[name];
            cookie.Expires = expires.HasValue ? expires.Value : DateTime.MaxValue;
            cookie[key] = value.ToString();
        }
    }
}