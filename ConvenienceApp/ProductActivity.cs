using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
    /// The User has been selected and now, the App waits for the products to be bought.
    /// </summary>
    [Activity(Label = "Produkte auswaehlen")]
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

		    // Fill the ListView!
            List<KeyValuePair<String, Double>> list4 = new List<KeyValuePair<string, double>>();
            foreach (KeyValuePair<String, Double> kv in ConApp.client.Products)
            {
                list4.Add(kv);
            }

            listview.Adapter = new TwoLineAdapter(this, list4);

            listview.BringToFront();

            // Set the Click-Listener... It's me!
            listview.OnItemClickListener = this;

			//Add CLick-Listeners
			Button Resetbutton = FindViewById<Button>(Resource.Id.Button01);

            // Reset: clear products and Update
			Resetbutton.Click += delegate
			{
				this.wantBuy.Clear ();
				this.UpdateBuyList ();
			};

				
			Button BackButton = FindViewById<Button> (Resource.Id.textView1);
            // Back: Clear products and go back one Activity
			BackButton.Click += delegate
			{
				this.wantBuy.Clear ();
				StartActivity (typeof(ConnectedActivity));
			};



			Button BuyButton = FindViewById<Button> (Resource.Id.Button02);
            //Buy: Buy this stuff, send to Server and go back one Activity
			BuyButton.Click += delegate
			{
				if (this.wantBuy.Count < 1)
				{
					this.alert("Bitte zuerst etwas aussuchen");
					return;
				}

				//wait for it...
				//Buy this stuff!
				if (ConApp.client.Buy(ConApp.User,this.wantBuy))
				{
					//juhu
					this.wantBuy.Clear();
					//alert?
					this.alertBack("Erfolgreich gekauft");

				}
				else
				{
					//doof - alert?
					this.alert("Fehler - bitte erneut versuchen (Oder Strichliste nutzen)");
				}
			};

        }


		/// <summary>
		/// Update the textview that displays the current list of products to be bought
		/// </summary>
		private void UpdateBuyList()
		{
			TextView tv = FindViewById<TextView> (Resource.Id.textView2);
			String s = "Bitte Produkte waehlen";
            Double price = 0.0;
            Button BuyButton = FindViewById<Button>(Resource.Id.Button02);
            if (wantBuy.Count < 1)
			{
				//do nothing
                BuyButton.Text = "Kaufen";
			} else
			{
				s = "";
				foreach (String prod in wantBuy)
				{
					//s += prod + System.Environment.NewLine;
					s += prod + ", " ;
                    price += ConApp.client.Products[prod];
				}
			}
			tv.Text = s;
            BuyButton.Text = "Kaufen (" + price.ToString("C") + ")";
		}

		/// <summary>
		/// Handler for clicking the product. Add the string to the List.
		/// </summary>
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            //whatever you need it to do goes here.
            //TextView tv = FindViewById<TextView>(Resource.Id.textView2);
            //tv.Text = "Clicked: " + position;

			String s = ConApp.client.Products.Keys.ElementAt (position);
			this.wantBuy.Add (s);

			this.UpdateBuyList ();
			//tv.Text = "Clicked: " + s;
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

		public void alertBack(String msg)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(Android.Resource.String.DialogAlertTitle);
			builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
			builder.SetMessage(msg);
			builder.SetPositiveButton("OK", (sender, e) =>
			{
				StartActivity (typeof(ConnectedActivity));
			});

			builder.Show();
		}
    }
}