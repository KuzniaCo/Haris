using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.DataModel.DataModels;

namespace Haris.DataModel.WebApiContracts
{
    public class CubeResponse
    {
        public string Name { get; set; }

        public string CubeType { get; set; }

        public string CubeAddress { get; set; }

        public List<WebHook> WebHooks { get; set; }

        public Dictionary<string, string> Outputs { get; set; }
    }
}
