using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMEX;
using BitMexBot;
using Newtonsoft.Json;
using System.Reflection;

namespace BitMexBot
{
    public class orderBook
    {
        public string URL = "https://www.bitmex.com/api/bitcoincharts/XBTUSD/orderbook";

        public string CurrencyType;
   
        public List<Dictionary<double,int>> bids = new List<Dictionary<double,int>>();
        public List<Dictionary<double,int>> asks = new List<Dictionary<double,int>>();

        public double GetOrderBook(Account A, string CurrencyType)
        {
            double AveragePrice = 0.0f;
            int count = 0;

            try
            {
               BitMEXApi bitmex = new BitMEXApi(A.APIKey, A.SecretKey);

               string trades = bitmex.QueryNonv1("GET", URL, null, false, false);
               trades = JsonHelper.FormatJson(trades);
               //trades = trades.Remove(0, 1);
               trades = trades.Remove(trades.IndexOf("\"asks\":"));
               trades = trades + "}";
               Console.WriteLine(trades);
               //string friendlyString = OrderBook.Remove(OrderBook.IndexOf("\"bids\":"), 7);
               //friendlyString = friendlyString.Remove(0, 1);

               bids = JsonConvert.DeserializeObject<List<Dictionary<double, int>>>(trades);
               //Console.WriteLine(bids[0]);
                
            }
            catch
            {
                Console.WriteLine("End of book or error. If shown orders, no error");
                return AveragePrice / count;
            }

            return AveragePrice / count;
        }
    }
}
