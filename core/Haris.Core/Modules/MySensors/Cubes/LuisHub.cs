using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Haris.Core.Modules.MySensors.Cubes
{
    public class LuisHub
    {
        private List<HarisAction> OnPassedListAction = new List<HarisAction>();
        public string Entity { get; set; }

        public string Things { get; set; }

        public string Localization { get; set; }

        public void Process(string luisResult)
        {
            //TODO: If parsed result pass to property, invoke event
        }

        public void OnPassed()
        {
            OnPassedListAction.ForEach(x =>
            {
                Type xType = x.GetType();
                MethodInfo theMethod = xType.GetMethod(x.ActionName);
                theMethod.Invoke(x, null);
            });
        }
    }
}
