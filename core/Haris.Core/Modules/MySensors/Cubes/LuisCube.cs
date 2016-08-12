using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Haris.Core.Modules.MySensors.Cubes
{
    public class LuisEndPointCube : BaseCube
    {
        private readonly List<HarisAction> OnResponseListAction = new List<HarisAction>();

        public void ProccedIntent(string textInput)
        {
            //TODO:SendMessage to Luis WebService. When response invoke event
        }

        public void AddOnResponseAction(HarisAction action)
        {
            OnResponseListAction.Add(action);
        }
        public void OnResponse(string result)
        {
            OnResponseListAction.ForEach(x =>
            {
                Type xType = x.GetType();
                MethodInfo theMethod = xType.GetMethod(x.ActionName);
                theMethod.Invoke(x, null);
            });
        }
    }
}
