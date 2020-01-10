using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BitMEX;
using Newtonsoft.Json;

namespace BitMexBot
{
    public class instrument
    {

        public string symbol { get; set; } //weird symbol, xbt seems to be ADA50 or something
        public string rootSymbol { get; set; } //ADA, XBTUSD, etc
        public string state { get; set; }
        public string typ { get; set; }
        public DateTime listing { get; set; }
        public DateTime front { get; set; }
        public DateTime expiry { get; set; }
        public DateTime settle { get; set; }
        public string inverseLeg { get; set; }
        public string sellLeg { get; set; }
        public string buyLeg { get; set; }
        public double optionStrikePcnt { get; set; }
        public double optionStrikeRound { get; set; }
        public double optionStrikePrice { get; set; }
        public double optionMultiplier { get; set; }
        public string positionCurrency { get; set; }
        public string underlying { get; set; }
        public string quoteCurrency { get; set; }
        public string underlyingSymbol { get; set; }
        public string reference { get; set; }
        public string referenceSymbol { get; set; }
        public DateTime calcInterval { get; set; }
        public DateTime publishInterval { get; set; }
        public DateTime publishTime { get; set; }
        public int maxOrderQty { get; set; }
        public double maxPrice { get; set; }
        public int lotSize { get; set; }
        public string tickSize { get; set; }
        public double multiplier { get; set; }
        public string settlCurrency { get; set; }
        public int underlyingToPositionMultiplier { get; set; }
        public int underlyingToSettleMultiplier { get; set; }
        public int quoteToSettleMultiplier { get; set; }
        public bool isQuanto { get; set; }
        public bool isInverse { get; set; }
        public double initMargin { get; set; }
        public double maintMargin { get; set; }
        public double riskLimit { get; set; }
        public double riskStep { get; set; }
        public int limit { get; set; }
        public bool capped { get; set; }
        public bool taxed { get; set; }
        public bool deleverage { get; set; }
        public double makerFee { get; set; }
        public double takerFee { get; set; }
        public double settlementFee { get; set; }
        public double insuranceFee { get; set; }
        public string fundingBaseSymbol { get; set; }
        public string fundingQuoteSymbol { get; set; }
        public string fundingPremiumSymbol { get; set; }
        public DateTime fundingTimestamp { get; set; }
        public DateTime fundingInterval { get; set; }
        public double fundingRate { get; set; }
        public double indicativeFundingRate { get; set; }
        public DateTime rebalanceTimestamp { get; set; }
        public DateTime rebalanceInterval { get; set; }
        public DateTime openingTimestamp { get; set; }
        public DateTime closingTimestamp { get; set; }
        public DateTime sessionInterval { get; set; }
        public double prevClosePrice { get; set; }
        public double limitDownPrice { get; set; }
        public double limitUpPrice { get; set; }
        public double bankruptLimitDownPrice { get; set; }
        public double bankruptLimitUpPrice { get; set; }
        public double prevTotalVolume { get; set; }
        public double totalVolume { get; set; }
        public double volume { get; set; }
        public double volume24h { get; set; }
        public double prevTotalTurnover { get; set; }
        public double totalTurnover { get; set; }
        public double turnover { get; set; }
        public double turnover24h { get; set; }
        public double homeNotional24h { get; set; }
        public double foreignNotional24h { get; set; }
        public double prevPrice24h { get; set; }
        public double vwap { get; set; }
        public double highPrice { get; set; }
        public double lowPrice { get; set; }
        public double lastPrice { get; set; }
        public double lastPriceProtected { get; set; }
        public string lastTickDirection { get; set; }
        public double lastChangePcnt { get; set; }
        public double bidPrice { get; set; }
        public double midPrice { get; set; }
        public double askPrice { get; set; }
        public double impactBidPrice { get; set; }
        public double impactMidPrice { get; set; }
        public double impactAskPrice { get; set; }
        public bool hasLiquidity { get; set; }
        public int openInterest { get; set; }
        public double openValue { get; set; }
        public string fairMethod { get; set; }
        public double fairBasisRate { get; set; }
        public double fairBasis { get; set; }
        public double fairPrice { get; set; }
        public string markMethod { get; set; }
        public double markPrice { get; set; }
        public double indicativeTaxRate { get; set; }
        public double indicativeSettlePrice { get; set; }
        public double optionUnderlyingPrice { get; set; }
        public double settledPrice { get; set; }
        public DateTime timestamp { get; set; }
    
        public double GetInstrumentPrice(Account A, string Symbol)
        {
            string URL = "/api/v1/instrument/active";

            BitMEXApi Api = new BitMEXApi(A.APIKey, A.SecretKey);

            string Result =  Api.QueryNonv1("GET", URL, null, true, true);
            Result = JsonHelper.FormatJson(Result);
            Result = Result.Remove(0, 1);


            string[] InstrumentList = Result.Split('}');
  
            foreach(string s in InstrumentList)
            {
                string str1 = s.Remove(0, 1);
                string str2 = str1.Insert(str1.Length-1, "}");
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    instrument I = new instrument();          
                    I = JsonConvert.DeserializeObject<instrument>(str2, settings);

                    if (I.symbol == Symbol)
                    {
                        Console.WriteLine(I.markPrice);
                        return I.markPrice;
                    }
                }
                catch(Exception E)
                {
                    Console.WriteLine("Error: " + E.Message + " " + E.Source);
                }
            }
            return 0.0f;   
        }
            
    }
}
