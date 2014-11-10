using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ConvenienceBackend
{
    public class ConNetServer
    {


        public ConvenienceServer cs;

        public ConNetServer()
        {

            this.cs = new ConvenienceServer();
        }

        private TcpListener listener;
        private Socket soc;
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;


        public Dictionary<String, Double> Users;
        public Dictionary<String, Double> Products;

        public Boolean Connect()
        {
                //Server-Mode
                Console.WriteLine("Starting Server");
                //IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
                //TcpListener listener = new TcpListener(ipAddress,4012);
                listener = new TcpListener(IPAddress.Any, 4012);
                Console.WriteLine("Starting Listener");

                listener.Start();

                // On failure, set this flag. Shut down Server, if flag is set and again an error occurs
                Boolean FailFlag = false;
                while (true)
                {
                    try
                    {
                        //Server event loop
                        Console.WriteLine("Socket init");
                        soc = listener.AcceptSocket();

                        FailFlag = false;

                        Console.WriteLine("Accepted Connection");
                        ns = new NetworkStream(soc);
                        sr = new StreamReader(ns);
                        sw = new StreamWriter(ns);
                        sw.AutoFlush = true;

                        Console.WriteLine("starting ReadLoop");

                        sw.WriteLine("starting SW");


                        while (soc.Connected)
                        {
                            string text = sr.ReadLine();
                            Console.WriteLine("Receive: " + text);
                            String answer;
                            this.ServerHandle(text, out answer);
                            Console.WriteLine("answer: " + answer);
                            sw.WriteLine(answer);
                            if (text == "quit") break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Fail: " + e.Message);
                        
                        if (FailFlag)
                        {
                            Console.WriteLine("Fail with flag -> shut down");
                            soc.Close();
                            break;
                        }

                        FailFlag = true;
                    }
                }
                //return false for signalling the socket being freed
                return false;
            
        }

        

        // Allows handling of an Request. Returns true, if data is available/no error occured
        public bool ServerHandle(String handle, out String answer)
        {
            //msg parts have to be seperated by "|" (now: Settings.MsgSeperator)
            Console.WriteLine("Serverhandle: " + handle);

            int index;
            //index = handle.LastIndexOf(Settings.MsgSeperator);
            index = handle.IndexOf(Settings.MsgSeperator);
            if (index < 1)
            {
                //not a valid msg 
                answer = Settings.MsgInvalid;
                return false;
            }

            Console.WriteLine("Serverhandles valid");
            
            String p1 = handle.Substring(0,index);
            String p2 = handle.Substring(index+1);
            //TODO: switch/case for handles
            Console.WriteLine("p1: " + p1);
            switch (p1)
            {
                case "register":
                    //TODO: Use in DB smewhere....
                    answer = this.cs.Register(p2);
                    //answer = "";
                    return true;
                case "update":
                    cs.Update();
                    answer = "done";
                    return true;
                case "prices":
                    //get prices
                    Dictionary<String, Double> dict = cs.GetProductsDict();
                    answer = Dict2String(dict);
                    return true;
                    //Transmit somehow...
                    //break;
                case "users":
                    //get users
                    Dictionary<String, Double> dictuser = cs.GetUserDict();
                    answer = Dict2String(dictuser);
                    return true;
                case "buy":
                    Console.WriteLine("Buying stuff");
                    String[] words = p2.Split(new Char[] { Settings.MsgSeperator });
                    List<String> list = new List<string>();
                    for (int i = 2; i < words.Length; i++)
                    {
                        list.Add(words[i]);
                    }
                    Boolean a = this.cs.Buy(words[1], list);
                    answer = "done";
                    return a;

                default:
                    Console.Write("Invalid Command: "+handle+" in ("+p1+","+p2+")");
                    answer = Settings.MsgInvalid;
                    return false;
                    
            }

        }

        public static String Dict2String(Dictionary<String,Double> dict)
        {
            String text = "";
            Console.WriteLine("Dict-size: " + dict.Count());
            
            foreach (KeyValuePair<String, Double> s in dict)
            {
                Console.WriteLine("[GetProducts] Product: " + s.Key + " with price: " + s.Value);
                text = text + s.Key + "=" + s.Value + Settings.MsgSeperator;
            }

            return text;
        }

        public static Dictionary<String, Double> String2Dict(String text)
        {
            Console.WriteLine("test000: " + text);
            
            Dictionary<String, String> dictS = new Dictionary<String, String>();

            dictS = text.Split(new[] {Settings.MsgSeperator}, StringSplitOptions.RemoveEmptyEntries)
               .Select(part => part.Split('='))
               .ToDictionary(split => split[0], split => split[1]);


            Dictionary<String, Double> dict = new Dictionary<string, double>();

            foreach (KeyValuePair<String, String> s in dictS)
            {
                String v = s.Value.Replace('.', ',');
                dict.Add(s.Key, (Convert.ToDouble(v)));
            }

            return dict;
        }
    }
}
