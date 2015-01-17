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
        private BinaryReader sr;
        private BinaryWriter sw;


        /// <summary>
        /// Partial Legacy-Support - just for indicating this is the binary version
        /// </summary>
        /// <returns></returns>
        public static Boolean isBinaryBackend()
        {
            return true;
        }

        /// <summary>
        /// Strting Server and the internal event loop
        /// </summary>
        public Boolean StartServer()
        {
				
            
            DateTime dt = DateTime.Now;				
			String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            Logger.Log("ConNetServer.Connect", "Starting Server");
            listener = new TcpListener(IPAddress.Any, Settings.Port);
            Logger.Log("ConNetServer.Connect", "Starting Listener on Port "+Settings.Port);
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

                    sr = new BinaryReader(ns);
                    sw = new BinaryWriter(ns);

                    Logger.Log("ConNetServer.Connect", "Starting EventLoop");
                    sw.Write("starting SW");
                    sw.Flush();

                    while (soc.Connected)
                    {
                        //format of binary commands:
                        //CMD(string)clientID(string)[arguments]
                        //No msg seperator needed!
                        Logger.Log("ConNetServer.Connect","waiting for commands");
                        string command = sr.ReadString();
                        Logger.Log("ConNetServer.Connect", "Received binary command: " + command);
                        if (command == "quit") break;
                        Boolean state = this.ServerHandleBinary(command);

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


        /// <summary>
        /// The "heart" of the Server. Decide what to do depending on the command that was received.
        /// </summary>
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
                BinarySerializers.SerializeDictSD(cs.GetProducts(), sw);
                return true;
            //break;
            case "users":
                //get users
                BinarySerializers.SerializeDictSD(cs.GetUsers(), sw);
                return true;
            case "fullusers":
                //get users
                //BinarySerializers.SerializeDictSD(cs.GetUserDict(), sw);
                BinarySerializers.SerializeListISDSS(cs.GetFullUsers(), sw);
                return true;
            case "fullproducts":
                BinarySerializers.SerializeListISDS(cs.GetFullProducts(), sw);
                return true;
            case "keydates":
                BinarySerializers.SerializeListS(cs.GetKeyDates(), sw);
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
        /// Send a Mail to the user for products they bought
        /// Should be used in a seperate thread!
        /// </summary>
        /// <param name="p">username</param>
        /// <param name="list">list of products</param>
        private void BuyMail(string p, List<string> list)
        {
            //TODO: extract Strings for mail and make it generic/english at least...
            //get mail for user
            String mail;
            //Console.WriteLine("DebugMail ("+p+")");
            Dictionary<String, String> dict = this.cs.GetMails();
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
            Dictionary<string,double> products = this.cs.GetProducts();
            foreach (String s in list)
            {
                //Console.WriteLine("buy: " + s);
                Double prod;
                if (this.cs.GetProducts().TryGetValue(s, out prod))
                    msg += s + " fuer " + (products[s]).ToString("C") + System.Environment.NewLine;
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

        /// <summary>
        /// Send a mail using the Credentials in the Settings.cs
        /// </summary>
        /// <param name="to">the target mailadress</param>
        /// <param name="message">the message body of the mail</param>
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

       

    }
}
