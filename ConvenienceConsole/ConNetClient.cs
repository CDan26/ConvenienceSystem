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


		public ConvenienceClient cs;

		public ConNetClient()
		{

			this.cs = new ConvenienceClient();
		}

		private StreamReader sr;
		private StreamWriter sw;

		private Stream s;
		private TcpClient client;

		public Dictionary<String, Double> Users;
		public Dictionary<String, Double> Products;


		/// <summary>
		/// Connects to the Server
		/// </summary>
		public Boolean Connect()
		{

			//Client-Mode
			//IPAddress ipAddress = Dns.GetHostEntry("auxua.eu").AddressList[0];
			Console.WriteLine("Set up Connection");
			try { client = new TcpClient(Settings.ServerIP, Settings.Port); }
			catch (Exception e) { return false; }
			Console.WriteLine("Connection ready");
			try
			{
				s = client.GetStream();
				sr = new StreamReader(s);
				sw = new StreamWriter(s);
				sw.AutoFlush = true;
				sr.ReadLine();
				/*Console.WriteLine(sr.ReadLine());
                    while (true)
                    {
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        sw.WriteLine(name);
                        if (name == "quit") break;
                        //Console.WriteLine(sr.ReadLine());
                        String answer = sr.ReadLine();
                        Console.WriteLine(answer);
                        this.Clienthandle(name, answer);
                    }
                    s.Close();*/
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
		public Boolean ClientCMD(String command)
		{
			//mom
			//if (Server) return false;
			if (s == null) return false;
			if (sr == null) return false;
			if (sw == null) return false;
			if (client == null) return false;
			try
			{
				sw.WriteLine(command);
				String answer = sr.ReadLine();
				//String answer = sr.ReadToEnd();
				Console.WriteLine("a: " + answer);
				return this.Clienthandle(command, answer);
			}
			catch (Exception e)
			{
				//Console.WriteLine("Exeption: " + e.Message);
				return false;
			}
			//return true;
		}

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
					Console.WriteLine("test0: "+answer);
					Dictionary<String, Double> dict = String2Dict(answer);
					//success!
					this.Users = dict;
					Console.WriteLine("test1");
					return true;
				}
				catch (Exception e)
				{
					return false;
				}
			case "update":
				return (answer == "done");

			case "prices":
				try
				{
					Dictionary<String, Double> dict = String2Dict(answer);
					//success!
					this.Products = dict;
					return true;
				}
				catch (Exception e)
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
		/// CLose the connection
		/// </summary>
		public void Close()
		{
			client.Close();
		}


		/// <summary>
		/// Converts a Dictionary<String,Double> to a String
		/// </summary>
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

		/// <summary>
		/// Converts a String to a Dictionary<String,Double>
		/// </summary>
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
