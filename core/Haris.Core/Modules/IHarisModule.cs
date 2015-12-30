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

	public abstract class HarisModuleBase<TPayload> : IHarisModule, IHandle<TPayload> where TPayload: BaseEvent
	{
		public abstract void Dispose();
		public abstract void Init();
		public abstract void Handle(TPayload message);
	}

	public class TestHarisModule : HarisModuleBase<SampleTimeEvent>
	{
		private readonly IEventAggregator _eventAggregator;

		public TestHarisModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public override void Init()
		{
			Console.WriteLine("Test module running...");
			_eventAggregator.Subscribe(this);
		}

		public override void Dispose()
		{
			Console.WriteLine("Test module disposing...");
			_eventAggregator.Unsubscribe(this);
		}

		public override void Handle(SampleTimeEvent message)
		{
			Console.WriteLine("{0}: {1}", message.Payload, message.Id);
		}
	}

	public class SampleTimeEvent: BaseEvent<DateTime>
	{
		public SampleTimeEvent(DateTime payload)
		{
			Payload = payload;
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