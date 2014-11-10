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
            ConNetClient c = new ConNetClient();
            c.Connect();
            Console.WriteLine("Connect is over");
            c.Close();

            Console.ReadLine();
        }
    }
}
