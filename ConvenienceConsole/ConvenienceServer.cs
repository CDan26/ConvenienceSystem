using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using MySql.Data.Types;
using System.Data;

namespace ConvenienceBackend
{
    internal class ConvenienceServer
    {
        private MySqlConnection Connection;

        //public Dictionary<String,Double> Users;
        //public List<Tuple<int, string, double, string, string>> FullUsers;
        //public Dictionary<String, Double> Products;
        //public List<Tuple<int, string, double, string>> FullProducts;
        //public Dictionary<String, String> Mails;
        //public Dictionary<String, String> KeyDates;
        /// <summary>
        /// List of recent Accounting Activites
        /// Tuples for (Date, user, price, comment)
        /// </summary>
        //public List<Tuple<String, String, Double, String>> Accounting;
        //public List<Tuple<String, String>> KeyDates;

        internal ConvenienceServer()
        {
            //Users = new Dictionary<string, double>();
            //FullUsers = new List<Tuple<int, string, double, string, string>>();
            //Products = new Dictionary<string, double>();
            //FullProducts = new List<Tuple<int, string, double, string>>();
            //Mails = new Dictionary<string, string>();
            //Accounting = new List<Tuple<string, string, double, string>>();
            //KeyDates = new List<Tuple<string, string>>();

            //do some init stuff if needed
        }

        private void Connect()
        {
            Connection = new MySqlConnection("server="+Settings.Server+";database="+Settings.DBName+";uid="+Settings.DBUser+";password="+Settings.DBPass);
            Connection.Open();

            //do sth.
            //this.Query("SELECT VERSION()");
          
        }

        //internal void Update()
        //{
        //    this.Connect();
        //    this.GetUsers();
        //    this.GetProducts();
        //    this.GetMails();
        //    this.GetAccounting();
        //    this.GetKeyDates();
        //    this.Close();
        //}



        internal List<Tuple<String, String, Double, String>> GetAccounting(Boolean all = false)
        {
            //Get all or only the last 25 accounting activities
            MySqlDataReader reader;
            List<Tuple<String, String, Double, String>> Accounting = new List<Tuple<string,string,double,string>>();
            if (all)
            {
                reader = this.Query("SELECT * FROM gk_accounting ORDER BY gk_accounting.date DESC");
            }
            else
            { 
                reader = this.Query("SELECT * FROM gk_accounting ORDER BY gk_accounting.date DESC LIMIT 0,10");
            }
            while (reader.Read())
            {
                Accounting.Add(new Tuple<string, string, double, string>(
                    reader.GetString("date"), 
                    reader.GetString("user"), 
                    reader.GetDouble("price"), 
                    reader.GetString("comment")));
            }

            reader.Close();
            return Accounting;

        }

        private void Close()
        {
            if (this.Connection != null)
            {
                Connection.Close();
                this.Connection = null;
            }
        }

        //public void Test()
        //{
        //    Console.WriteLine("[Test] starting Connection");
        //    this.Connect();
        //    Console.WriteLine("[Test] starting Query GetUsers");
        //    this.GetUsers();
        //    Console.WriteLine("[Test] starting Query GetProducts");
        //    this.GetProducts();
        //    Console.WriteLine("[Test] starting Query GetMails");
        //    this.GetMails();
        //    Console.WriteLine("[Test] starting Decimal Test");
        //    this.DecTest();
        //    Console.WriteLine("[Test] Test finished");
        //}

        //private void DecTest()
        //{
        //    this.Connect();
        //    MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE date >= (SELECT MAX(keydate) FROM gk_keydates) GROUP BY user");
        //    while (reader.Read())
        //    {
        //        Console.WriteLine(reader.GetString("user") + ", " + reader.GetDouble("SUM(price)"));
        //    }
        //    reader.Close();
        //    this.Close();
        //}

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

        internal Dictionary<String, Double> GetUsers()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user WHERE gk_user.state='active' ORDER BY username ASC LIMIT 0,200");
            Dictionary<String, Double> Users = new Dictionary<string, double>();

            while (reader.Read())
            {
                Users.Add(reader.GetString("username"),reader.GetDouble("debt"));
            }

            reader.Close();
            return Users;
        }

        internal List<Tuple<int, string, double, string, string>> GetFullUsers()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user ORDER BY username ASC LIMIT 0,200");

            List<Tuple<int, string, double, string, string>> list = new List<Tuple<int, string, double, string, string>>();
            
            while (reader.Read())
            {
                string comment;
                try 
                { 
                    comment = reader.GetString("comment"); 
                }
                catch (Exception)
                {
                    comment = "";
                }
                list.Add(new Tuple<int, string, double, string, string>(
                    reader.GetInt32("ID"),
                    reader.GetString("username"),
                    reader.GetDouble("debt"),
                    reader.GetString("state"),
                    comment));
            }
            
