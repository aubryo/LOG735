using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Log735Schedule.SignalR
{
    public class PrivateRoom : Hub
    {
        //public void Send(string name, string message)
        //{
        //    // Call the broadcastMessage method to update clients.
        //    var state = CommunicationHandler.ExecuteMethod("GetMarketState",
        //                    "", IPAddress.Any.ToString(), "CurrencyExchangeHub");
        //    Console.WriteLine("Market State is " + state);

        //    if (state == "Closed")
        //    {
        //        var returnCode = CommunicationHandler.ExecuteMethod
        //            ("OpenMarket", "", IPAddress.Any.ToString(), "CurrencyExchangeHub");
             
        //        Console.WriteLine("Market State is Open");
        //    }

            
        //    Clients.All.broadcastMessage("Market State is " + state, message);
        //}
    }
}