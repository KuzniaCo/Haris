using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Haris.Core.Infrastructure
{
	class ActionQueue
	{
		private readonly object _syncObject = new object();

		private ConcurrentQueue<Action> _actionsQueue;
		private bool _isStarted;

		public ManualResetEvent Mre { get; private set; }

		public ActionQueue()
		{
			Mre = new ManualResetEvent(false);
		}

		public void Enqueue(Action action)
		{
			lock (_syncObject)
			{
				if (_isStarted == false)
				{
					Start();
					_isStarted = true;
				}
			}
			_actionsQueue.Enqueue(action);
		}

		public void Start()
		{
			Mre.Reset();
			_actionsQueue = new ConcurrentQueue<Action>();
			ThreadPool.QueueUserWorkItem(_ => Run(), null);
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
						catch (Exception)
						{
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
			catch (Exception)
			{
			}
		}
	}
}