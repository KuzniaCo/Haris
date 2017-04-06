using Nancy;

namespace Haris.WebApi.Controllers
{
    public class AngularModule : NancyModule
    {
        public AngularModule()
        {
            Get["/"] = parameters => View["Index", new {Title = " Strona główna "}];
        }
    }
}
