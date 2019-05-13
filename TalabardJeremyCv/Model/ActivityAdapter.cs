using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Cv_Core;
using Cv_Core.DataModel;
using TalabardJeremyCv.XView.Fragment;

namespace TalabardJeremyCv.Model
{
    public class ActivityAdapter<T> : BaseAdapter<T> where T : Activity
    {
        private List<T> _Activities;

        private Context _Context;

        public ActivityAdapter(Context context, List<T> activities)
        {
            _Activities = activities;
            _Context = context;
        }

        public override T this[int position]
        {
            get
            {
                return _Activities[position];
            }
        }

        public override int Count
        {
            get
            {
                return _Activities.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, parent, false);

                var photo = view.FindViewById<ImageView>(Resource.Id.photoImageView);
                var title = view.FindViewById<TextView>(Resource.Id.titleTextView);
                var subTitle = view.FindViewById<TextView>(Resource.Id.subTitleTextView);
                var date = view.FindViewById<TextView>(Resource.Id.dateTextView);
                var summary = view.FindViewById<TextView>(Resource.Id.summaryTextView);

                var gridView = view.FindViewById<ExpandableHeightGridView>(Resource.Id.gridButton);
                gridView.Expanded =true;


                view.Tag = new ViewItemHolder() { Image = photo, Title = title, SubTitle = subTitle, Date = date, Summary = summary, Grid = gridView };
            }

            var holder = (ViewItemHolder)view.Tag;

            holder.Image.SetImageBitmap(ImageManager.GetBitmapFromPath(_Activities[position].Image));
            holder.Title.Text = _Activities[position].Title;
            holder.SubTitle.Text = _Activities[position].SubTitle;
            holder.Date.Text = _Activities[position].Date;
            holder.Summary.Text = _Activities[position].Summary;

            holder.Grid.Adapter = new ButtonAdapter(_Context,_Activities[position].Links);

            return view;
        }
    }
}