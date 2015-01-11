#define BINARY
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace ConvenienceBackend
{
    public class ConNetServer
    {


        private ConvenienceServer cs;

        public ConNetServer()
        {
            this.cs = new ConvenienceServer();
        }

        private TcpListener listener;
        private Socket soc;
        private NetworkStream ns;
#if (!BINARY)
        private StreamReader sr;
        private StreamWriter sw;
#else
        private BinaryReader sr;
        private BinaryWriter sw;
#endif

        public static Boolean isBinaryBackend()
        {
#if BINARY
            return true;
#else
            return false;
#endif
        }

        //public Dictionary<String, Double> Users;
        //public Dictionary<String, Double> Products;

        public Boolean Connect()
        {
				DateTime dt = DateTime.Now;				
				String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
                //Server-Mode
                //Console.WriteLine("[]Starting Server");
                Logger.Log("ConNetServer.Connect", "Starting Server");
                //IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
                //TcpListener listener = new TcpListener(ipAddress,4012);
				listener = new TcpListener(IPAddress.Any, Settings.Port);
                Logger.Log("ConNetServer.Connect", "Starting Listener on Port"+Settings.Port);
                //Console.WriteLine("Starting Listener");

                listener.Start();

                // On failure, set this flag. Shut down Server, if flag is set and again an error occurs
                Boolean FailFlag = false;
                while (true)
                {
                    try
                    {
                        //Server event loop
                        Logger.Log("ConNetServer.Connect", "Socket init");
                        soc = listener.AcceptSocket();

                        FailFlag = false;

                        Logger.Log("ConNetServer.Connect", "Accepted Connection");
                        ns = new NetworkStream(soc);
#if (!BINARY)
                        sr = new StreamReader(ns);
                        sw = new StreamWriter(ns);
                        sw.AutoFlush = true;
#else
                        sr = new BinaryReader(ns);
                        sw = new BinaryWriter(ns);
#endif
                        

                        Logger.Log("ConNetServer.Connect", "starting ReadLoop");
#if (!BINARY)
                        sw.WriteLine("starting SW");
#else
                        sw.Write("starting SW");
                        sw.Flush();
#endif


                        while (soc.Connected)
                        {
#if (!BINARY)
                            string text = sr.ReadLine();
                            Logger.Log("ConNetServer.Connect", "Receive: " + text);
                            String answer;
                            this.ServerHandle(text, out answer);
                            Logger.Log("ConNetServer.Connect", "answer: " + answer);
                            sw.WriteLine(answer);
                            if (text == "quit") break;
#else
                            //format of binary commands:
                            //CMD(string)clientID(string)[arguments]
                            //No msg seperator needed!
                            Logger.Log("ConNetServer.Connect","waiting for commands");
                            string command = sr.ReadString();
                            Logger.Log("ConNetServer.Connect", "Received binary command: " + command);
                            if (command == "quit") break;
                            Boolean state = this.ServerHandleBinary(command);

#endif

                        }

                    }
                    catch (Exception e)
                    {
                        Logger.Log("ConNetServer.Connect", "Connect-Exception: " + e.Message);
                        
                        if (FailFlag)
                        {
                            Logger.Log("ConNetServer.Connect", "Fail with flag -> shut down");
                            soc.Close();
                            break;
                        }

                        FailFlag = true;
                    }
                }
                //return false for signalling the socket being freed
                return false;
            
        }

#if (BINARY)
        private bool ServerHandleBinary(string command)
        {
            //msg parts have to be seperated by "|" (now: Settings.MsgSeperator)
            Logger.Log("ConNetServer.ServerHandle", "ServerhandleBinary commend: " + command);

            //Get the client identifier
            String client = sr.ReadString();
            //TODO: check for authorization of this client
            
            switch (command)
            {
            case "register":
                //TODO: Use in DB smewhere....
                //answer = this.cs.Register(p2);
                //answer = "";
                return true;
            case "update":
                //cs.Update();
                //not supported anymore!
                sw.Write(Settings.MsgACK);
                sw.Flush();
                return true;
            case "prices":
                //get prices
                BinarySerializers.SerializeDictSD(cs.GetProductsDict(), sw);
                return true;
            //break;
            case "users":
                //get users
                BinarySerializers.SerializeDictSD(cs.GetUserDict(), sw);
                return true;
            case "fullusers":
                //get users
                //BinarySerializers.SerializeDictSD(cs.GetUserDict(), sw);
                BinarySerializers.SerializeListISDSS(cs.GetFullUsers(), sw);
                return true;
            case "keydates":
                BinarySerializers.SerializeListS(cs.GetKeyDatesList(), sw);
                return true;
			case "lastkeydate":
				BinarySerializers.SerializeDictSD(cs.GetDebtSinceKeyDate (), sw);	
				return true;
            case "activity":
                BinarySerializers.SerializeListSSDS(cs.GetLastActivity(), sw);
                return true;
            case "addkeydate":
                //command: keydate,client,keydate=MsgACK,comment=MsgACK
                string keydate = sr.ReadString();
                string comment = sr.ReadString();
                if (keydate == Settings.MsgACK)
                    keydate = "";
                if (comment == Settings.MsgACK)
                    comment = "";
                cs.InsertKeyDate(keydate, comment);
                sw.Write(Settings.MsgACK);
                sw.Flush();
                return true;
            case "buy":
                List<String> list = new List<String>();
                String user = sr.ReadString();
                try
                {
                    while (true)
                    {
                        String s = sr.ReadString();
                        if (s == Settings.MsgACK)
                            break;
                        list.Add(s);
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("ConNetServer.ServerhandleBinary.Buy","Exception found: " + e.Message);
                }
                
                Boolean a = this.cs.Buy(user, list);
                if (a)
                {
                    //send mail!
                    this.BuyMailThread(user, list);
                }
                //answer = "done";
                sw.Write(Settings.MsgACK);
                return true;

            default:
                Logger.Log("ConNetServer.ServerHandleBinary", "Invalid Command: " + command);
                sw.Write(Settings.MsgInvalid);
                return false;

            }
        }
#endif
        /// <summary>
        /// Just a wrapper for executing the sending mail method as thread
        /// </summary>
        private void BuyMailThread(string user,List<String> list)
        {
            Logger.Log("ConNetServer.BuymailThread", "Send (thread) mail to " + user);
            Thread thread = new Thread(delegate() { this.BuyMail(user, list); });
            thread.Start();
        }

        /// <summary>
        /// Allows handling of an Request. Returns true, if data is available/no error occured
        /// Works only for the non-binary (legacy) version
        /// </summary>
        private bool ServerHandle(String handle, out String answer)
        {
            //msg parts have to be seperated by "|" (now: Settings.MsgSeperator)
            Logger.Log("ConNetServer.ServerHandle", "Serverhandle: " + handle);

            int index;
            //index = handle.LastIndexOf(Settings.MsgSeperator);
            index = handle.IndexOf(Settings.MsgSeperator);
            if (index < 1)
            {
                //not a valid msg 
                answer = Settings.MsgInvalid;
                return false;
            }

            //Console.WriteLine("Serverhandles valid");
            
            String p1 = handle.Substring(0,index);
            String p2 = handle.Substring(index+1);
            //TODO: switch/case for handles
            //Console.WriteLine("p1: " + p1);
			//Console.WriteLine ("p2: " + p2);
            switch (p1)
            {
                case "register":
                    //TODO: Use in DB smewhere....
                    answer = this.cs.Register(p2);
                    //answer = "";
                    return true;
                case "update":
                    //cs.Update();
                    //not supported anymore!
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
					//Console.WriteLine ("Buying stuff");
					String[] words = p2.Split (new Char[] { Settings.MsgSeperator });
					List<String> list = new List<string> ();
					for (int i = 2; i < words.Length; i++)
					{
						list.Add (words [i]);
					}
					//Console.WriteLine ("list: " + list.ToString ());
		            Boolean a = this.cs.Buy(words[1], list);
                    if (a)
                    {
                        //send mail!
                        Logger.Log("ConNetServer.Connect", "Send mail to " + words[1]);
                        this.BuyMail(words[1], list);
                    }
		            answer = "done";
		            return a;

                default:
                    Logger.Log("ConNetServer.ServerHandle", "Invalid Command: "+handle+" in ("+p1+","+p2+")");
                    answer = Settings.MsgInvalid;
                    return false;
                    
            }

        }

        private void BuyMail(string p, List<string> list)
        {
            //TODO: extract Strings for mail and make it generic/english at least...
            //get mail for user
            String mail;
            //Console.WriteLine("DebugMail ("+p+")");
            Dictionary<String, String> dict = this.cs.GetMailsDict();
            /*foreach (KeyValuePair<String, String> kv in dict)
            {
                Console.WriteLine(kv.Key + " : " + kv.Value);
            }
            Console.WriteLine("End kv-list");*/
            try
            {
                Boolean a = dict.TryGetValue(p, out mail);
                //mail = this.cs.GetMailsDict()[p];
                if (!a) return;
            }
            catch (Exception e)
            {
                //no mail adress known for this person -> return
                Logger.Log("ConNetServer.BuyMail", "BuyMail-Fail: " + e.Message);
                return;
            }


            String msg = "Hallo " + p + ", " + System.Environment.NewLine + System.Environment.NewLine;
            msg += "Du hast gerade Prdukte gekauft: " + System.Environment.NewLine;
            foreach (String s in list)
            {
                //Console.WriteLine("buy: " + s);
                Double prod;
                if (this.cs.GetProductsDict().TryGetValue(s, out prod))
                    msg += s + " fuer " + (this.cs.GetProductsDict()[s]).ToString("C") + System.Environment.NewLine;
            }
            msg += "Bitte beachte, dass die Daten nur sporadisch aktualisiert werden. Bei Fragen wende dich einfach an: " + Settings.Contactmail + System.Environment.NewLine;
            msg += "Vielen Dank und guten Durst/Appetit, " + System.Environment.NewLine + "Deine Getraenkekasse";


            bool success = this.SendMail(mail, msg);

            if (success) 
            { 
                Logger.Log("ConNetServer.BuyMail", "Mail was sent");
            }
            else 
            { 
                Logger.Log("ConNetServer.BuyMail", "Mail was NOT sent"); 
            }
        }

        private Boolean SendMail(String to, String message)
        {
            //Console.WriteLine("Now, Sending Mail!");
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            MailMessage mail = new MailMessage(Settings.MailFrom, to);
            SmtpClient client = new SmtpClient();
            client.Port = Settings.MailPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = Settings.MailSMTPHost;
            //client.Credentials = new System.Net.NetworkCredential(Settings.MailUser, Settings.MailPass);
            client.Credentials = new System.Net.NetworkCredential(Settings.MailUser, Settings.MailPass);
            client.EnableSsl = true;
            mail.Subject = "Kauf im Getränkekassen-System";
            mail.Body = message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            try
            {
                //Console.WriteLine(client.Credentials.ToString());
                client.Send(mail);
            }
            catch (Exception e)
            {
                Logger.Log("ConNetServer.SendMail", "Mail-Fail: " + e.Message);
                return false;
            }

            return true;
        }

        [System.Obsolete("use the Serialization class methods instead!")]
        public static String Dict2String(Dictionary<String,Double> dict)
        {
            String text = "";
            //Console.WriteLine("Dict-size: " + dict.Count());
            
            foreach (KeyValuePair<String, Double> s in dict)
            {
                //Console.WriteLine("[GetProducts] Product: " + s.Key + " with price: " + s.Value);
                text = text + s.Key + "=" + s.Value + Settings.MsgSeperator;
            }

            return text;
        }

        [System.Obsolete("use the Serialization class methods instead!")]
        public static Dictionary<String, Double> String2Dict(String text)
        {
            //Console.WriteLine("test000: " + text);
            
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
