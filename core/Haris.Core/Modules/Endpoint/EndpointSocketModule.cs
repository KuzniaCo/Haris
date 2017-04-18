using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Services;

namespace Haris.Core.Modules.Endpoint
{
    [DisableModule]
    public class EndpointSocketModule : HarisModuleBase<AttributedMessageEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly EngineService _engineService;

        public EndpointSocketModule(IEventAggregator eventAggregator, EngineService engineService)
        {
            _eventAggregator = eventAggregator;
            _engineService = engineService;
        }

        public override void Dispose()
        {
            //TODO throw new NotImplementedException();
        }

        public override void Init()
        {
            Task.Run(() =>
            {
                _engineService.StartSocketServer();
            });

        }

        public override void Handle(AttributedMessageEvent message)
        {

        }


    }
}