using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Haris.DataModel;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories;
using Haris.DataModel.Repositories.Implementation;

namespace Haris.WebApi.Controllers
{
    [RoutePrefix("api/cube/temperature")]
    public class TemperatureCubeController : ApiController
    {
        private readonly ICubeRepository _cubeRepository;

        public TemperatureCubeController()
        {
            _cubeRepository = new CubeRepository(new HarisDbContext());
        }

        // GET api/values/5 
        [Route("{address}")]
        public string Get(string address)
        {
            return _cubeRepository.GetCube(address).OutputCubes.FirstOrDefault(x => x.ValueName == "Temp").Value;
        }

    }
}
