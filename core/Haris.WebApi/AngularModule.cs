using Nancy;

namespace Haris.WebApi
{
    public class AngularModule : NancyModule
    {
        public AngularModule()
        {
            Get["/"] = parameters => View["Content/www/index.html", new {Title = " Strona główna "}];
        }
    }
}
