using System;
using Caliburn.Micro;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public sealed class TempCube : BaseCube
    {
        public TempCube(IEventAggregator eventAggregator, Cube cubeEntity, CubeRepository cubeRepository)
            : base(eventAggregator, cubeEntity, cubeRepository)
        {
        }

        private String[] messageItems;

        public override void ProcessMessage(string message)
        {
            messageItems = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            OnReceivedTemp(messageItems[1], message);
        }

        public void OnReceivedTemp(string value, string message)
        {
            var log = new Log()
            {
                Date = DateTime.Now,
                OriginMessage = message,
                Value = value
            };
            _cubeRepository.AddLog(log, messageItems[0]);
            
        }
    }
}
