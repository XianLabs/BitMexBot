using BitMEX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    public class Order
    {
        public string orderID { get; set; }
        public string clOrdID { get; set; }
        public string clOrdLinkID { get; set; }
        public string account { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public object simpleOrderQty { get; set; }
        public int orderQty { get; set; }
        public double price { get; set; }
        public object displayQty { get; set; }
        public object stopPx { get; set; }
        public object pegOffsetValue { get; set; }
        public string pegPriceType { get; set; }
        public string currency { get; set; }
        public string settlCurrency { get; set; }
        public string ordType { get; set; }
        public string timeInForce { get; set; }
        public string execInst { get; set; }
        public string contingencyType { get; set; }
        public string exDestination { get; set; }
        public string ordStatus { get; set; }
        public string triggered { get; set; }
        public bool workingIndicator { get; set; }
        public string ordRejReason { get; set; }
        public object simpleLeavesQty { get; set; }
        public int leavesQty { get; set; }
        public object simpleCumQty { get; set; }
        public int cumQty { get; set; }
        public string avgPx { get; set; }
        public string multiLegReportingType { get; set; }
        public string text { get; set; }
        public DateTime transactTime { get; set; }
        public DateTime timestamp { get; set; }


        public bool GetOrders(Account A, string Symbol)
        {
            BitMEXApi bitmex = new BitMEXApi(A.APIKey, A.SecretKey);
            string orders = bitmex.GetOrders(Symbol);
            orders = orders.Remove(0, 1);

            string[] orderlist = orders.Split('}');

            foreach (string shit in orderlist)
            {
                string oneOrder = shit;
                oneOrder += "}";
                if (oneOrder[0] == ',')
                    oneOrder = oneOrder.Remove(0, 1);

                //Console.WriteLine(oneOrder);
                try
                {
                    var data = JsonConvert.DeserializeObject<Order>(oneOrder);
                    Console.WriteLine(data.orderID + " " + data.symbol + " " + data.side + " " + data.price);
                }
                catch
                {
                    Console.WriteLine("Finished parsing user's orders.");
                    return false;
                }
            }

            return true;
        }
    }
}
