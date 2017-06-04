using Nancy;

namespace WebService.WebSite
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get[@"/"] = parameters =>
            {
                return Response.AsFile("home/index.html", "text/html");
            };
        }
    }
}
