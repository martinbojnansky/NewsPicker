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
        public static void LoadCulture(this IDotvvmRequestContext context)
        {
            var cultureName = Cookies.Get(nameof(CultureInfo), "Name");

            if (!string.IsNullOrEmpty(cultureName))
            {
                context.ChangeCurrentCulture(cultureName);
            }
        }

        public static void ChangeCulture(this IDotvvmRequestContext context, string cultureName)
        {
            Cookies.Set(nameof(CultureInfo), "Name", cultureName);
            context.Configuration.DefaultCulture = cultureName;
            context.RedirectToRoute(context.Route.RouteName);
        }
    }
}