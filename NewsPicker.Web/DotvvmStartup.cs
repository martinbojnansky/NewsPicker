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
            config.RouteTable.Add("Default", "", "Views/Articles/Articles.dothtml");

            // auto-register all dothtml files in the Views folder
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            //// register custom resources and adjust paths to the built-in resources
            //config.Resources.Register("style.less", new StylesheetResource(new FileResourceLocation("~/Resources/Stylesheets/style.less")));
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(config => AutoMapperConfig.CreateMap(config));
            Mapper.AssertConfigurationIsValid();
        }
    }
}