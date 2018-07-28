using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Log735Schedule.Startup))]
namespace Log735Schedule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigurationSignalR(app);
            ConfigureAuth(app);
        }
    }
}
