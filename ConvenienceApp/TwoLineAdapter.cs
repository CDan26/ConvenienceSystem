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

namespace ConvenienceApp
{
    public class TwoLineAdapter : BaseAdapter<KeyValuePair<String,Double>>
    {
       List<KeyValuePair<String,Double>> items;
       Activity context;
       public TwoLineAdapter(Activity context, List<KeyValuePair<String,Double>> items)
           : base()
       {
           this.context = context;
           this.items = items;
       }
       public override long GetItemId(int position)
       {
           return position;
       }
       public override KeyValuePair<String,Double> this[int position]
       {
           get { return items[position]; }
       }
       public override int Count
       {
           get { return items.Count; }
       }
       public override View GetView(int position, View convertView, ViewGroup parent)
       {
           var item = items[position];
           View view = convertView;
           if (view == null) // no view to re-use, create new
               view = context.LayoutInflater.Inflate(Resource.Layout.TwoLineView, null);
           view.FindViewById<TextView>(Resource.Id.TText1).Text = item.Key;
           view.FindViewById<TextView>(Resource.Id.TText2).Text = Convert.ToString(item.Value);
           //view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageResourceId);
           return view;
       }
    }
}