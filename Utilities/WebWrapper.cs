using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexBot
{
    class WebWrapper : CookieAwareWebClient
    {
        bool SendData(string Uri, string[] Arguments)
        {
            Console.WriteLine("hello");
            return true;
        }
        


    }
}
