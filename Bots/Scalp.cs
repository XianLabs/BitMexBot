using BitMEX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot.Bots
{
    class Scalp
    {
        //{"symbol":"ETHUSD","price":181.85,"ordType":"Limit","execInst":"Close","text":"Position Close from www.bitmex.com"}
        //{"ordType":"Market","orderQty":7000,"side":"Buy","symbol":"ETHUSD","text":"Submission from www.bitmex.com"
        public bool MarketTransaction(Account A, uint Quantity, string Side, string Symbol) //market order chooses price for u, highest by default
        {
            BitMEXApi bitmex = new BitMEXApi(A.APIKey, A.SecretKey);

            Dictionary<string, string> Params = new Dictionary<string, string>();

            Params.Add("ordType", "Market");           
            Params.Add("orderQty", Quantity.ToString().Replace("\"", ""));
            Params.Add("side", Side);
            Params.Add("symbol", Symbol);
            Params.Add("text", "Submission from www.bitmex.com");
        
            try
            { 
                if(Side == "Sell")
                {
                    Transaction S = new Transaction();
                    bool Result = S.CreateTransaction(A, true, Quantity, Transaction.OrderTypes.Market, Convert.ToDecimal(0.0), Symbol, "Sell");

                }
                else if (Side == "Buy")
                {
                    Transaction L = new Transaction();
                    L.CreateTransaction(A, true, Quantity, Transaction.OrderTypes.Market, Convert.ToDecimal(0.0), Symbol, "Buy");

                }
            }
            catch
            {
                Console.WriteLine("Error on Sell/buy");
                return false;
            }



            if(Side == "Buy")
            {


            }
            else if (Side == "Sell")
            {

            }

            return true;
        }
    }
}
