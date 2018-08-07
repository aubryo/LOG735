using Microsoft.AspNet.SignalR;
using PrivateRoomDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRPrivateRoomServices
{
    public class MyHub : Hub
    {
        public void AddMessage(string name, string message)
        {
            Console.WriteLine("Hub AddMessage {0} {1}\n", name, message);
            Clients.All.addMessage(name, message);
        }

        public void Heartbeat()
        {
            Console.WriteLine("Hub Heartbeatfffff\n");
           
            Clients.All.heartbeat();
        }

        public void SendHelloObject(HelloModel hello)
        {
            Console.WriteLine("Hub hello {0} {1}\n", hello.Molly, hello.Age);
            Clients.All.sendHelloObject(hello);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Hub OnConnected {0}\n", Context.ConnectionId);
            return (base.OnConnected());
        }

       

       
    }
}
