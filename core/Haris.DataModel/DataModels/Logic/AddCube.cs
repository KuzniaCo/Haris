using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haris.DataModel.DataModels.Logic
{
    public class AndCube : Cube
    {
        public string Value1 { get; set; }

        public string Value2 { get; set; }

        public List<Reaction> Out { get; set; }
    }
}
