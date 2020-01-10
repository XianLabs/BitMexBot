using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    class amend : Order
    {
        public bool AmendActiveOrder(string OrderID, string OrderType, double price)
        {
            //{"symbol":"ETHUSD","price":170.85,"ordType":"Limit","execInst":"Close","text":"Position Close from www.bitmex.com"}
            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("symbol", OrderID);
            Params.Add("price", price.ToString());
            Params.Add("ordType", OrderType);
            Params.Add("execInst", "Close");
            Params.Add("text", "Position Close from www.bitmex.com");

            return true; 
        }

        public void LoopExploitedAmend(string OrderID, string OrderType, double Price) 
        {

            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("orderID", OrderID);
            Params.Add("price", price.ToString());
            Params.Add("ordType", OrderType);
            Params.Add("execInst", "Close");
            Params.Add("text", "Position Close from www.bitmex.com");
        }

    }
}
    
    
    //Most table queries accept count, start, and reverse params. Set reverse=true to get rows newest-first.


