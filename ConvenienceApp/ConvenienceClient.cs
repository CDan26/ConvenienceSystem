using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
