using System.Collections.Generic;
using System.Web.Http;

namespace Haris.WebApi.Controllers
{
    [RoutePrefix("api/cube/temperature")]
    public class TemperatureCubeController : ApiController
    {
        // GET api/values 
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        [Route("{address}")]
        public string Get(string address)
        {
            return address;
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
