using Nancy;

namespace Haris.WebApi
{
    public class AngularModule : NancyModule
    {
        public AngularModule()
        {
            Get["/"] = parameters => View["index.html", new {Title = " Strona główna "}];
        }
    }
}
