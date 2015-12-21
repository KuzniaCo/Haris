using System;

namespace Haris.Core.Modules
{
	public interface IHarisModule: IDisposable
	{
		void Init();
	}

	public class TestHarisModule : IHarisModule
	{
		public void Init()
		{
			Console.WriteLine("Test module running...");
		}

		public void Dispose()
		{
			Console.WriteLine("Test module disposing...");
		}
	}
}