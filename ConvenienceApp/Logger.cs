using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvenienceBackend
{
    class Logger
    {
        
        /// <summary>
        /// represents what output for the logger should be used.
        /// Default is the console
        /// TODO: add external for providing custom delegates/adapters
        /// </summary>
        public enum Output
        {
            CONSOLE,
            NONE
        }

        public static Output Target = Output.CONSOLE;

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

            switch (Target)
            {
                case Output.CONSOLE:
                    Console.WriteLine(datum + ": " + text);
                    break;
                case Output.NONE:
                    //do nothing
                    break;
                default:
                    //should never happen...
                    break;
            }

            
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
