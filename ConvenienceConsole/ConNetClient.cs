#define BINARY
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
#if (!BINARY)
		private StreamReader sr;
		private StreamWriter sw;
#else
        private BinaryReader sr;
        private BinaryWriter sw;
#endif


		private Stream s;
		private TcpClient client;

        //Base data - kept local from start
		public Dictionary<String, Double> Users;
		public Dictionary<String, Double> Products;
        public List<Tuple<String, String>> Keydates;
        //further data - only when asking for it
        private Object answer = null;

        public static Boolean isBinaryBackend()
        {
#if BINARY
            return true;
#else
            return false;
#endif
        }

		/// <summary>
		/// Connects to the Server
		/// </summary>
		public Boolean Connect()
		{

			//Client-Mode
			//Console.WriteLine("Set up Connection");
			try { client = new TcpClient(Settings.ServerIP, Settings.Port); }
			catch (Exception) { return false; }
			//Console.WriteLine("Connection ready");
			try
			{
				s = client.GetStream();
#if (!BINARY)
				sr = new StreamReader(s);
				sw = new StreamWriter(s);
				sw.AutoFlush = true;
                sr.ReadLine();
#else
                sr = new BinaryReader(s);
                sw = new BinaryWriter(s);
                sr.ReadString();
#endif
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
#if (!BINARY)
                sw.WriteLine(command);
				String answer = sr.ReadLine();
				//String answer = sr.ReadToEnd();
				Console.WriteLine("a: " + answer);
				return this.Clienthandle(command, answer);
#else
                this.ClientCMDBinary(command);
                return this.ClientHandleBinary(command);
#endif
                
			}
			catch (Exception)
			{
				//Console.WriteLine("Exeption: " + e.Message);
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

            return this.ClientCMD(command);
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
            this.ClientCMD("keydates" + Settings.MsgSeperator + "gkclient");
        }

        public List<Tuple<int,string,double,string,string>> GetFullUsers()
        {
            bool a = this.ClientCMD("fullusers" + Settings.MsgSeperator + "gkclient");
            if (a)
            {
                //successfull
                return ((List<Tuple<int,string,double,string,string>>) this.answer);
            }
            //failed - return null
            return null;
        }

        public List<Tuple<string,string,double,string>> GetActivity()
        {
            bool a = this.ClientCMD("activity" + Settings.MsgSeperator + "gkclient");
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
			bool a = this.ClientCMD ("lastkeydate" + Settings.MsgSeperator + "gkclient");
			if (a)
			{
				return ((Dictionary<string,double>)this.answer);
			}
			return null;
		}

#if (BINARY)
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

			    default:
				    //what happened?
				    return true;
			    }

        }

#endif
		/// <summary>
		/// Tells the server to update its data and then fetches User and Product Information
		/// </summary>
		public void Update()
		{
			Console.WriteLine("Update Server");
			Console.WriteLine(this.ClientCMD("update" + Settings.MsgSeperator + "gkclient"));
			Console.WriteLine("Update Users");
			Console.WriteLine(this.ClientCMD("users" + Settings.MsgSeperator + "gkclient"));
			Console.WriteLine("Update Products");
			Console.WriteLine(this.ClientCMD("prices" + Settings.MsgSeperator + "gkclient"));
		}

		/// <summary>
		/// Handles the commands and answers on the client side.
		/// Returns true, if everxthing is fine
		/// </summary>
		/// <param name="name">The command that was sent</param>
		/// <param name="answer">The answer received from Server</param>
		private Boolean Clienthandle(string name, string answer)
		{
			String p1 = name.Substring(0, name.IndexOf(Settings.MsgSeperator));

			switch (p1)
			{
			case "users":
				try
				{
					Dictionary<String, Double> dict = String2Dict(answer);
					//success!
					this.Users = dict;
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			case "update":
				return (answer == Settings.MsgACK);
            case "buy":
                return (answer == Settings.MsgACK);

			case "prices":
				try
				{
					Dictionary<String, Double> dict = String2Dict(answer);
					//success!
					this.Products = dict;
					return true;
				}
				catch (Exception)
				{
					return false;
				}

			case "register":
				//not yet Implemented
				return true;

			default:
				//what happened?
				return true;
			}

			throw new NotImplementedException();
		}

		/// <summary>
		/// Close the connection
		/// </summary>
		public void Close()
		{
			client.Close();
		}


		/*private Boolean Buy(List<String> prod)
		{
			//create String
			String s = "buy" + Settings.MsgSeperator + "gkclient";
			foreach (String p in prod)
			{
				s += "" + Settings.MsgSeperator + p;
			}
			this.Connect ();
			Boolean answer = this.ClientCMD (s);
			this.Close ();

			return answer;
		}*/

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

		/// <summary>
		/// Converts a Dictionary<String,Double> to a String
		/// </summary>
        [System.Obsolete("use the Serialization class methods instead!")]
		public static String Dict2String(Dictionary<String,Double> dict)
		{
			String text = "";
			Console.WriteLine("Dict-size: " + dict.Count());

			foreach (KeyValuePair<String, Double> s in dict)
			{
				//Console.WriteLine("[GetProducts] Product: " + s.Key + " with price: " + s.Value);
				text = text + s.Key + "=" + s.Value + Settings.MsgSeperator;
			}

			return text;
		}

		/// <summary>
		/// Converts a String to a Dictionary<String,Double>
		/// </summary>
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
                /**
                 * 
                 * Known Issue!
                 * Depending on Localization the Strings need ',' or '.'.
                 * For now, we only handle Germany (DE, using ',') and (##, using '.')
                 * 
                 * 
                 **/
                String v = s.Value;
                if (System.Globalization.RegionInfo.CurrentRegion.TwoLetterISORegionName.Equals("DE"))
                {
                    v = s.Value.Replace('.', ',');
                }
                //String v = s.Value;
                dict.Add(s.Key, (Convert.ToDouble(v)));
                //Console.WriteLine("s2d: ("+v+") "+(Convert.ToDouble(v)).ToString("R"));
            }

			return dict;
		}
	}
}
