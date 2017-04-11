using System;
using Caliburn.Micro;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public sealed class TempCube : BaseCube
    {
        public TempCube(Cube cubeEntity, CubeRepository cubeRepository)
            : base( cubeEntity, cubeRepository)
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
                CubeId = _cubeEntity.Id,
                Date = DateTime.Now,
                OriginMessage = message,
                Value = value
            };
            _cubeRepository.AddLog(log);
            
        }
    }
}
