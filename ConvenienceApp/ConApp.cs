using System;
using System.Collections.Generic;
using System.Data;
using ConvenienceBackend;

namespace ConvenienceApp
{
	public class ConApp
	{
		/*public ConApp ()
		{
			this.dict = new Dictionary<string, double> ();
			dict.Add ("Test", 0.5);
		}*/

		public static Dictionary<String, Double> dict = new Dictionary<String,Double>();

		public static ConNetClient client { get; set; }

	}
}

