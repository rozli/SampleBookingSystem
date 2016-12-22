using System.Web.Mvc;
using System.Web.Routing;

namespace SampleBookingSystem.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "News",
                url: "News/{page}",
                defaults: new { controller = "Home", action = "News", page = UrlParameter.Optional },
                constraints: new { page = @"[0-9]*" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
