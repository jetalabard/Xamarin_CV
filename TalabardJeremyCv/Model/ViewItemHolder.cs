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

namespace TalabardJeremyCv.Model
{
    public class ViewItemHolder : Java.Lang.Object
    {
        public ImageView Image { get; set; }
        public TextView Title { get; set; }
        public TextView SubTitle { get; set; }
        public TextView Date { get; set; }
        public TextView Summary { get; set; }

        public GridView Grid { get; set; }
    }
}