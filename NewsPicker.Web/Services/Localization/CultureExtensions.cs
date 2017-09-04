using DotVVM.Framework.Hosting;
using NewsPicker.Web.Services.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace NewsPicker.Web.Services.Localization
{
    public static class CultureExtensions
    {
        public static void LoadCulture(this IDotvvmRequestContext context)
        {
            if (!Cookies.Enabled) return;

            var cultureName = Cookies.Get(nameof(CultureInfo), nameof(CultureInfo.Name));

            if (!string.IsNullOrEmpty(cultureName))
            {
                context.ChangeCurrentCulture(cultureName);
            }
        }

        public static void ChangeCulture(this IDotvvmRequestContext context, string cultureName)
        {
            if (!Cookies.Enabled) return;

            Cookies.Set(nameof(CultureInfo), nameof(CultureInfo.Name), cultureName);
            context.Configuration.DefaultCulture = cultureName;
            context.RedirectToRoutePermanent(context.Route.RouteName);
        }
    }
}