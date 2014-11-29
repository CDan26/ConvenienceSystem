using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvenienceBackend
{
    class Logger
    {
        private static Logger instance;

        private Logger() { }

        private static Logger Instance
        {
            get
            {
                if (instance == null) instance = new Logger();
                return instance;
            }
        }

        private void log(String text)
        {
            DateTime dt = DateTime.Now;
            String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
            Console.WriteLine(datum + ": " + text);
        }

        public static void Log(String text)
        {
            Instance.log(text);
        }

        public static void Log(String Sender, String text)
        {
            String s = "[" + Sender + "] " + text;
            Instance.log(s);
        }
    }
}
