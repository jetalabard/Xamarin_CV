using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Cv_Core.DataModel;
using Java.IO;

namespace TalabardJeremyCv.Model
{
    public class ButtonAdapter : BaseAdapter<Link>
    {
        private List<Link> links;

        private Context _Context;

        public ButtonAdapter(Context context ,List<Link> links)
        {
            this.links = links;
            _Context = context;
        }

        public override Link this[int position] {
        get
            {
                return links[position];
            }
}

    public override int Count => links.Count;

        public int CompletedDownloadApk { get; private set; }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.buttonGrid, parent, false);

                Button button = view.FindViewById<Button>(Resource.Id.buttonActivity);

                view.Tag = new ViewItemButton() { Button = button };
            }

            var holder = (ViewItemButton)view.Tag;

            Link link = links[position];
            Drawable img = null;
            if (link.IsUrl)
            {
                img = ContextCompat.GetDrawable(_Context, Android.Resource.Drawable.IcMenuSearch);
            }
            else
            {
                img = ContextCompat.GetDrawable(_Context, Android.Resource.Drawable.StatSysDownload);
            }

            img.SetBounds(0, 0, 60, 60);

            holder.Button.SetCompoundDrawables(img, null, null, null);

            holder.Button.Text = link.Title;
            holder.Button.Click += delegate {
                BtnOneClick(position);
            };

            return view;
        }

        private void InstallAPK(File apkFile)
        {
            Intent intent = new Intent("android.intent.action.VIEW");
            intent.AddCategory("android.intent.category.DEFAULT");
            intent.SetDataAndType(Android.Net.Uri.FromFile(apkFile), "application/vnd.android.package-archive");
            _Context.StartActivity(intent);
        }

        private void BtnOneClick(int position)
        {
            Link l = links[position];
            if (l.LinkTitle.Contains(".apk"))
            {
                Download(l);
            }
            else if (l.IsUrl)
            {
                var uri = Android.Net.Uri.Parse(l.Url);
                var intent = new Intent(Intent.ActionView, uri);
                _Context.StartActivity(intent);
            }
            else
            {
                Download(l);
            }
        }

        private void Download(Link l)
        {
            var source = Android.Net.Uri.Parse(l.Url);
            var request = new DownloadManager.Request(source);
            request.AllowScanningByMediaScanner();
            request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, source.LastPathSegment);
            var manager = (DownloadManager)_Context.GetSystemService(Context.DownloadService);
            manager.Enqueue(request);
        }

    }
}