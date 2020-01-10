using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMEX;
using Newtonsoft.Json;


namespace BitMexBot
{
    public class Account
    {
        public static string BaseURI = "https://www.bitmex.com/api/v1";

        public int account { get; set; }
        public string currency { get; set; }
        public long riskLimit { get; set; }
        public string prevState { get; set; }
        public string state { get; set; }
        public string action { get; set; }
        public int amount { get; set; }
        public double pendingCredit { get; set; }
        public double pendingDebit { get; set; }
        public double confirmedDebit { get; set; }
        public double prevRealisedPnl { get; set; }
        public double prevUnrealisedPnl { get; set; }
        public double grossComm { get; set; }
        public double grossOpenCost { get; set; }
        public double grossOpenPremium { get; set; }
        public double grossExecCost { get; set; }
        public double grossMarkValue { get; set; }
        public double riskValue { get; set; }
        public double taxableMargin { get; set; }
        public double initMargin { get; set; }
        public double maintMargin { get; set; }
        public double sessionMargin { get; set; }
        public double targetExcessMargin { get; set; }
        public double varMargin { get; set; }
        public double realisedPnl { get; set; }
        public double unrealisedPnl { get; set; }
        public double indicativeTax { get; set; }
        public double unrealisedProfit { get; set; }
        public object syntheticMargin { get; set; }
        public int walletBalance { get; set; }
        public int marginBalance { get; set; }
        public double marginBalancePcnt { get; set; }
        public double marginLeverage { get; set; }
        public double marginUsedPcnt { get; set; }
        public double excessMargin { get; set; }
        public double excessMarginPcnt { get; set; }
        public double availableMargin { get; set; }
        public double withdrawableMargin { get; set; }
        public DateTime timestamp { get; set; }
        public int grossLastValue { get; set; }
        public object commission { get; set; }
        
        public BitMEXApi bitmex = new BitMEXApi(); //bitmex API
        public int TickCount = 1000;

        public string APIKey;
        public string SecretKey;
        public string Email;
        public string ReadableName;

        public bool AutoTrading;

        public List<Transaction> Transactions = new List<Transaction>();

        public int AccountId;

        public static string WalletBalanceURL = "/user/margin?currency=XBt";// + //symbol; //field returned Available Balance
 
        public Account GetAccount()
        {
            return this;
        }

        public bool FillAPIKeys(string textFile)
        {
            string text = System.IO.File.ReadAllText(textFile);

            string[] Keypairs = text.Split(',');

            foreach (string Line in Keypairs) //cmd line, use it on email
            {
                APIKey = Keypairs[0];
                SecretKey = Keypairs[1];
                Email = Keypairs[2];
                ReadableName = Keypairs[3];       
            }

            return true;
        }

        public string GetWallet(string symbol) { 

            BitMEXApi bitmex = new BitMEXApi(this.APIKey, this.SecretKey);
            var param = new Dictionary<string, string>();
            param["symbol"] = symbol; 
            return bitmex.Query("GET", "/user/wallet", param, true);
        }

        public double GetWalletBalance()
        {
            double Satoshi = 0.0f;
            BitMEXApi bitmex = new BitMEXApi(this.APIKey, this.SecretKey);
            string Result = bitmex.Query("GET", WalletBalanceURL, null, true, false);
            Console.WriteLine(Result);
            var data = JsonConvert.DeserializeObject<Account>(Result);
            Satoshi = data.walletBalance;
            AccountId = data.account;
            return Satoshi;
        }
    }
}















