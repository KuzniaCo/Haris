using System;
using System.Threading;
using Haris.Core;
using Haris.WebApi;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;

namespace Haris.HostApp
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("-----------------------");
			Console.WriteLine("Press Ctrl+C to quit...");
			Console.WriteLine("-----------------------");
			var bootstrapper = new AppCoreBootstrapper();
			string WebApiUrl = "http://*:88";
			bootstrapper.Run();
			var owinHost = WebApp.Start<StartupWebApi>(WebApiUrl);
           

			var mre = new ManualResetEvent(false);
			Console.CancelKeyPress += delegate
			{
				bootstrapper.Shutdown();
				owinHost.Dispose();
				mre.Set();
			};
			mre.WaitOne();
		}
	}
}
