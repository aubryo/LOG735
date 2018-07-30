﻿using Microsoft.AspNet.SignalR.Client;
using PrivateRoomDomain;
using PrivateRoomDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Log735Schedule
{
    public static class CommunicationHandler
    {
        public static List<string> ListHubUrl = new List<string>{ "http://localhost:8089" }; //"http://localhost:8088"
        private static IHubProxy CurrentPrivateRoomHubProxy;
        private static HubConnection currentPrivateRoomHub;
     
        private static void HandleNotify(Currency currency)
        {
            Console.WriteLine("Currency " + currency.CurrencySign + ", Rate = " + currency.USDValue);
        }

        public static string GlobalInvokeFindRightHub(string method, string args)
        {
            foreach (var hubUrl in ListHubUrl)
            {
                try
                {
                    var hubConnection = new HubConnection(hubUrl);
                    IHubProxy privateRoomHubProxy = hubConnection.CreateHubProxy("PrivateRoomHub");
                    hubConnection.Start();
                    Execute(hubConnection);
                    var existingRoom = privateRoomHubProxy.Invoke<bool>(method,args).Result;

                    if (existingRoom)
                    {
                        return hubUrl;
                    }
                   
                    hubConnection.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return "no";
        }

        public static PrivateRooms GetPrivateRoom(string roomName)
        {
            var res = CurrentPrivateRoomHubProxy.Invoke<PrivateRooms>("GetPrivateRoom", roomName).Result;
 
            return res;
        }

        public static void SetCurrentHubConnection(string hubUrl)
        {
            var currentPrivateRoomHub = new HubConnection(hubUrl);
            IHubProxy CurrentPrivateRoomHubProxy = currentPrivateRoomHub.CreateHubProxy("PrivateRoomHub");
            currentPrivateRoomHub.Start();
            Execute(currentPrivateRoomHub);
            
        }

        private static bool AddCourse(Courses course)
        {
           var r =CurrentPrivateRoomHubProxy.Invoke<bool>("AddCourse",course).Result;

            return r;


        }
        public static void CloseConnection()
        {
            currentPrivateRoomHub.Dispose();

        }
        public static PrivateRooms CreatePrivateRoom(PrivateRooms privateRoom)
        {
            var r = CurrentPrivateRoomHubProxy.Invoke<PrivateRooms>("CreatePrivateRoom", privateRoom).Result;

            return r;

        }
        
        public static void SetAvailableHubConnection()
        {
            var numberConnectList = new List<KeyValuePair<string, int>>(); 
            foreach (var hubUrl in ListHubUrl)
            {
                try
                {
                    var hubConnection = new HubConnection(hubUrl);
                    IHubProxy privateRoomHubProxy = hubConnection.CreateHubProxy("PrivateRoomHub");
                    hubConnection.Start();
                    Execute(hubConnection);
                    var numberClient = privateRoomHubProxy.Invoke<int>("GetNumberClientOnline").Result;
                    numberConnectList.Add(new KeyValuePair<string, int>(hubUrl, numberClient));
                    //hubConnection.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            var AvailableHub = numberConnectList.OrderBy(v => v.Value).First();
            ConnectToHub(AvailableHub.Key);
     

        }
        private static void ConnectToHub(string hubUrl)
        {
            try
            {
                currentPrivateRoomHub = new HubConnection(hubUrl);
                CurrentPrivateRoomHubProxy = currentPrivateRoomHub.CreateHubProxy("PrivateRoomHub");
                currentPrivateRoomHub.Start();
                Execute(currentPrivateRoomHub);
            }
            catch (Exception e)
            {

            }

        }



        private static void Execute(HubConnection hubConnection)
        {
            hubConnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());

                    return;
                }
                else
                {
                    Console.WriteLine("Connected to Server.The ConnectionID is:" + hubConnection.ConnectionId);

                }

            }).Wait();
        }
        public static string ExecuteMethod(string method, string args, string serverUri, string hubName)
        {
            var hubConnection = new HubConnection("http://localhost:8089");
            IHubProxy currencyExchangeHubProxy = hubConnection.CreateHubProxy("CurrencyExchangeHub");

            // This line is necessary to subscribe for broadcasting messages
            currencyExchangeHubProxy.On<Currency>("NotifyChange", HandleNotify);

            // Start the connection
            hubConnection.Start().Wait();

            var result = currencyExchangeHubProxy.Invoke<string>(method).Result;

            return result;
        }

    }
}