using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    public class liquidation
    {
        public string orderID { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public int price { get; set; }
        public int leavesQty { get; set; }
 
    
    }
}
