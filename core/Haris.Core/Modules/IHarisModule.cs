using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events;

namespace Haris.Core.Modules
{
	public interface IHarisModule: IDisposable
	{
		void Init();
	}

	public class TestHarisModule : IHarisModule, IHandle<SampleTimeEvent>
	{
		private readonly IEventAggregator _eventAggregator;

		public TestHarisModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public void Init()
		{
			Console.WriteLine("Test module running...");
			_eventAggregator.Subscribe(this);
		}

		public void Dispose()
		{
			Console.WriteLine("Test module disposing...");
			_eventAggregator.Unsubscribe(this);
		}

		public void Handle(SampleTimeEvent message)
		{
			Console.WriteLine("{0}: {1}", message.Time, message.Id);
		}
	}

	public class SampleTimeEvent: BaseEvent
	{
		public DateTime Time { get; set; }

		public SampleTimeEvent(DateTime time)
		{
			Time = time;
		}
	}

	public class ClockModule : IHarisModule
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly CancellationTokenSource _cts;

		public ClockModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_cts = new CancellationTokenSource();
		}

		public void Dispose()
		{
			_cts.Cancel();
		}

		public void Init()
		{
			Task.Run(async () =>
			{
				while (_cts.IsCancellationRequested == false)
				{
					_eventAggregator.Publish(new SampleTimeEvent(DateTime.Now));
					await Task.Delay(1000, _cts.Token); 
				}
			}, _cts.Token);
		}
	}
}