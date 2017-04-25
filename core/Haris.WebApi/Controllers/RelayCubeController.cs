using System.Collections.Generic;
using System.Web.Http;
using Haris.Core;
using Haris.Core.Cubes;
using Haris.Core.Services;
using Haris.DataModel;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories;
using Haris.DataModel.Repositories.Implementation;
using SimpleInjector.Integration.WebApi;

namespace Haris.WebApi.Controllers
{
    [RoutePrefix("api/cube/relay")]
    public class RelayCubeController : ApiController
    {
        private readonly CubeRepository _cubeRepository;

        public RelayCubeController()
        {
            _cubeRepository = new CubeRepository(new HarisDbContext());
        }
        
        [Route("{address}/TurnOn")]
        [HttpGet]
        public IHttpActionResult TurnOn(string address)
        {
            EngineService service = (EngineService)AppCoreBootstrapper.Container.GetInstance(typeof(EngineService)); ;
            Cube entityCube = service._cubeRepository.GetCube(address);
            RelayCube displayCube = new RelayCube(entityCube, _cubeRepository, service);

            displayCube.TurnOn();
            return Ok();
        }

        [Route("{address}/TurnOff")]
        [HttpGet]
        public IHttpActionResult TurnOffBacklight(string address)
        {
            EngineService service = (EngineService) AppCoreBootstrapper.Container.GetInstance(typeof(EngineService)); ;
            Cube entityCube = service._cubeRepository.GetCube(address);
            RelayCube displayCube = new RelayCube(entityCube, _cubeRepository, service);
           
            displayCube.TurnOff();
            return Ok();
        }

        [Route("{address}")]
        [HttpPost]
        public IHttpActionResult SetDisplay([FromUri]string address, [FromBody] SetDisplayRequest request)
        {
            EngineService service = (EngineService)AppCoreBootstrapper.Container.GetInstance(typeof(EngineService)); ;
            Cube entityCube = service._cubeRepository.GetCube(address);
            DisplayCube displayCube = new DisplayCube(entityCube, _cubeRepository, service);

            displayCube.SetDisplayText(request.Row, request.Content);
            return Ok();
        }
    }
}
