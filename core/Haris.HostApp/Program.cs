using System;
using System.Threading;
using Haris.Core;

namespace Haris.HostApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine("-----------------------");
			Console.WriteLine("Press Ctrl+C to quit...");
			Console.WriteLine("-----------------------");
			var bootstrapper = new AppCoreBootstrapper();
			bootstrapper.Run();
			var mre = new ManualResetEvent(false);
			Console.CancelKeyPress += delegate
			{
				bootstrapper.Shutdown();
				mre.Set();
			};
			mre.WaitOne();
		}
	}
}
