using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.Command;
using Haris.Core.Events.System;
using Haris.DataModel.Luis;

namespace Haris.Core.Modules.ConsoleInput
{
	public class ConsoleCommandInputModule: HarisModuleBase<SystemActionRequest>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly CancellationTokenSource _cts;

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

		public override void Handle(SystemActionRequest message)
		{
			Task.Run(() =>
			{
				var action = message.Payload;
				if (action == null || action.OriginalIntent is LuisResponseDto == false)
				{
					Console.WriteLine("ERR: Not a LUIS recognizer or empty result.");
					return;
				}
				var intent = (LuisResponseDto) action.OriginalIntent;
				Console.WriteLine("{2}> LUIS API response: {0} {1}", intent.MostProbableIntent.Intent,
					string.Join(", ",
						intent.Entities.DefaultIfEmpty(new LuisEntity {Entity = "NONE"})
							.OrderByDescending(e => e.Score)
							.Select(e => string.Format("{0}:{1}", e.Entity, e.Type))), DateTime.Now.ToString("HH:m:s"));
			}, _cts.Token);
		}
	}
}