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
using TalabardJeremyCv.Controller.Services;
using static Android.Support.V7.Widget.RecyclerView;

namespace TalabardJeremyCv.Model
{
    public class ActivityAdapter<T> : BaseAdapter<T> where T : Activity
    {
        private List<T> _Activities;

        public ActivityAdapter(List<T> activities)
        {
            _Activities = activities;
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


                view.Tag = new ViewItemHolder() { Image = photo, Title = title, SubTitle = subTitle, Date = date, Summary = summary };
            }

            var holder = (ViewItemHolder)view.Tag;

            holder.Image.SetImageDrawable(ImageManager.Get(parent.Context, _Activities[position].Image));
            holder.Title.Text = _Activities[position].Title;
            holder.SubTitle.Text = _Activities[position].SubTitle;
            holder.Date.Text = _Activities[position].Date;
            holder.Summary.Text = _Activities[position].Summary;


            return view;
        }
    }
}