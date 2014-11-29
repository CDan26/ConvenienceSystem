using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using MySql.Data.Types;
using System.Data;

namespace ConvenienceBackend
{
    public class ConvenienceServer
    {
        private MySqlConnection Connection;

        public Dictionary<String,Double> Users;
        public Dictionary<String, Double> Products;
        public Dictionary<String, String> Mails;
        //public Dictionary<String, String> KeyDates;

        public ConvenienceServer()
        {
            Users = new Dictionary<string, double>();
            Products = new Dictionary<string, double>();
            Mails = new Dictionary<string, string>();
        }

        private void Connect()
        {
            Connection = new MySqlConnection("server="+Settings.Server+";database="+Settings.DBName+";uid="+Settings.DBUser+";password="+Settings.DBPass);
            Connection.Open();

            //do sth.
            //this.Query("SELECT VERSION()");
          
        }

        public void Update()
        {
            this.Connect();
            this.GetUsers();
            this.GetProducts();
            this.GetMails();
            this.Close();
        }

        private void Close()
        {
            if (this.Connection != null)
            {
                Connection.Close();
                this.Connection = null;
            }
        }

        public void Test()
        {
            Console.WriteLine("[Test] starting Connection");
            this.Connect();
            Console.WriteLine("[Test] starting Query GetUsers");
            this.GetUsers();
            Console.WriteLine("[Test] starting Query GetProducts");
            this.GetProducts();
            Console.WriteLine("[Test] starting Query GetMails");
            this.GetMails();
            Console.WriteLine("[Test] starting Decimal Test");
            this.DecTest();
            Console.WriteLine("[Test] Test finished");
        }

        private void DecTest()
        {
            this.Connect();
            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE date >= (SELECT MAX(keydate) FROM gk_keydates) GROUP BY user");
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString("user") + ", " + reader.GetDouble("SUM(price)"));
            }
            reader.Close();
            this.Close();
        }

        private MySqlDataReader Query(String stm)
        {
            this.Connect();
            
            //String test ="";
            MySqlCommand cmd = new MySqlCommand(stm, Connection);
            //Object obj = cmd.ExecuteScalar();
            MySqlDataReader reader = cmd.ExecuteReader();
            //String test = Convert.ToString(obj);
            /*while (reader.Read())
            {
                //reader.Read();
                test = test + reader.GetString("debt");
            }

            //Debugging: print it!
            Console.WriteLine("[Query] Result: " + test);*/
            //reader.Close();
            //this.Close();
            return reader;
        }

        private void GetUsers()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user WHERE gk_user.state='active' ORDER BY username ASC LIMIT 0,200");
            Users.Clear();
            while (reader.Read())
            {
                /*if (Users.ContainsKey(reader.GetString("username")))
                {
                    Users.Remove(reader.GetString("username"));
                }*/
                Users.Add(reader.GetString("username"),reader.GetDouble("debt"));
            }

            //Debug
            /*foreach (KeyValuePair<String,Double> s in Users)
            {
                Console.WriteLine("[GetUsers] user: " + s.Key+" with debt: "+s.Value);
            }*/
            reader.Close();
            //return users;
        }

        public Dictionary<String, Double> GetUserDict()
        {
            if (this.Users == null) return null;
            return this.Users;
        }

        public Dictionary<String, Double> GetProductsDict()
        {
            if (this.Products == null) return null;
            return this.Products;
        }

        public Dictionary<String, String> GetMailsDict()
        {
            if (this.Mails == null) return null;
            return this.Mails;
        }

        private void GetProducts()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_pricing ORDER BY product ASC LIMIT 0,200");
            Products.Clear();
            while (reader.Read())
            {
                /*if (Products.ContainsKey(reader.GetString("product")))
                {
                    Products.Remove(reader.GetString("product"));
                }*/
                Products.Add(reader.GetString("product"),reader.GetDouble("price"));
            }

            //Debug
            /*foreach (KeyValuePair<String, Double> s in Products)
            {
                Console.WriteLine("[GetProducts] Product: " + s.Key + " with price: " + s.Value);
            }*/
            reader.Close();
        }

        public Dictionary<String,String> GetKeyDatesDict()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_keydates ORDER BY keydate DESC LIMIT 0,200");
            Dictionary<String, String> dict = new Dictionary<string, string>();
            while (reader.Read())
            {
                dict.Add(reader.GetString("keydate"), reader.GetString("comment"));
            }

            reader.Close();
            return dict;
        }

        private void GetMails()
        {
            
            MySqlDataReader reader = this.Query("SELECT * FROM gk_mail WHERE active='true'");
            Mails.Clear();
            while (reader.Read())
            {
                /*if (Mails.ContainsKey(reader.GetString("username")))
                {
                    Mails.Remove(reader.GetString("username"));
                }*/
                Mails.Add(reader.GetString("username"), reader.GetString("adress"));
            }

            //Debug
            /*foreach (KeyValuePair<String, Double> s in Products)
            {
                Console.WriteLine("[GetProducts] Product: " + s.Key + " with price: " + s.Value);
            }*/
            reader.Close();
        }

		public Boolean Buy(String username, List<String> products)
		{
			//Console.WriteLine ("CS, u:" + username + ", p:" + products);
			DateTime dt = DateTime.Now;
			//String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
			String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);


			Double fullPrice = 0;
			String plist = "";

			foreach (String s in products) 
			{
				Double p;
                Boolean b = this.Products.TryGetValue(s, out p);
				fullPrice = fullPrice + p;
				plist = plist + s + ", ";
				String pString = p.ToString ().Replace (',', '.');
				if (!b) break;
                String cmd = "INSERT INTO `gk_accounting` (`ID`, `date`, `user`, `price`, `comment`) VALUES (NULL, '"+datum+"', '"+username+"', '"+pString+"', '"+s+"');";
				//Console.WriteLine ("CMD: " + cmd);
				MySqlDataReader reader = this.Query (cmd);
				if (reader.Read())
				{
                    String answer = reader.GetString(0);
                    //Console.WriteLine(answer);
				}
			}

			Console.WriteLine ("["+datum+"] "+ username + " bought " + plist);

			//Update user's debt
			Double nDebt;
			Users.TryGetValue (username, out nDebt);
			nDebt = nDebt + fullPrice;
			String newDebt = nDebt.ToString().Replace(',','.');
			String cmd2 = "UPDATE `gk_user` SET `debt` = '"+newDebt+"' WHERE `gk_user`.`username` = '"+username+"';";
			//Console.WriteLine (cmd2);
			MySqlDataReader reader2 = this.Query (cmd2);
			if (reader2.Read())
			{
				String answer = reader2.GetString(0);
				//Console.WriteLine(answer);
			}

			return true;
		}

        private String GenerateRandomString()
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!§$%&/()=?#°+-.:,;<>";
            Random random = new Random();
            String result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

		private Double GetPrice(String s)
		{
			Double p = 0;
			this.Products.TryGetValue (s, out p);
			return p;
		}

        public String Register(String name)
        {
            String id = "";

            String key = this.GenerateRandomString();
            id = key + "__" + name;


            return id;
        }

        public void Test2()
        {
            Console.WriteLine("[Test2] Let's see if this still works...");
            this.GetUsers();
        }

        ~ConvenienceServer()
        {
            if (Connection != null)
                Connection.Close();
        }
    }
}
