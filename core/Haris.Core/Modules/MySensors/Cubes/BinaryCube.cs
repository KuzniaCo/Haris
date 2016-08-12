using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Haris.Core.Modules.MySensors.Cubes
{
    public sealed class BinaryCube : BaseCube
    {
        private List<HarisAction>  OnChangedStatusActions = new List<HarisAction>();
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set
            {
                //TODO:SendMessage
                _status = value;
            }
        }

        public void ChangeStatus()
        {
            //TODO: Send message
        }

        public void AddAction(HarisAction action)
        {
            OnChangedStatusActions.Add(action);
        }

        public void OnChangedStatus()
        {
            OnChangedStatusActions.ForEach(x =>
            {
                Type xType = x.GetType();
                MethodInfo theMethod = xType.GetMethod(x.ActionName);
                theMethod.Invoke(x, null);
            });
        }
    
    }
}
