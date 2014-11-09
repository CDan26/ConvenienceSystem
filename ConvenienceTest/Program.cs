using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvenienceBackend;

namespace ConvenienceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvenienceClient cc = new ConvenienceClient();
            cc.SendMail("arno@fsmpi.rwth-aachen.de", "Dies ist eine Testmail");

            Console.ReadLine();
        }
    }
}
