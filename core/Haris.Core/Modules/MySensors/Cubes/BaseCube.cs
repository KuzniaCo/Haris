using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haris.Core.Modules.MySensors.Cubes
{
    public abstract class BaseCube
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void Ping()
        {
            //TODO:SendMessage
        }

        public void OnPong()
        {
            //TODO:DoSomething
        }
    }
}
