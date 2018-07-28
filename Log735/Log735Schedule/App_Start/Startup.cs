using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Log735Schedule.Startup))]

namespace Log735Schedule
{
    public partial class Startup
    {

        public void ConfigurationSignalR(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
          //  app.MapSignalR();
        }
    }
}
