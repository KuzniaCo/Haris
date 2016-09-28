using Haris.Core.Services.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Haris.Core.Infrastructure
{
	class ActionQueue
	{
		private readonly object _syncObject = new object();

		private readonly ConcurrentQueue<Action> _actionsQueue = new ConcurrentQueue<Action>();
		private bool _isStarted;

		public ManualResetEvent Mre { get; private set; }

		public ActionQueue()
		{
			Mre = new ManualResetEvent(false);
		}

		public void Enqueue(Action action)
		{
			_actionsQueue.Enqueue(action);
			lock (_syncObject)
			{
				if (_isStarted == false)
				{
					Start();
					_isStarted = true;
				}
			}
		}

		public void Start()
		{
			lock (_syncObject)
			{
				Mre.Reset();
#if DEBUG
				int minCompletionPort,
					minWorker,
					maxWorker,
					maxCompletionPort,
					availWorker,
					availCompletionPort;
				ThreadPool.GetMinThreads(out minWorker, out minCompletionPort);
				ThreadPool.GetMaxThreads(out maxWorker, out maxCompletionPort);
				ThreadPool.GetAvailableThreads(out availWorker, out availCompletionPort);

				Logger.LogInfo($"Queue start\n" +
				 $"Worker:  \t{minWorker}\t{availWorker}\t{maxWorker}\n"+
				 $"CompPort:\t{minCompletionPort}\t{availCompletionPort}\t{maxCompletionPort}");
#endif
			}
			Task.Factory.StartNew(Run, TaskCreationOptions.LongRunning);
		}

		void Run()
		{
			try
			{
				while (true)
				{
					Action action;
					if (_actionsQueue.TryDequeue(out action))
					{
						try
						{
							action();
						}
						catch (Exception e)
						{
							Logger.LogError("AAQ exception: {0}", e.Message);
						}
					}
					else
					{
						lock (_syncObject)
						{
							_isStarted = false;
							Mre.Set();
							return;
						}
					}
				}
			}
			catch (Exception e)
			{
				Logger.LogError("AAQ2 exception: {0}", e.Message);
			}
		}
	}
}