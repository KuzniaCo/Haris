using System;
using Caliburn.Micro;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public sealed class RelayCube : BaseCube
    {
        private bool _status;
        public bool Status
        {
            get { return _status; }
        }

        public void ChangeStatus()
        {
            //TODO: Send message
        }

        public void TurnOn()
        {
            throw new NotImplementedException();
        }

        public void TurnOff()
        {
            throw new NotImplementedException();
        }

        public void OnChangedStatus()
        {
            throw new NotImplementedException();
        }

        public RelayCube(IEventAggregator eventAggregator, Cube cubeEntity, CubeRepository cubeRepository) : base(cubeEntity, cubeRepository)
        {
        }

        public override void ProcessMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
