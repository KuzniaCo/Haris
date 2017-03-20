using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
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
            if (_cubeEntity.Logs == null)
            {
                _cubeEntity.Logs = new List<Log>();
            }
            _cubeEntity.Logs.Add(log);
            _cubeRepository.UpdateCube(_cubeEntity);
        }


        public void SetInterval(int timeInSeconds)
        {
            _eventAggregator.Publish(new AttributedMessageEvent(_cubeEntity + "|" + TempCubeActions.SET_INTERVAL + "|"));
        }

        public enum TempCubeActions
        {
            GET_CURRENT_TEMP = 0,
            GET_BATTERY_LEVEL = 1,
            SET_INTERVAL = 2
        }
    }
}
