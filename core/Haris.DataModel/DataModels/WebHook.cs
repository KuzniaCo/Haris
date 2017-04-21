using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haris.DataModel.DataModels
{
    public class WebHook
    {
        public int Id { get; set; }

        public int CubeId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Cube Cube { get; set; }
    }
}
