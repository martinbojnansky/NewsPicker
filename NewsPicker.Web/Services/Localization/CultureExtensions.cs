using DotVVM.Framework.Hosting;
using NewsPicker.Web.Services.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NewsPicker.Web.Services.Localization
{
    public static class CultureExtensions
    {
        public static string LoadCulture(this IDotvvmRequestContext context)
        {
            var cultureName = (string)context.Parameters[Constants.CultureRouteParameterName];
            if (string.IsNullOrEmpty(cultureName))
            {
                context.LoadCultureNameCookie();
            }

            if (context.Configuration.DefaultCulture != cultureName)
            {
                context.ChangeCulture(cultureName);
            }

            context.ChangeCurrentCulture(cultureName);

            return cultureName;
        }

        private static void LoadCultureNameCookie(this IDotvvmRequestContext context)
        {
            var cultureName = Cookies.Get(nameof(CultureInfo), nameof(CultureInfo.Name));
            if (string.IsNullOrEmpty(cultureName))
            {
                context.ChangeCulture(Constants.DefaultCultureName);
            }
            else
            {
                context.ChangeCulture(cultureName);
            }
        }

        public static void ChangeCulture(this IDotvvmRequestContext context, string cultureName)
        {
            Cookies.Set(nameof(CultureInfo), nameof(CultureInfo.Name), cultureName);
            context.Configuration.DefaultCulture = cultureName;
            context.RedirectToRoute(context.Route.RouteName, new { CultureName = cultureName });
        }
    }
}