using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.DataModel.DataModels.Logic;

namespace Haris.DataModel.DataModels
{
    public class TempCube : Cube
    {
        public List<Reaction> OnTemperatureRecived { get; set; }

        public double Value { get; set; }
    }
}
