using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.Endpoint
{
    [DisableModule]
    public class EndpointModule : HarisModuleBase<AttributedMessageEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly EngineService _engineService;

        public EndpointModule(IEventAggregator eventAggregator, EngineService engineService)
        {
            _eventAggregator = eventAggregator;
            _engineService = engineService;
        }

        public override void Dispose()
        {
            Logger.LogInfo("Dispose Endpoint module");
        }

        public override void Init()
        {
            Logger.LogInfo("Start Endpoint module");
            _engineService.OpenSerialPort(115200, "COM3");
        }

        public override void Handle(AttributedMessageEvent message)
        {
            
        }
    }
}