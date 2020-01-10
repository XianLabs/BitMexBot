using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    public class Error
    {

        public string message { get; set; }
        public string name { get; set; }
        public Error error { get; set; }
        
        int GetErrorMessage(string ErrorText)
        {

            return 0;
        }

    }
}