            reader.Close();
            return list;
        }

        internal List<Tuple<int, string, double, string>> GetFullProducts()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_pricing ORDER BY username ASC LIMIT 0,200");

            List<Tuple<int, string, double, string>> list = new List<Tuple<int, string, double, string>>();

            while (reader.Read())
            {
                list.Add(new Tuple<int, string, double, string>(
                    reader.GetInt32("ID"),
                    reader.GetString("product"),
                    reader.GetDouble("price"),
                    reader.GetString("comment")
                    ));
            }

            reader.Close();
            return list;
        }


        /// <summary>
        /// Gets the Sum of products bought in the system since the last Keydate
        /// </summary>
        /// <returns>A Dictionary of username (key) and the sum of debt (value)</returns>
        internal Dictionary<String,Double> GetDebtSinceKeyDate()
        {
            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE gk_accounting.date>=(SELECT MAX(keydate) FROM gk_keydates) GROUP BY user LIMIT 0,200");

            Dictionary<String, Double> Debts = new Dictionary<string, double>();
            
            while (reader.Read())
            {
                Debts.Add(reader.GetString("user"), reader.GetDouble("SUM(price)"));
            }
            reader.Close();
            return Debts;
        }

        /// <summary>
        /// Gets the Sum of products bought in the system since the provided Keydate (form: yyyy-mm-dd)
        /// </summary>
        /// <returns>A Dictionary of username (key) and the sum of debt (value)</returns>
        internal Dictionary<String, Double> GetDebtSinceKeyDate(String keydate)
        {
            MySqlDataReader reader = this.Query("SELECT *,SUM(price) FROM gk_accounting WHERE gk_accounting.date>=\""+keydate+"\" GROUP BY user LIMIT 0,200");

            Dictionary<String, Double> Debts = new Dictionary<string, double>();

            while (reader.Read())
            {
                Debts.Add(reader.GetString("user"), reader.GetDouble("SUM(price)"));
            }
            reader.Close();
            return Debts;
        }

        /// <summary>
        /// Returns the what product was bought how often by the user.
        /// Beware! uses the "comment" column of the DB - on-product-comments are possible!
        /// </summary>
        /// <param name="user">The user</param>
        internal Dictionary<String, Int32> GetProductsCountForUser(String user)
        {
            Dictionary<String, Int32> prod = new Dictionary<string, Int32>();

            MySqlDataReader reader = this.Query("SELECT *,COUNT(date) FROM `gk_accounting` WHERE user='Arno Schmetz' GROUP BY `comment` DESC LIMIT 0,200");

            while (reader.Read())
            {
                prod.Add(reader.GetString("comment"), reader.GetInt32("COUNT(date)"));
            }
            reader.Close();
            return prod;
        }

        /// <summary>
        /// inserts a new keydate (form yyyy-MM-dd HH:mm:ss) into the database
        /// </summary>
        /// <param name="keydate">the keydate</param>
        /// <param name="comment">the comment that shuld be added for this keydate</param>
        internal void InsertKeyDate(String keydate = "", String comment = "Added via Application without comment")
        {
            //No keydate provided? use current datetime!
            if (keydate=="")
            {
                DateTime dt = DateTime.Now;
                //String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
                keydate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            }
            String cmd = "INSERT INTO gk_keydates (`keydate`, `comment`) VALUES ('"+keydate+"', '" + comment + "');";

            MySqlDataReader reader = this.Query(cmd);
            if (reader.Read())
            {
                String answer = reader.GetString(0);
                //Console.WriteLine(answer);
            }
        }

        internal Dictionary<String, Double> GetUserDict()
        {
            //if (this.Users == null) return null;
            //return this.Users;
            return this.GetUsers();
        }

        internal Dictionary<String, Double> GetProductsDict()
        {
            //if (this.Products == null) return null;
            //return this.Products;
            return this.GetProducts();
        }

        internal Dictionary<String, String> GetMailsDict()
        {
            //if (this.Mails == null) return null;
            //return this.Mails;
            return this.GetMails();
        }

        internal Dictionary<String, Double> GetProducts()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_pricing ORDER BY product ASC LIMIT 0,200");

            Dictionary<String, Double> Products = new Dictionary<string, double>();

            while (reader.Read())
            {
                Products.Add(reader.GetString("product"),reader.GetDouble("price"));
            }
            reader.Close();
            return Products;
        }


        internal List<Tuple<String, String>> GetKeyDates()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_keydates ORDER BY keydate DESC LIMIT 0,200");

            List<Tuple<String, String>> KeyDates = new List<Tuple<string, string>>();
            
            while (reader.Read())
            {
                KeyDates.Add(new Tuple<string, string>(reader.GetString("keydate"), reader.GetString("comment")));
            }

            reader.Close();
            return KeyDates;
        }

        internal List<Tuple<String, String>> GetKeyDatesList()
        {
            //if (this.KeyDates == null) return null;
            //return this.KeyDates;
            return this.GetKeyDates();
        }

        internal Dictionary<String, String> GetMails()
        {
            
            MySqlDataReader reader = this.Query("SELECT * FROM gk_mail WHERE active='true'");
            Dictionary<String, String> Mails = new Dictionary<string, string>();

            while (reader.Read())
            {
                Mails.Add(reader.GetString("username"), reader.GetString("adress"));
            }

            reader.Close();
            return Mails;
        }

        internal Boolean Buy(String username, List<String> products)
		{
			//Console.WriteLine ("CS, u:" + username + ", p:" + products);
			DateTime dt = DateTime.Now;
			//String datum = String.Format ("yyyy'-'MM'-'dd HH':'mm':'ss'", dt);
			String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);


			Double fullPrice = 0;
			String plist = "";

            //get Products and users
            //TODO: do this stuff inside SQL
            Dictionary<string, double> prod = this.GetProducts();
            Dictionary<string, double> user = this.GetUsers();

			foreach (String s in products) 
			{
				Double p;
                Boolean b = prod.TryGetValue(s, out p);
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

			//Console.WriteLine ("["+datum+"] "+ username + " bought " + plist);
            Logger.Log("ConvenienceServer.Buy", username + " bought " + plist);

			//Update user's debt
			Double nDebt;
			user.TryGetValue (username, out nDebt);
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

        internal String GenerateRandomString()
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!§$%&/()=?#°+-.:,;<>";
            Random random = new Random();
            String result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        internal String Register(String name)
        {
            String id = "";

            String key = this.GenerateRandomString();
            id = key + "__" + name;


            return id;
        }


        ~ConvenienceServer()
        {
            if (Connection != null)
                Connection.Close();
        }
    }
}
