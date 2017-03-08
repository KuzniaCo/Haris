using System.Reflection;
using Haris.Core.Events.System;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.MySensors
{
    public class MySensorControllerModule : HarisModuleBase<SystemActionRequest>
    {
        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            Logger.LogPrompt("Initilize MySensorsController");
        }

        public override void Handle(SystemActionRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}