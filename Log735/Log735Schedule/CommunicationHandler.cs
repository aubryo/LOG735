//using Microsoft.AspNet.SignalR.Client;
//using PrivateRoomDomain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Log735Schedule
//{
//    public static class CommunicationHandler
//    {
//        public static string ExecuteMethod(string method, string args, string serverUri, string hubName)
//        {
//            var hubConnection = new HubConnection("http://localhost:7716");
//            IHubProxy currencyExchangeHubProxy = hubConnection.CreateHubProxy("CurrencyExchangeHub");

//            // This line is necessary to subscribe for broadcasting messages
//            currencyExchangeHubProxy.On<Currency>("NotifyChange", HandleNotify);

//            // Start the connection
//            hubConnection.Start().Wait();

//            var result = currencyExchangeHubProxy.Invoke<string>(method).Result;

//            return result;
//        }

//        private static void HandleNotify(Currency currency)
//        {
//            Console.WriteLine("Currency " + currency.CurrencySign + ", Rate = " + currency.USDValue);
//        }
//    }
//}