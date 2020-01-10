using BitMEX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Threading;
using Newtonsoft.Json;


namespace BitMexBot
{
    public class Transaction //uses json dict
    {
        public enum OrderTypes
        {
            Limit,
            Market,
            StopLoss
        }

        public enum Perpetual
        {
            XBT,
            ETHUSD
        }


        public const string url = "/order";

        public string orderID { get; set; }
        public string clOrdID { get; set; }
        public string clOrdLinkID { get; set; }
        public int account { get; set; }
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
        public object avgPx { get; set; }
        public string multiLegReportingType { get; set; }
        public string text { get; set; }
        public DateTime transactTime { get; set; }
        public DateTime timestamp { get; set; }
        //result from buy/sells

        public bool CancelTransaction(Account A, string TransactionID) //we can bulk delete by adding more params.
        {
            BitMEXApi bitmex = new BitMEXApi(A.APIKey, A.SecretKey);

            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add(orderID, TransactionID);
            Params.Add("text", "Cancel from www.bitmex.com");

            try
            {
                string trades = bitmex.Query("DELETE", Transaction.url, Params, true, true);
                Console.WriteLine(trades);

                if(trades.Contains("error"))
                {
                    Console.WriteLine("Error occured while deleting transaction.");
                }
            }
            catch
            {
                return false;
            }

            Thread.Sleep(1000);

            try
            {
                string trades = bitmex.Query("DELETE", Transaction.url, Params, true, true);
                Console.WriteLine(trades);

                if (trades.Contains("error"))
                {
                    Console.WriteLine("Error occured while deleting transaction.");
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

         public bool CreateTransaction(Account A, bool execInst, uint orderQty, OrderTypes OrdType, Decimal price, string Symbol, string BuyOrSell)
        {
            BitMEXApi bitmex = new BitMEXApi(A.APIKey, A.SecretKey);

            Dictionary<string, string> Params = new Dictionary<string, string>();

            if (OrdType == Transaction.OrderTypes.Market)
            {
                Params.Add("ordType", "Market");
            }
            else if (OrdType == Transaction.OrderTypes.Limit)
            {
                Params.Add("ordType", "Limit");
            }

            string Side = BuyOrSell;

            //{"ordType":"Limit","price":8650,"orderQty":24,"side":"Buy","execInst":"ParticipateDoNotInitiate","symbol":"XBTUSD","text":"Submission from www.bitmex.com"}
            uint RoundedPrice = (uint)price;

            Params.Add("price", RoundedPrice.ToString().Replace("\"", ""));
            Params.Add("orderQty", orderQty.ToString().Replace("\"", ""));
            Params.Add("side", BuyOrSell);
            Params.Add("execInst", "ParticipateDoNotInitiate"); //change this to ability to market sell
            Params.Add("symbol", Symbol);
            Params.Add("text", "Submission from www.bitmex.com");
        
            try
            {
                string trades = bitmex.Query("POST", Transaction.url, Params, true, true);
                Transaction data = JsonConvert.DeserializeObject<Transaction>(trades);

                Console.WriteLine(data.account + " " +  data.symbol +" " +  " " + data.cumQty);
            }
            catch
            {
                Console.WriteLine("Error while selling.");
                return false;
            }

            return true;
            }
    }
}

/*




/* Example 2, Funding Fees
BitMEX has a type of derivative contract called a Perpetual Contract. Buyers and sellers of perpetual contacts pay and receive funding fees periodically throughout the trading day. To learn more, please read the Perpetual Contracts Guide.

John is trading XBTUSD, which is a perpetual contract. Every 8 hours, there is a funding fee. The funding fee is currently 1%, and is paid from buyers to sellers.

John is currently long 100 XBT worth of XBTUSD. The position has no realised PNL. It is funding time and John must pay 1 XBT because he is long XBTUSD. After the funding fee has been paid, John’s realised PNL is now -1 XBT.

If John had been short 100 XBT worth of XBTUSD instead, he would have received 1 XBT. His realised profit would then be 1 XBT instead of -1 XBT.

Example 3, Trading Fees
All trading fees are accounted for through realised pnl.

John bought XBTUSD. The market has not moved. His unrealised PNL is 0, but his realised PNL is negative. John’s realised PNL is negative because he paid a taker fee when he bought XBTUSD.

If John had placed a passive limit order, he would be classified as a maker once the order was executed. As a maker, John would have been paid a rebate on the trade. In that situation, his unrealised PNL would be 0 and realised PNL positive.

/*/