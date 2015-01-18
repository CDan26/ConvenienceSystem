using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ConvenienceBackend
{
	public class ConNetClient
	{


		//public ConvenienceClient cs;

		public ConNetClient()
		{
            //init stuff...            
			//this.cs = new ConvenienceClient();
		}
        private BinaryReader sr;
        private BinaryWriter sw;



		private Stream s;
		private TcpClient client;

        //Base data - kept local from start
		public Dictionary<String, Double> Users;
		public Dictionary<String, Double> Products;
        public List<Tuple<String, String>> Keydates;
        //further data - only when asking for it
        private Object answer = null;

        /// <summary>
        /// partial Legacy-Support (just for indicating that it is the binary backend)
        /// </summary>
        /// <returns></returns>
        public static Boolean isBinaryBackend()
        {

            return true;
        }

		/// <summary>
		/// Connects to the Server
		/// </summary>
		private Boolean Connect()
		{

			//Client-Mode
			//Console.WriteLine("Set up Connection");
			try { client = new TcpClient(Settings.ServerIP, Settings.Port); }
			catch (Exception) { return false; }
			//Console.WriteLine("Connection ready");
			try
			{
				s = client.GetStream();
                sr = new BinaryReader(s);
                sw = new BinaryWriter(s);
                sr.ReadString();

			}
			catch (Exception e)
			{
				Console.WriteLine("Fail: " + e.Message);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Send a command to the server.
		/// Pattern: CMD(Seperator)clientID(Seperator)[Arguments]
		/// </summary>
		/// <returns><c>true</c>, if command was sent and the answer handled successfully, <c>false</c> otherwise.</returns>
		/// <param name="command">The command to be sent</param>
		private Boolean ClientCMD(String command)
		{
			//mom
			//if (Server) return false;
			if (s == null) return false;
			if (sr == null) return false;
			if (sw == null) return false;
			if (client == null) return false;
			try
			{

                this.ClientCMDBinary(command);
                return this.ClientHandleBinary(command);

                
			}
			catch (Exception e)
			{
                Logger.Log("ConNetClient.ClientCMD", "Exception: " + e.Message);
				return false;
			}
			//return true;
		}

        public Boolean InsertKeyDate(string keydate="",string comment="")
        {
            string command = "addkeydate" + Settings.MsgSeperator;
            if (keydate == "")
            {
                command = command + Settings.MsgACK + Settings.MsgSeperator;
            }
            else
            {
                command = command + keydate + Settings.MsgSeperator;
            }

            if (comment == "")
            {
                command = command + Settings.MsgACK + Settings.MsgSeperator;
            }
            else
            {
                command = command + comment + Settings.MsgSeperator;
            }
            this.Connect();
            var ret = this.ClientCMD(command);
            this.Close();
            return ret;
        }

        /// <summary>
        /// reorganizes the command for the binary writer.
        /// In future, a more flexible method should be implemented
        /// </summary>
        private void ClientCMDBinary(string command)
        {
 	        
            
            //just split it up again to a string array (0:cmd, 1:clientname, 2-n:parameters)
            String[] words = command.Split(new Char[] { Settings.MsgSeperator });
            
            //get the actual command
            String p1 = words[0];

            //Send the command and the client ID
            sw.Write(p1);
            sw.Write(words[1]);

            if (p1 == "buy")
            {
                //indices 2-n are (user,product) representations
                for (int i=2;i<words.Count(); i++)
                {
                    if (words[i] != "")
                        sw.Write(words[i]);
                }
                //mark the end of the stream!
                sw.Write(Settings.MsgACK);
            }
            sw.Flush();

        }

        public void UpdateKeydates()
        {
            this.Connect();
            this.ClientCMD("keydates" + Settings.MsgSeperator + "gkclient");
            this.Close();
        }

        public List<Tuple<int,string,double,string,string>> GetFullUsers()
        {
            this.Connect();
            bool a = this.ClientCMD("fullusers" + Settings.MsgSeperator + "gkclient");
            this.Close();
            if (a)
            {
                //successfull
                return ((List<Tuple<int,string,double,string,string>>) this.answer);
            }
            //failed - return null
            return null;
        }


        public List<Tuple<int, string, double, string>> GetFullProducts()
        {
            this.Connect();
            bool a = this.ClientCMD("fullproducts" + Settings.MsgSeperator + "gkclient");
            this.Close();
            if (a)
            {
                //successfull
                return ((List<Tuple<int, string, double, string>>)this.answer);
            }
            //failed - return null
            return null;
        }

        public List<Tuple<string,string,double,string>> GetActivity()
        {
            this.Connect();
            bool a = this.ClientCMD("activity" + Settings.MsgSeperator + "gkclient");
            this.Close();
            if (a)
            {
                //successfull
                return ((List<Tuple<string, string, double, string>>)this.answer);
            }
            //failed - return null
            return null;
        }

		public Dictionary<string,double> GetDebtSinceKeydate()
		{
            this.Connect();
            bool a = this.ClientCMD("lastkeydate" + Settings.MsgSeperator + "gkclient");
            this.Close();
			if (a)
			{
				return ((Dictionary<string,double>)this.answer);
			}
			return null;
		}


        private bool ClientHandleBinary(String command)
        {
 	        //Decide on what Data to wait for by the command that was sent
            //The split-command is for legacy support

            //just split it up again to a string array (0:cmd, 1:clientname, 2-n:parameters)
            String[] words = command.Split(new Char[] { Settings.MsgSeperator });
            
            //get the actual command
            String p1 = words[0];

            String answer;

			switch (p1)
			{
			    case "users":
				    try
				    {
					    Dictionary<String, Double> dict = BinarySerializers.DeserializeDictSD(sr);
					    //success!
					    this.Users = dict;
					    Logger.Log("got users");
					    return true;
				    }
				    catch (Exception)
				    {
					    return false;
				    }
			    case "fullusers":
				    try
				    {
                        var dict = BinarySerializers.DeserializeListtISDSS(sr);
                        this.answer = dict;
					    return true;
				    }
				    catch (Exception)
				    {
					    return false;
				    }
                case "fullproducts":
                    try
                    {
                        var dict = BinarySerializers.DeserializeListtISDS(sr);
                        this.answer = dict;
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                
                case "update":
				    answer = sr.ReadString();
				    return (answer == Settings.MsgACK);

			    case "prices":
				    try
				    {
					    Dictionary<String, Double> dict = BinarySerializers.DeserializeDictSD(sr);
					    //success!
					    this.Products = dict;
					    Logger.Log("got Products");
					    return true;
				    }
				    catch (Exception)
				    {
					    return false;
				    }

			    case "register":
				    //not yet Implemented
				    return true;

				case "lastkeydate":
					Dictionary<string,double> dictDebt = BinarySerializers.DeserializeDictSD (sr);
					this.answer = dictDebt;
					return true;

                case "activity":
                    List<Tuple<string, string, double, string>> actlist = BinarySerializers.DeserializeListtSSDS(sr);
                    this.answer = actlist;
                    return true;

			    case "keydates":
				    try
				    {
					    this.Keydates = BinarySerializers.DeserializeListtS(sr);
					    Logger.Log("got Keydates");
					    return true;
				    }
				    catch (Exception)
				    {
					    return false;
				    }
                case "addkeydate":
                    answer = sr.ReadString();
                    return (answer == Settings.MsgACK);
			    case "buy":
				    //bought successfully
				    answer = sr.ReadString();
				    return (answer == Settings.MsgACK);
                case "emptynotification":
                    answer = sr.ReadString();
                    return (answer == Settings.MsgACK);


			    default:
				    //what happened?
				    return true;
			    }

        }


		/// <summary>
		/// Fetches User and Product Information from the server
		/// </summary>
		public void Update()
		{
            this.Connect();
            Console.WriteLine("Update Users");
			Console.WriteLine(this.ClientCMD("users" + Settings.MsgSeperator + "gkclient"));
			Console.WriteLine("Update Products");
			Console.WriteLine(this.ClientCMD("prices" + Settings.MsgSeperator + "gkclient"));
            this.Close();
		}

		

		/// <summary>
		/// Close the connection
		/// </summary>
		private void Close()
		{
			if (this.client != null)
            {
                if (this.client.Connected)
                {
                    this.client.Close();
                }
            }
		}



        /// <summary>
        /// Tell the server that a user bought products
        /// </summary>
        public Boolean Buy(String user, List<String> prod)
        {
            //create String
            String s = "buy" + Settings.MsgSeperator + "gkclient" + Settings.MsgSeperator + user;
            foreach (String p in prod)
            {
                s += "" + Settings.MsgSeperator + p;
            }
            s = s + Settings.MsgSeperator;
            this.Connect();
            Boolean answer = this.ClientCMD(s);
            this.Close();

            return answer;
        }



        internal void EmptyNotification()
        {
            string s = "emptynotification" + Settings.MsgSeperator+"gkclient";
            this.Connect();
            this.ClientCMD(s);
            this.Close();
            
        }
    }
}
