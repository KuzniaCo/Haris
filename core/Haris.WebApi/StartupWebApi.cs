using System;
using System.Linq;
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
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            app.UseWebApi(config).UseNancy();

        }
    }
}