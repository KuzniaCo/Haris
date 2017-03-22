using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.Core.Cubes.Logic
{
    public class AndCube : BaseCube
    {
        public AndCube(IEventAggregator eventAggregator, Cube cubeEntity, CubeRepository cubeRepository) : base(eventAggregator, cubeEntity, cubeRepository)
        {
        }

        public bool Value1 { get; set; }

        public bool Value2 { get; set; }

        public override void ProcessMessage(string messag)
        {
            if (Value1 && Value2)
            {
                
            }
        }
    }
}
