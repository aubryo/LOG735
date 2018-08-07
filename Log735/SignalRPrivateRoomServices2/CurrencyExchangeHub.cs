//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;
//using Microsoft.Owin;
//using PrivateRoomDomain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SignalRPrivateRoomServices
//{
//    public class CurrencyExchangeHub : Hub
//    {
//        private readonly SignalRPrivateRoomServices1 _currencyExchangeHub;

//        public CurrencyExchangeHub() :
//            this(SignalRPrivateRoomServices1.Instance)
//        {

//        }

//        public CurrencyExchangeHub(SignalRPrivateRoomServices1 currencyExchange)
//        {
//            _currencyExchangeHub = currencyExchange;
//        }

//        public IEnumerable<Currency> GetAllCurrencies()
//        {
//            return _currencyExchangeHub.GetAllCurrencies();
//        }

//        public string GetMarketState()
//        {
//            return _currencyExchangeHub.MarketState.ToString();
//        }

//        public bool OpenMarket()
//        {
//            _currencyExchangeHub.OpenMarket();
//            return true;
//        }

//        public bool CloseMarket()
//        {
//            _currencyExchangeHub.CloseMarket();
//            return true;
//        }

//        public bool Reset()
//        {
//            _currencyExchangeHub.Reset();
//            return true;
//        }
   
//    }
//}
