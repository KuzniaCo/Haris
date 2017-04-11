using Caliburn.Micro;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes
{
    public abstract class BaseCube
    {
        protected readonly Cube _cubeEntity;
        protected readonly CubeRepository _cubeRepository;

        public BaseCube(Cube cubeEntity, CubeRepository cubeRepository)
        {
            _cubeEntity = cubeEntity;
            _cubeRepository = cubeRepository;
        }

        public string Id { get; set; }

        public string CubeAddress { get; set; }

        public void Ping()
        {
            //TODO:SendMessage
        }

        public void OnPong()
        {
            //TODO:DoSomething
        }

        public abstract void ProcessMessage(string messag);
    }
}
