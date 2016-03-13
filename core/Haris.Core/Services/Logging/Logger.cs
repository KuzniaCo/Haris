using System;

namespace Haris.Core.Services.Logging
{
	public static class Logger
	{
		public static object SyncObject = new object();

		public static void Log(string message)
		{
			LogInfo(message);
		}

		public static void LogError(string message)
		{
			ColorWrap(ConsoleColor.Red, message);
		}

		public static void LogError(string format, params object[] parameters)
		{
			ColorWrap(ConsoleColor.White, format, parameters);
		}

		public static void LogInfo(string message)
		{
			ColorWrap(ConsoleColor.White, message);
		}

		public static void LogInfo(string format, params object[] parameters)
		{
			ColorWrap(ConsoleColor.White, format, parameters);
		}

		private static void LogInternal(string message)
		{
			Console.Write(DateTime.Now.ToString("HH:mm:ss> "));
			Console.WriteLine(message);
		}

		private static void ColorWrap(ConsoleColor color, string format, params object[] parameters)
		{
			ColorWrap(color, string.Format(format, parameters));
		}

		private static void ColorWrap(ConsoleColor color, string message)
		{
			lock (SyncObject)
			{
				var c = Console.ForegroundColor;
				Console.ForegroundColor = color;
				LogInternal(message);
				Console.ForegroundColor = c;
			}
		}

		public static void LogPrompt(string message)
		{
			ColorWrap(ConsoleColor.DarkGreen, message);
		}
	}
}