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
        /// <summary>
        /// The actual Database Connection instance
        /// </summary>
        private MySqlConnection Connection;

        internal ConvenienceServer()
        {
            //do some init stuff if needed
        }

        /// <summary>
        /// Connects to the Database Server
        /// </summary>
        private void Connect()
        {
            Connection = new MySqlConnection("server="+Settings.Server+";database="+Settings.DBName+";uid="+Settings.DBUser+";password="+Settings.DBPass);
            Connection.Open();

            //do sth.
            //this.Query("SELECT VERSION()");
          
        }

       
        /// <summary>
        /// Closes the connection if it was open
        /// </summary>
        private void Close()
        {
            if (this.Connection != null)
            {
                Connection.Close();
                this.Connection = null;
            }
        }


        /// <summary>
        /// Executes the query in the Database returning and MySQLDataReader for the results.
        /// BEWARE: The Connection remains open and needs to be cloesd when finished reading!
        /// </summary>
        private MySqlDataReader Query(String stm)
        {
            this.Connect();
            
            MySqlCommand cmd = new MySqlCommand(stm, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// returns a Dictionary representing the (200) active users,  and their current debts
        /// </summary>
        internal Dictionary<String, Double> GetUsers()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_user WHERE gk_user.state='active' ORDER BY username ASC LIMIT 0,200");
            Dictionary<String, Double> Users = new Dictionary<string, double>();

            while (reader.Read())
            {
                Users.Add(reader.GetString("username"),reader.GetDouble("debt"));
            }

            reader.Close();
            this.Close();
            return Users;
        }

        /// <summary>
        /// Returns a List of Tuples with all information of all users (limited to 200)
        /// </summary>
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
            this.Close();
            return list;
        }

        /// <summary>
        /// returns a List of Tuples with all information about the products (limit:200)
        /// </summary>
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
            this.Close();
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
            this.Close();
            return Debts;
        }

        /// <summary>
        /// Gets the (count) last activity elements of the system.
        /// For non-positive values of count, get everything
        /// </summary>
        internal List<Tuple<string,string,double,string>> GetLastActivity(int count = 10)
        {
            MySqlDataReader reader;
            
            //Allow getting all activities by havin non-positive count-parameter
            if (count < 1)
            {
                reader = this.Query("SELECT * FROM gk_accounting ORDER BY gk_accounting.date DESC");
            }
            else
            {
                reader = this.Query("SELECT * FROM gk_accounting ORDER BY gk_accounting.date DESC LIMIT 0," + count);
            }
            

            List<Tuple<string, string, double, string>> Debts = new List<Tuple<string, string, double, string>>();

            while (reader.Read())
            {
                //Debts.Add(reader.GetString("user"), reader.GetDouble("SUM(price)"));
                string date = reader.GetString("date");
                string user = reader.GetString("user");
                double price = reader.GetDouble("price");
                string comment = reader.GetString("comment");
                Debts.Add(new Tuple<string, string, double, string>(date, user, price, comment));

            }
            reader.Close();
            this.Close();
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
            this.Close();
            return Debts;
        }

        /// <summary>
        /// Returns what product was bought how often by the user.
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
            this.Close();
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

            this.Close();
        }


        /// <summary>
        /// Returns a Dictionary of products and the corresponding price
        /// </summary>
        internal Dictionary<String, Double> GetProducts()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_pricing ORDER BY product ASC LIMIT 0,200");

            Dictionary<String, Double> Products = new Dictionary<string, double>();

            while (reader.Read())
            {
                Products.Add(reader.GetString("product"),reader.GetDouble("price"));
            }
            reader.Close();
            this.Close();
            return Products;
        }

        /// <summary>
        /// Returns a List of Tuples representing the Keydates
        /// </summary>
        internal List<Tuple<String, String>> GetKeyDates()
        {
            MySqlDataReader reader = this.Query("SELECT * FROM gk_keydates ORDER BY keydate DESC LIMIT 0,200");

            List<Tuple<String, String>> KeyDates = new List<Tuple<string, string>>();
            
            while (reader.Read())
            {
                KeyDates.Add(new Tuple<string, string>(reader.GetString("keydate"), reader.GetString("comment")));
            }

            reader.Close();
            this.Close();
            return KeyDates;
        }

        /// <summary>
        /// Returns a Dictionary of the users and their mailadresses
        /// </summary>
        internal Dictionary<String, String> GetMails()
        {
            
            MySqlDataReader reader = this.Query("SELECT * FROM gk_mail WHERE active='true'");
            Dictionary<String, String> Mails = new Dictionary<string, string>();

            while (reader.Read())
            {
                Mails.Add(reader.GetString("username"), reader.GetString("adress"));
            }

            reader.Close();
            this.Close();
            return Mails;
        }

        /// <summary>
        /// perform the buy action for a user
        /// </summary>
        /// <param name="username">the buying user</param>
        /// <param name="products">A List of the products</param>
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
            this.Close();
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

        /// <summary>
        /// TODO (unfinished)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
