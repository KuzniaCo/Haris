using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.Samples;

namespace Haris.Core.Modules.Samples
{
	[DisableModule]
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