using System;

namespace Haris.Core.Infrastructure
{
	internal static class AsyncActionsQueue
	{
		private static readonly ActionQueue ActionQueue = new ActionQueue();
		public static void Enqueue(Action action)
		{
			ActionQueue.Enqueue(action);
		}

		public static void _waitForDrying()
		{
			ActionQueue.Mre.WaitOne();
		}
	}
}