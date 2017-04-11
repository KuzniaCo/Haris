using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Cubes;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories;
using TempCube = Haris.Core.Cubes.TempCube;

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
           Logger.LogInfo("Message Engine Module Off");
        }

        public override void Init()
        {
            Logger.LogPrompt("Initilized Message Engine Module");
            _eventAggregator.Subscribe(this);
        }

        public override void Handle(MessageReceivedEvent message)
        {
//
//            Task.Run(() =>
//            {
//                Logger.LogPrompt("Recived message: " + message.Payload);
//                var engineCube = CreateDeliveryCube(GetAddress(message.Payload));
//                engineCube.ProcessMessage(message.Payload);
//            }, _cts.Token);
           
        }

//        private string GetAddress(string message)
//        {
//            String[] messageItems = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
//            return messageItems[0];
//        }
//
//        private async BaseCube CreateDeliveryCube(string address)
//        {
//            Cube addressedCube = await _cubeRepository.GetCube(address);
//            if (addressedCube == null)
//            {
//                Logger.LogError("Not found cube addressed: " + address);
//            } 
//            var cubeType = GetType().Assembly.GetTypes()
//                .FirstOrDefault(x=>x.Name.Contains(addressedCube.CubeType));
//            Object[] args = {_eventAggregator, addressedCube, _cubeRepository};
//            BaseCube cube = (BaseCube)Activator.CreateInstance(cubeType, args);
//            return cube;
//        }
    }
}
