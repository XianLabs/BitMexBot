using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    public class Stats
    {

        public static string URL = "stats/historyUSD";
        
        public string rootSymbol { get; set; }
        public string currency { get; set; }
        public int turnover24h { get; set; }
        public object turnover30d { get; set; }
        public object turnover365d { get; set; }
        public object turnover { get; set; }

        Stats GetStats(string CurrencyType)
        {
            Stats s = new Stats();
            return s;
        }
    }
}
