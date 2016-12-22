using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SampleBookingSystem.Common.Mappings;
using SampleBookingSystem.Data;
using SampleBookingSystem.Data.Migrations;
using SampleBookingSystem.Web.App_Start;

namespace SampleBookingSystem.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Set the database intializer which is run once during application start
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SampleBookingSystemDbContext, Configuration>());

            AutofacCofig.RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
