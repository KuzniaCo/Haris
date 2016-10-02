using Haris.Core;
using System;
using System.Threading;

namespace Haris.HostApp
{
	class MainClass
	{
		private const string Logo = @"
   _    _            _     
  | |  | |          (_)    
  | |__| | __ _ _ __ _ ___ 
  |  __  |/ _` | '__| / __|
  | |  | | (_| | |  | \__ \
  |_|  |_|\__,_|_|  |_|___/
                           
                           ";

		public static void Main (string[] args)
		{
			var isDemoMode = Environment.OSVersion.Platform == PlatformID.Win32NT;
			Console.WriteLine(Logo);
			Console.WriteLine("-----------------------");
			if (isDemoMode)
			{
				Console.WriteLine("Note: Running on Windows, simulated GPIO");
			}
			Console.WriteLine("Press Ctrl+C to quit...");
			Console.WriteLine("-----------------------");
			var bootstrapper = new AppCoreBootstrapper();
			bootstrapper.Run(isDemoMode);
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
