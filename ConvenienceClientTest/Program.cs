using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using ConvenienceBackend;

namespace ConvenienceClientTest
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("connecting...");
            ConNetClient c = new ConNetClient();
            c.Connect();
            Console.WriteLine("updating...");
            c.Update();
            c.Close();

            //just test sth.
            Console.WriteLine("buying...");
            List<String> list = new List<string>();
            list.Add("Club Mate");
            list.Add("Club Mate");
            c.Buy("ZZTest-User", list);

            Console.WriteLine("Test has finished");
            //c.Close();

            Console.ReadLine();
        }
    }
}
