using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleBookingSystem.Web.Startup))]
namespace SampleBookingSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
