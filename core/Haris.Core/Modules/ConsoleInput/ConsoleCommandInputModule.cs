using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.Command;
using Haris.Core.Events.IntentRecognition;

namespace Haris.Core.Modules.ConsoleInput
{
	public class ConsoleCommandInputModule: HarisModuleBase<CommandTextAcquiredEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		private CancellationTokenSource _cts;

		public ConsoleCommandInputModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_cts = new CancellationTokenSource();
		}

		public override void Dispose()
		{
			_cts.Cancel();
		}

		public override void Init()
		{
			Task.Run(() =>
			{
				while (_cts.IsCancellationRequested == false)
				{
					Console.WriteLine("Type commands to send to LUIS:");
					var cmd = Console.ReadLine();
					if (cmd != null)
					{
						_eventAggregator.Publish(new CommandTextAcquiredEvent(cmd));
					}
					else
					{
						break;
					}
					
				}
			}, _cts.Token);
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(CommandTextAcquiredEvent message)
		{
			_eventAggregator.Publish(new LuisApiRequest(message.Payload));
		}
	}
}