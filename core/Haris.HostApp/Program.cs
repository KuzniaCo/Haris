using Haris.Core;
using System;
using System.Threading;

namespace Haris.HostApp
{
	class MainClass
	{
		private const string Logo = @"  _    _            _     
 | |  | |          (_)    
 | |__| | __ _ _ __ _ ___ 
 |  __  |/ _` | '__| / __|
 | |  | | (_| | |  | \__ \
 |_|  |_|\__,_|_|  |_|___/
                          
                          ";
		public static void Main (string[] args)
		{
			bool isTestMode = false;
			Console.WriteLine("Welcome to Haris system with GPIO simulation.");
			Console.Write("Would you like to run in test mode? [Y/N] ");
			var c = Console.ReadKey();
			Console.WriteLine();
			isTestMode = c.Key == ConsoleKey.Y;
			Console.WriteLine();
			Console.WriteLine(Logo);
			Console.WriteLine("-----------------------");
			Console.WriteLine("Press Ctrl+C to quit...");
			Console.WriteLine("-----------------------");
			var bootstrapper = new AppCoreBootstrapper();
			bootstrapper.Run(isTestMode);
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
