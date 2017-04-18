using System;
using System.Linq;
using Haris.Core.Services;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public sealed class TempCube : BaseCube
    {
        public TempCube(Cube cubeEntity, CubeRepository cubeRepository, EngineService engineService)
            : base( cubeEntity, cubeRepository, engineService)
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
            var temp = _cubeEntity.OutputCubes.FirstOrDefault(x => x.ValueName == "Temp");
            var date = _cubeEntity.OutputCubes.FirstOrDefault(x => x.ValueName == "Date");
            date.Value = DateTime.Now.ToString("G");
            temp.Value = messageItems[1];
            _cubeRepository.SaveChanges();
        }
    }
}
