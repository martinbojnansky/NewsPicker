using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using AutoMapper;
using NewsPicker.Database.AutoMapper;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Hosting;
using System;
using System.Collections.Generic;
using NewsPicker.Web;

namespace NewsPicker.Web
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
            ConfigureAutoMapper();
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            AddLocalizedRoute(config, "Articles", "", "Views/Articles/Articles.dothtml");

            // auto-register all dothtml files in the Views folder
            //config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void AddLocalizedRoute(DotvvmConfiguration config, string routeName, string url, string virtualPath, object defaultValues = null, Func<IDotvvmPresenter> presenterFactory = null)
        {
            url = $"{{{Constants.CultureRouteParameterName}?}}" + (string.IsNullOrWhiteSpace(url) ? "" : "/") + url;
            config.RouteTable.Add(routeName, url, virtualPath, defaultValues, presenterFactory);
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AutoDiscoverControls(new DefaultControlRegistrationStrategy(config, "controls", "Controls"));
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(config => AutoMapperConfig.CreateMap(config));
            Mapper.AssertConfigurationIsValid();
        }
    }
}