using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using NewsPicker.Web.Helpers.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NewsPicker.Web.Helpers.Localization
{
    public static class CultureExtensions
    {
        public static void AddCultureRouteParameterConstraint(this Dictionary<string, IRouteParameterConstraint> routeConstraints)
        {
            routeConstraints.Add(Constants.CultureRouteParameterName, new GenericRouteParameterType(part => "[a-z]{0,2}"));
        }

        public static void AddLocalizedRoute(this DotvvmRouteTable routeTable, string routeName, string url, string virtualPath, object defaultValues = null, Func<IDotvvmPresenter> presenterFactory = null)
        {
            var localizedUrl = $"{{{Constants.CultureRouteParameterName}:{Constants.CultureRouteParameterName}}}{(string.IsNullOrWhiteSpace(url) ? "" : "/")}{url}";
            routeTable.Add(routeName, localizedUrl, virtualPath, defaultValues, presenterFactory);
        }

        public static string LoadCulture(this IDotvvmRequestContext context)
        {
            var cultureName = (string)context.Parameters[Constants.CultureRouteParameterName];

            if (string.IsNullOrEmpty(cultureName) || cultureName == "xx")
            {
                context.LoadCultureNameCookie();
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
            context.RedirectToRoute(context.Route.RouteName, new { CultureName = cultureName });
        }
    }
}