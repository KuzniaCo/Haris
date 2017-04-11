using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.Core.Cubes;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories;

namespace Haris.Core.Services
{
    public class EngineService
    {
        private readonly ICubeRepository _cubeRepository;

        public EngineService(ICubeRepository cubeRepository)
        {
            _cubeRepository = cubeRepository;
        }

        public void ProccessMessage(MessageReceivedEvent message)
        {

            
                Logger.LogPrompt("Recived message: " + message.Payload);
                var engineCube = CreateDeliveryCube(GetAddress(message.Payload));
                engineCube.ProcessMessage(message.Payload);
           
        }

        public string GetAddress(string message)
        {
            String[] messageItems = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            return messageItems[0];
        }

        public BaseCube CreateDeliveryCube(string address)
        {
            Cube addressedCube =  _cubeRepository.GetCube(address);
            if (addressedCube == null)
            {
                Logger.LogError("Not found cube addressed: " + address);
            }
            var cubeType = GetType().Assembly.GetTypes()
                .FirstOrDefault(x => x.Name.Contains(addressedCube.CubeType));
            Object[] args = { addressedCube, _cubeRepository };
            BaseCube cube = (BaseCube)Activator.CreateInstance(cubeType, args);
            return cube;
        }
    }
}
