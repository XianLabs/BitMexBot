using BitMEX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    public class trollbox
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string user { get; set; }
        public string message { get; set; }
        public string html { get; set; }
        public bool fromBot { get; set; }
        public int channelID { get; set; }   

        public bool GetTrollboxData(Account A, int count, int channelID)
        {
            BitMEXApi api = new BitMEXApi(A.APIKey, A.SecretKey);
            Console.WriteLine(A.APIKey);
            Console.WriteLine(A.SecretKey);
            string TargetURI = "/api/v1/chat?count=";
            TargetURI += count;
            TargetURI += "&start=1";
            TargetURI += "&reverse=true";
            TargetURI += "&channelID=" + channelID;

            string Result = api.QueryNonv1("GET", TargetURI, null, true, false);

            Console.WriteLine(Result);

            return true;
        }

        public bool PostMessage(Account A, string msg)
        {
            BitMEXApi Api = new BitMEXApi(A.APIKey, A.SecretKey);

            Dictionary<string, string> Params = new Dictionary<string, string>();
            Params.Add("channelID", "1");
            Params.Add("message", msg);
            string Result = Api.Query("POST", "/chat", Params, true, false);
            Console.WriteLine(Result);

            return true;
        }
        
    }
}
