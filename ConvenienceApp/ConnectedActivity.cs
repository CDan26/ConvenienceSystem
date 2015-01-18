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
    /// <summary>
    /// System is connected and waiting for user selection
    /// </summary>
    [Activity(Label = "Person auswaehlen")]
    public class ConnectedActivity : Activity,ListView.IOnItemClickListener
    {
        
        ConNetClient client;

		List<KeyValuePair<String,Double>> list4;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Connected);

            // Try to connect to the ConvenienceServer.
            // When not successfull, tell the user and go back!
			try {
				client = new ConNetClient();
            	//client.Connect();
            	client.Update();
				//client.Close ();
			}
			catch (Exception)
			{
                this.BackConnectAlert("Verbindung konnte nicht aufgebaut werden. Bitte App beenden und spaeter neu starten!");
                return;
			}
			ConApp.client = client;
      		ListView listview = FindViewById<ListView>(Resource.Id.listView1);
            
            //Fill Listview with data!
			list4 = new List<KeyValuePair<string, double>>();
            foreach (KeyValuePair<String, Double> kv in ConApp.client.Users)
            {
                list4.Add(kv);
            }

            listview.Adapter = new TwoLineAdapter(this, list4);

            listview.BringToFront();

            //Set the Listener - it's me!
            listview.OnItemClickListener = this;

            //Add CLick-Listeners
            Button EmptyButton = FindViewById<Button>(Resource.Id.ButtonEmpty);

            // Reset: clear products and Update
            EmptyButton.Click += delegate
            {
                this.SendEmptyNotification();
                this.alert("Danke für's Melden");
            };
            
        }

        private void SendEmptyNotification()
        {
            ConApp.client.EmptyNotification();
        }

        /// <summary>
        /// Listener for havin selected a user.
        /// </summary>
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
			//ConApp.User = list3.ElementAt (position);
			ConApp.User = list4.ElementAt (position).Key;
			StartActivity(typeof(ProductActivity));
        }

        protected override void OnPause()
        {
            base.OnPause();
            //tear down Connection if needed
        }

        protected override void OnStop()
        {
            base.OnStop();
            //tear down data structures if needed
        }

        /// <summary>
        /// Just display a message and an OK-Button
        /// </summary>
		public void alert(String msg)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(Android.Resource.String.DialogAlertTitle);
			builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
			builder.SetMessage(msg);
			builder.SetPositiveButton("OK", (sender, e) =>
			{
				//nope
			});

			builder.Show();
		}

        /// <summary>
        /// Display a message. Clicking OK triggers going back to the MainActivity
        /// </summary>
        public void BackConnectAlert(String msg)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(Android.Resource.String.DialogAlertTitle);
            builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            builder.SetMessage(msg);
            builder.SetPositiveButton("OK", (sender, e) =>
            {
                StartActivity(typeof(MainActivity));
            });

            builder.Show();
        }
    }
}