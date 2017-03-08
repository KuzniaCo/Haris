using System;
using System.Collections.Generic;
using System.Reflection;

namespace Haris.DataModel.Cubes
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
    
    }
}
