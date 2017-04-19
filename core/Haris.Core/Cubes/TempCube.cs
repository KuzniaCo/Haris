using System;
using System.Linq;
using Haris.Core.Services;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories.Implementation;
using Newtonsoft.Json;
using RestSharp;

namespace Haris.Core.Cubes
{
    public sealed class TempCube : BaseCube
    {
        public TempCube(Cube cubeEntity, CubeRepository cubeRepository, EngineService engineService)
            : base( cubeEntity, cubeRepository, engineService)
        {
        }

        private String[] messageItems;

        public override void ProcessMessage(string message)
        {
            messageItems = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            OnReceivedTemp(messageItems[1], message);
        }

        public void OnReceivedTemp(string value, string message)
        {
            var temp = _cubeEntity.OutputCubes.FirstOrDefault(x => x.ValueName == "Temp");
            var date = _cubeEntity.OutputCubes.FirstOrDefault(x => x.ValueName == "Date");
            date.Value = DateTime.Now.ToString("G");
            temp.Value = messageItems[1];
            _cubeRepository.SaveChanges();
            //foreach (var webHook in _cubeEntity.WebHooks)
            //{
            //    var client = new RestClient();
            //    var request = new RestRequest(webHook.Url, Method.POST);
            //    var body = JsonConvert.SerializeObject(new {Temp = temp.Value, Date = date.Value});
            //    request.AddParameter("application/json", body, ParameterType.RequestBody);
            //    request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method


            //    IRestResponse response = client.Execute(request);
            //    var content = response.Content; // raw content as string                
            //}
        }
    }
}
