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
    [RoutePrefix("api/cube/display")]
    public class DisplayCubeController : ApiController
    {
        private readonly CubeRepository _cubeRepository;

        public DisplayCubeController()
        {
            _cubeRepository = new CubeRepository(new HarisDbContext());
        }
        
        [Route("{address}/TurnOnBacklight")]
        [HttpGet]
        public IHttpActionResult TurnOnBacklight(string address)
        {
            EngineService service = (EngineService)AppCoreBootstrapper.Container.GetInstance(typeof(EngineService)); ;
            Cube entityCube = service._cubeRepository.GetCube(address);
            DisplayCube displayCube = new DisplayCube(entityCube, _cubeRepository, service);

            displayCube.TurnOnBacklight();
            return Ok();
        }

        [Route("{address}/TurnOffBacklight")]
        [HttpGet]
        public IHttpActionResult TurnOffBacklight(string address)
        {
            EngineService service = (EngineService) AppCoreBootstrapper.Container.GetInstance(typeof(EngineService)); ;
            Cube entityCube = service._cubeRepository.GetCube(address);
            DisplayCube displayCube = new DisplayCube(entityCube, _cubeRepository, service);
           
            displayCube.TurnOffBacklight();
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
