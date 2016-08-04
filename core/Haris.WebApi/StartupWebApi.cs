
using System;
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

			app.Run(context =>
			{
				context.Response.ContentType = "text/plain";

				string output = string.Format(
					"I'm running on {0} nFrom assembly {1}",
					Environment.OSVersion,
					System.Reflection.Assembly.GetEntryAssembly().FullName
					);

				return context.Response.WriteAsync(output);

			});
		}
	}
}