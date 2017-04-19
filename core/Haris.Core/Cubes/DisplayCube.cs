using System;
using System.Linq;
using Haris.Core.Services;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public sealed class DisplayCube : BaseCube
    {
        public DisplayCube(Cube cubeEntity, CubeRepository cubeRepository, EngineService engineService)
            : base( cubeEntity, cubeRepository, engineService)
        {
        }

        private String[] messageItems;

        public override void ProcessMessage(string message)
        {
            messageItems = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void SetDisplayText(int row, string content)
        {
            int line = row + 1;
            _engineService.SendMessage(_cubeEntity.CubeAddress + "|" + (int)DisplayCube.Actions.SetDisplay + "|" + row + "|" + content);
            var lineEntity = _cubeEntity.OutputCubes.FirstOrDefault(x => x.ValueName == "Line "+ line);
            lineEntity.Value = content;
            _cubeRepository.SaveChanges();
        }

        public void TurnOnBacklight()
        {
            _engineService.SendMessage(_cubeEntity.CubeAddress + "|" + (int)DisplayCube.Actions.SetBacklight + "|1");
        }

        public void TurnOffBacklight()
        {
            _engineService.SendMessage(_cubeEntity.CubeAddress + "|" + (int)DisplayCube.Actions.SetBacklight + "|0");
        }

        public enum Actions
        {
            SetDisplay = 1,
            SetBacklight = 2
        }
    }
}
