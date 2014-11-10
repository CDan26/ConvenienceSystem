using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using ConvenienceBackend;

namespace ConvenienceApp
{
    [Activity(Label = "ConvenienceApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += delegate 
            { 
                StartActivity(typeof(ConnectedActivity)); 
                //var ca = new Intent(this, typeof(ConnectedActivity));
                //ca.PutExtra("Test", new ConvenienceClient());
            };

            //test(null);
        }


		public void test(Action<bool> callback, String msg)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(Android.Resource.String.DialogAlertTitle);
            builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            builder.SetMessage(msg);
            builder.SetPositiveButton("OK", (sender, e) =>
            {
                callback(true);
            });
            builder.SetNegativeButton("NO", (sender, e) =>
            {
                callback(false);
            });

            builder.Show();
        }
    }
}

