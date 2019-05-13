using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Cv_Core;
using Cv_Core.DataModel;

namespace TalabardJeremyCv.Model
{
    public class ImageAdapter : BaseAdapter<Knowledge>
    {
        Android.App.Activity context;
        List<Knowledge> list;

        public ImageAdapter(Android.App.Activity _context,List<Knowledge> knowledges)
            : base()
        {
            this.context = _context;
            list = knowledges;
        }

        public override int Count
        {
            get {
                return list != null ? list.Count : 0;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Knowledge this[int index]
        {
            get { return list != null ?list[index] : null; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;
            if (convertView == null)
            {
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(200, 200);
                imageView.SetScaleType(ImageView.ScaleType.FitCenter);
                imageView.SetPadding(4, 4, 4, 4);
            }
            else
            {
                imageView = (ImageView)convertView;
            }
            if(list != null && list[position] != null)
            {
                imageView.SetImageBitmap(ImageManager.GetBitmapFromPath(list[position].Image));
            }
            return imageView;
        }




    }
}