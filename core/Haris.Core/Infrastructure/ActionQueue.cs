using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Haris.Core.Infrastructure
{
	public class ActionQueue
	{
		private readonly object _syncObject = new object();

		private ConcurrentQueue<Action> _actionsQueue;
		private bool _isStarted;
		private Task _taskReference;

		private readonly ManualResetEvent _mre;

		public ManualResetEvent Mre
		{
			get
			{
				lock (_syncObject)
				{
					return _mre;
				}
			}
		}

		public ActionQueue()
		{
			_mre = new ManualResetEvent(false);
		}

		public void Enqueue(Action action)
		{
			lock (_syncObject)
			{
				if (_isStarted == false)
				{
					_taskReference?.Wait();
					Start();
					_isStarted = true;
				}
			}
			_actionsQueue.Enqueue(action);
		}

		public void Start()
		{
			lock (_syncObject)
			{
				Mre.Reset();
				_actionsQueue = _actionsQueue ?? new ConcurrentQueue<Action>();
			}
			_taskReference = Task.Run(() => Run()).ContinueWith(t => Mre.Set());
		}

		void Run()
		{
			try
			{
				while (true)
				{
					Action action;
					bool dequeued;
					lock (_syncObject)
					{
						dequeued = _actionsQueue.TryDequeue(out action);
					}
					if (dequeued)
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
							if (_actionsQueue.IsEmpty == false)
							{
								continue;
							}
							_isStarted = false;
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