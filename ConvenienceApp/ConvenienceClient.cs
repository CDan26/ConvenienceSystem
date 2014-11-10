using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.Mail;
using Android.OS;

namespace ConvenienceBackend
{
    public class ConvenienceClient
    {
        

        public Dictionary<String,Double> Users;
        public Dictionary<String, Double> Products;

        public ConvenienceClient()
        {
            Users = new Dictionary<string, double>();
            Products = new Dictionary<string, double>();
        }


        public Boolean SendMail(String to, String message)
        {
            MailMessage mail = new MailMessage(Settings.MailFrom, to);
            SmtpClient client = new SmtpClient();
            client.Port = Settings.MailPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = Settings.MailSMTPHost;
            //client.Credentials = new System.Net.NetworkCredential(Settings.MailUser, Settings.MailPass);
            client.Credentials = new System.Net.NetworkCredential(Settings.MailUser, Settings.MailPass);
            client.EnableSsl = true;
            mail.Subject = "this is a test email.";
            mail.Body = message;
            try
            {
                Console.WriteLine(client.Credentials.ToString());
                client.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("Fail: " + e.Message);
            }

            return true;
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
    }
}
