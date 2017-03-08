using System;

namespace Haris.DataModel.Cubes
{
    public abstract class BaseCube
    {
        public string Id { get; set; }

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
