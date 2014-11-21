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
    /// <summary>
    /// The Main or startup-Activity. Waits for the user to tell the App to connect to the server.
    /// </summary>
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

            
            // On Button-Click go to the next Activity!
            button.Click += delegate 
            { 
                StartActivity(typeof(ConnectedActivity)); 
            };

        }

        /// <summary>
        /// Some kind of "historic" test method for Dialogues.
        /// </summary>
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

