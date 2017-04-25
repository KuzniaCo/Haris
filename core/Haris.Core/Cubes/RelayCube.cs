using Caliburn.Micro;
using Haris.Core.Services;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public sealed class RelayCube : BaseCube
    {
        public bool Status { get; set; }

        public RelayCube( Cube cubeEntity, CubeRepository cubeRepository, EngineService engineService) : base(cubeEntity, cubeRepository, engineService)
        {
        }

        public void TurnOn()
        {
            _engineService.SendMessage(_cubeEntity.CubeAddress + "|1");
        }

        public void TurnOff()
        {
            _engineService.SendMessage(_cubeEntity.CubeAddress + "|0");
        }

        public override void ProcessMessage(string message)
        {
        }
    }
}
