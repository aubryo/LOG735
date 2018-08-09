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
            var service = new SignalRPrivateRoomServices(args);
            ServiceBase[] ServicesToRun = new ServiceBase[] { service };
            ServiceBase.Run(ServicesToRun);

            string url = "http://localhost:8089";//18.191.13.220
            using (WebApp.Start(url))
            {



                while (true)
                {

                }
            }


        }






    }

}
