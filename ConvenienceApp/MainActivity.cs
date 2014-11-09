using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ConvenienceApp
{
    [Activity(Label = "ConvenienceApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += delegate { StartActivity(typeof(ConnectedActivity)); };

            //test(null);
        }


        public void test(Action<bool> callback)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(Android.Resource.String.DialogAlertTitle);
            builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            builder.SetMessage("message");
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

