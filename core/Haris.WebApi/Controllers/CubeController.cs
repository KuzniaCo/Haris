using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Haris.DataModel;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;
using Haris.DataModel.WebApiContracts;

namespace Haris.WebApi.Controllers
{
    [RoutePrefix("api/cubes")]
    public class CubeController : ApiController
    {
        private CubeRepository _cubeRepository;

        public CubeController()
        {
            _cubeRepository = new CubeRepository(new HarisDbContext());
        }

        // GET api/cubes 
        [Route("")]
        [HttpGet]
        public IEnumerable<CubeResponse> Get()
        {
           var data = _cubeRepository.GetCubes();
           var result = data.ToList().Select(x => new CubeResponse()
            {
                CubeAddress = x.CubeAddress,
                CubeType = x.CubeType,
                Name = x.Name,
                Outputs = MapOutputs(x.OutputCubes)
            });

            return result;
        }

        // GET api/cubes/{address}
        [HttpGet]
        [Route("{address}")]
        public Cube Get(string address)
        {
            return _cubeRepository.GetCube(address);
        }

        private Dictionary<string, string> MapOutputs(List<OutputCube>  outputCubes)
        {
            var result = new Dictionary<string,string>();
            foreach (var outputCube in outputCubes)
            {
                result.Add(outputCube.ValueName, outputCube.Value);
            }
            return result;
        }
    }
}
