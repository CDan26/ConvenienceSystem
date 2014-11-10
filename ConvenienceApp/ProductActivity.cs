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
    public class ConnectedActivity : Activity,ListView.IOnItemClickListener
    {


        ConNetClient client;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Connected);

            client = new ConNetClient();
            client.Connect();
            client.Update();

			ConApp.cc = client;
            //client.
            
            /*cn = new ConNet(false);
            cn.Connect();
            cn.Update();
            cn.Close();*/
            /*

            // Create your application here
            ListView listview = FindViewById<ListView>(Resource.Id.listView1);
            ListView lv = new ListView(this);

            

            test = new String[3];
            test[0] ="1a"; test[1]="2b"; test[2] = "4d";

            ArrayAdapter ListAdapter = new ArrayAdapter<String>(this, Resource.Id.listView1, test);
            ArrayAdapter LVAdapter = new ArrayAdapter<String>(this, lv.Id, test);

            //ListView.FromArray(test);
            //listview.Activated = true;
            //listview.Adapter = ListAdapter;
            listview.BringToFront();
            listview.SetBackgroundColor(Android.Graphics.Color.White);

            lv.BringToFront();
            lv.SetBackgroundColor(Android.Graphics.Color.White);
             * */
            ListView listview = FindViewById<ListView>(Resource.Id.listView1);
            //ListView listview = new ListView(this);

            //listview.SetMinimumWidth(250);
            //listview.SetMinimumHeight(250);
            listview.SetBackgroundColor(Android.Graphics.Color.DarkOrange);

            //LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            //ll.AddView(listview);


            //var ListSort = WebS.Fetch(Core.AdresID, DateTime.Today.Year, null);
            //var Sorts = ListSort.Where(i => i.Date >= DateTime.Today).Select(k => k.Sort).ToArray();

            //var Dates = ListSort.Where(i => i.Date >= DateTime.Today).Select(k => k.Date).ToArray();

            /*Dictionary<String, Double> dict = cn.cs.Users;
            List<String> userList = new List<string>();
            foreach (KeyValuePair<String, Double> kv in dict)
            {
                userList.Add(kv.Key + " (" + kv.Value + ")");
            }*/

            List<String> list = new List<string>();
            List<Tuple<String, String>> list2 = new List<Tuple<string, string>>();
            list.Add("Test1!");
            list.Add("Test2!");
            list2.Add(new Tuple<string, string>("Test1!", "uTest1"));
            list2.Add(new Tuple<string, string>("Test2!", "uTest2"));

            listview.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemSingleChoice, list);
            listview.BringToFront();

            listview.OnItemClickListener = this;

            //ll.AddView(listview, 300, 300);

            //listview.ChoiceMode = ChoiceMode.Single;
            //listview.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(listview_ItemClick);

            //SetContentView(Resource.Layout.Connected);
        }


        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            //whatever you need it to do goes here.
            TextView tv = FindViewById<TextView>(Resource.Id.textView2);
            tv.Text = "Clicked: " + position;
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