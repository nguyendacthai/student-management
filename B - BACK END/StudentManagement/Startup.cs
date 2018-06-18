using System.Web.Http;
using Microsoft.Owin;
using Owin;
using StudentManagement;
using StudentManagement.Configs;

[assembly: OwinStartup(typeof(Startup))]
namespace StudentManagement
{
    public class Startup
    {
        #region Methods

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var httpConfiguration = new HttpConfiguration();

            // Register web api configuration.
            WebApiConfig.Register(httpConfiguration);

            // Inversion of control registration.
            IocConfig.Register(appBuilder, httpConfiguration);

            // Add application global filter.
            FilterConfig.Register(httpConfiguration.Filters);

            // Register web API module.
            appBuilder.UseWebApi(httpConfiguration);
        }

        #endregion
    }
}