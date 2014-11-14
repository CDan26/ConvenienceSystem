using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ConvenienceBackend;

namespace ConvenienceApp
{
    [Activity(Label = "ConnectedActivity")]
    public class ProductActivity : Activity,ListView.IOnItemClickListener
    {
		/// <summary>
		/// The List of Products the User wants to buy
		/// </summary>
		List<String> wantBuy;
        //ConNetClient client

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Products);
			this.wantBuy = new List<string> ();
            
            ListView listview = FindViewById<ListView>(Resource.Id.listView1);
            //ListView listview = new ListView(this);

            //listview.SetMinimumWidth(250);
            //listview.SetMinimumHeight(250);
            listview.SetBackgroundColor(Android.Graphics.Color.DarkOrange);

            
			List<String> list3 = new List<string> ();
			foreach (String s in ConApp.client.Products.Keys)
			{
				list3.Add (s);
			}

            List<String> list = new List<string>();
            List<Tuple<String, String>> list2 = new List<Tuple<string, string>>();
            list.Add("Test1!");
            list.Add("Test2!");
            list2.Add(new Tuple<string, string>("Test1!", "uTest1"));
            list2.Add(new Tuple<string, string>("Test2!", "uTest2"));

            listview.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemSingleChoice, list3);
            listview.BringToFront();

            listview.OnItemClickListener = this;

            //ll.AddView(listview, 300, 300);

            //listview.ChoiceMode = ChoiceMode.Single;
            //listview.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(listview_ItemClick);

            //SetContentView(Resource.Layout.Connected);
			//Console.WriteLine ("Size in new Activity: " + ConApp.cc.Products.Count);
        }


        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            //whatever you need it to do goes here.
            TextView tv = FindViewById<TextView>(Resource.Id.textView2);
            tv.Text = "Clicked: " + position;
			//TODO: List-Position to Product?
			String s = ConApp.client.Products.Keys.ElementAt (position);
			this.wantBuy.Add (s);
			tv.Text = "Clicked: " + s;
        }


        public void OnPause()
        {
            base.OnPause();
            //tear down Connection if needed
        }

        public void OnStop() 
        {
            base.OnStop();
            //tear down data structures if needed
        }
    }
}