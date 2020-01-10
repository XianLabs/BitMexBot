using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMEX;

namespace BitMexBot
{
    public class User
    {
        public class OrderBookBinning
        {
            public int XBTUSD { get; set; }
        }

        public class Preferences
        {
            public bool alertOnLiquidations { get; set; }
            public bool animationsEnabled { get; set; }
            public DateTime announcementsLastSeen { get; set; }
            public string colorTheme { get; set; }
            public List<string> hideConfirmDialogs { get; set; }
            public bool hideConnectionModal { get; set; }
            public bool hideFromLeaderboard { get; set; }
            public bool hideNameFromLeaderboard { get; set; }
            public string locale { get; set; }
            public List<string> msgsSeen { get; set; }
            public OrderBookBinning orderBookBinning { get; set; }
            public string orderBookType { get; set; }
            public bool orderClearImmediate { get; set; }
            public bool showLocaleNumbers { get; set; }
            public bool strictIPCheck { get; set; }
            public bool strictTimeout { get; set; }
            public bool tickerPinned { get; set; }
            public string tradeLayout { get; set; }
        }

        public class RestrictedEngineFields
        {
        }

        public class Role
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public DateTime created { get; set; }
            public DateTime modified { get; set; }
        }
        public int id { get; set; }
        public object ownerId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public object phone { get; set; }
        public DateTime created { get; set; }
        public DateTime lastUpdated { get; set; }
        public Preferences preferences { get; set; }
        public RestrictedEngineFields restrictedEngineFields { get; set; }
        public string TFAEnabled { get; set; }
        public string affiliateID { get; set; }
        public object pgpPubKey { get; set; }
        public string country { get; set; }
        public string geoipCountry { get; set; }
        public string geoipRegion { get; set; }
        public string typ { get; set; }
        public bool isRestricted { get; set; }
        public List<Role> roles { get; set; }
     

        public bool requestWithdrawl(Account A) //user/requestWithdrawl
        {
            return true;
        }

        public bool Withdrawl(Account A) //user/Withdrawl
        {
            return true;
        }

        public bool CancelWithdrawl(Account A, string Token) //user/cancelWithdrawl
        {
            return true;
        }

        public string GetDepositAddress(Account A) //user/depositAddress GET
        {
            string data = "XBt";
            return "hi";
        }

        public bool SendXbtToAccount(Account A, double Amount) //user/requestWithdrawl - sends to email on record
        {
            return true;
        }
        
        public bool Logout(Account A) //user/logout
        {
            return true;
        }

        public double GetAffiliateIncome(Account A) //get income gained by affiliates
        {
            double Income = 0.0f;
            return Income;
        }

        public bool GetWalletHistory(Account A)
        {

            return true;
        }

    }


}
