using System;
using System.Collections.Generic;
using System.Data;
using ConvenienceBackend;

namespace ConvenienceApp
{
    /// <summary>
    /// Just a static Class for making data available through all Activities
    /// </summary>
	public class ConApp
	{
        // The client for the Convenient-System
		public static ConNetClient client { get; set; }

        // The currently selected User
		public static String User = "";

	}
}

