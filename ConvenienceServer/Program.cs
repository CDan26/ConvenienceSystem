using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.IO;
using ConvenienceBackend;

namespace ConvenienceServer
{
    class Program
    {        

        public static void Main(string[] args)
        {
            //Console.WriteLine("Starting Server");
            ConNetServer c = new ConNetServer();
            c.StartServer();

            /*ConvenienceBackend.ConvenienceServer cs = new ConvenienceBackend.ConvenienceServer();
            cs.Test();*/
            

            //Console.WriteLine("exit");
            //Console.ReadLine();
             
            Console.ReadLine();
        }

        // Statische Helper-Funktion
        static void PrintHostInfo(String host)
        {
            try
            {
                IPHostEntry hostInfo;

                // Versuche die DNS für die übergebenen Host und IP-Adressen aufzulösen
                hostInfo = Dns.GetHostEntry(host);

                // Ausgabe des kanonischen Namens
                Console.WriteLine("\tCanonical Name: " + hostInfo.HostName);

                // Ausgabe der IP-Adressen
                Console.Write("\tIP Addresses: ");

                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                    Console.Write(ipaddr.ToString() + " ");
                }

                // Ausgabe der Alias-Namen für diesen Host
                Console.Write("\n\tAliases: ");

                foreach (String alias in hostInfo.Aliases)
                {
                    Console.Write(alias + " ");
                }

                Console.WriteLine("\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\tUnable to resolve host: " + host + "\n");
            }
        }
    }
}
