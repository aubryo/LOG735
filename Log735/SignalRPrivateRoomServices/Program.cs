using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using PrivateRoomDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SignalRPrivateRoomServices
{
 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static class Program
        {
            private static readonly List<ServiceBase> _servicesToRun = new List<ServiceBase>();

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            static void Main(string[] args)
            {

            ServiceBase[] ServicesToRun = new ServiceBase[] { new SignalRPrivateRoomServices(args) };
            ServiceBase.Run(ServicesToRun);
            //string url = "http://localhost:8089";
            //using (WebApp.Start(url))
            //{
            //    Console.WriteLine("Server running on {0}", url);
            //    while (true)
            //    {

            //        string key = Console.ReadLine();
            //        if (key == null)
            //            continue;
            //        if (key.ToUpper() == "W")
            //        {
            //            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            //            hubContext.Clients.All.addMessage("server", "ServerMessage");
            //            Console.WriteLine("Server Sending addMessage\n");
            //        }
            //        if (key.ToUpper() == "E")
            //        {
            //            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            //            hubContext.Clients.All.heartbeat();
            //            Console.WriteLine("Server Sending heartbeat\n");
            //        }
            //        if (key.ToUpper() == "R")
            //        {
            //            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();

            //            var vv = new HelloModel { Age = 37, Molly = "pushed direct from Server " };

            //            hubContext.Clients.All.sendHelloObject(vv);
            //            Console.WriteLine("Server Sending sendHelloObject\n");
            //        }
            //        if (key.ToUpper() == "C")
            //        {
            //            break;
            //        }
            //    }

            //    Console.ReadLine();
            //}
        }
        }
    
}
