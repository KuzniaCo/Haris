using System;
using System.Web.Http;
using Haris.WebApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(StartupWebApi))]
namespace Haris.WebApi
{

    public class StartupWebApi
    {
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config).UseNancy();

        }
    }
}