using System;
using Haris.Core;

namespace Haris.HostApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var bootstrapper = new AppCoreBootstrapper();
			bootstrapper.Run();
			Console.WriteLine("-----------------------");
			Console.WriteLine("Press Enter to quit...");
			Console.ReadLine();
			bootstrapper.Shutdown();
		}
	}
}
