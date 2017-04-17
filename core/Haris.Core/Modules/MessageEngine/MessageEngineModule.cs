using System;
using System.Linq;
using System.Threading;
using Caliburn.Micro;
using Haris.Core.Cubes;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories;

namespace Haris.Core.Modules.MessageEngine
{
	public class MessageEngineModule : HarisModuleBase<MessageReceivedEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly ICubeRepository _cubeRepository;
		private readonly CancellationTokenSource _cts;

		public MessageEngineModule(IEventAggregator eventAggregator, ICubeRepository cubeRepository)
		{
			_eventAggregator = eventAggregator;
			_cubeRepository = cubeRepository;
			_cts = new CancellationTokenSource();
		}

		public override void Dispose()
		{
			_cts.Cancel();
			Logger.LogInfo("Message Engine Module Off");
			_eventAggregator.Unsubscribe(this);
		}

		public override void Init()
		{
			Logger.LogPrompt("Initilized Message Engine Module");
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(MessageReceivedEvent message)
		{
			RunInBusyContextWithErrorFeedback(() =>
			{
				Logger.LogPrompt("Recived message: " + message.Payload);
				var engineCube = CreateDeliveryCube(GetAddress(message.Payload));
				engineCube.ProcessMessage(message.Payload);
			}, _cts.Token);

		}

		private string GetAddress(string message)
		{
			String[] messageItems = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			return messageItems[0];
		}

		private BaseCube CreateDeliveryCube(string address)
		{
			Cube addressedCube = _cubeRepository.GetCube(address);
			if (addressedCube == null)
			{
				Logger.LogError("Not found cube addressed: " + address);
			}
			var cubeType = GetType().Assembly.GetTypes()
				.FirstOrDefault(x => x.Name.Contains(addressedCube.CubeType));
			Object[] args = { _eventAggregator, addressedCube, _cubeRepository };
			BaseCube cube = (BaseCube)Activator.CreateInstance(cubeType, args);
			return cube;
		}
	}
}
