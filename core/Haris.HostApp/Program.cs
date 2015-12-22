using System;
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
			Console.CancelKeyPress += delegate { bootstrapper.Shutdown(); };
			while(true) Console.ReadLine();
		}
	}
}
