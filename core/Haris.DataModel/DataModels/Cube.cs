using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haris.DataModel.DataModels
{
    public class Cube
    {
        public int Id { get; set; }

        public string CubeAddress { get; set; }
        
        public string CubeType { get; set; }

        public string Name { get; set; }

        public virtual List<Log> Logs { get; set; }

        public virtual List<WebHook> WebHooks { get; set; }

        public virtual List<OutputCube> OutputCubes { get; set; }
    }
}
