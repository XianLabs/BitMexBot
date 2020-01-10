using BitMEX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BitMexBot.Trades
{
    public class RecentTrades
    {
        public DateTime timestamp { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public int size { get; set; }
        public double price { get; set; }
        public string tickDirection { get; set; }
        public string trdMatchID { get; set; }
        public uint grossValue { get; set; }
        public double homeNotional { get; set; }
        public double foreignNotional { get; set; }


        public bool GetRecentTrades(Account A, string Symbol)
        {
            BitMEXApi bitmex = new BitMEXApi(A.APIKey, A.SecretKey);
            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("count", "100");
            Params.Add("reverse", "true");
            Params.Add("symbol", Symbol);

            int TotalBuyQuantity = 0;
            int TotalSellQuantity = 0;

            try
            {
                string trades = bitmex.Query("GET", "/trade", Params, true, false);
                trades = JsonHelper.FormatJson(trades);
                trades = trades.Remove(0, 1);

                int BuyQuantities = 0;
                int SellQuantities = 0;

                string[] tradeList = trades.Split('}');

                foreach (string T in tradeList)
                {
                    string FormatTrade = T;

                    if (FormatTrade.Contains(']'))
                    {
                        FormatTrade.Remove(']');
                    }

                    FormatTrade = T + '}';

                    if (FormatTrade[0] == ',')
                    {
                        FormatTrade = FormatTrade.Remove(0, 1);
                    }

                    RecentTrades data = JsonConvert.DeserializeObject<RecentTrades>(FormatTrade);
                    Console.WriteLine("Price: " + data.price + ", " + "Quantity: " + data.size + ", " + "Symbol: " + data.symbol +  ", Buy/Sell: " + data.side + ", Timestamp: " + data.timestamp);

                    if(data.side == "Buy")
                    {
                        BuyQuantities += 1;
                    }
                    else if(data.side == "Sell")
                    {
                        SellQuantities += 1;
                    }           
                }

                Console.WriteLine("Total orderbook Buys: " + BuyQuantities + "(" + Symbol + ")" + "\nTotal Orderbook Sells: " + SellQuantities + "(" + Symbol + ")");  
            }
            catch
            {
                Console.WriteLine("Finished Traversing Recent Trades.");
            }

            Console.WriteLine("Buys: {0}", TotalBuyQuantity);
            Console.WriteLine("Sells: {0}", TotalSellQuantity);

            return true;
        }
    }
}
