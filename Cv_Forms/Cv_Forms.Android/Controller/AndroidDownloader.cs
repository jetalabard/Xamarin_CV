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
using Cv_Core.DataModel;
using Cv_Core.Downloader;

namespace Cv_Forms.Droid.Controller
{
    public class AndroidDownloader : IDownloaderManager
    {
        public void Download(string url)
        {
            var source = Android.Net.Uri.Parse(url);
            var request = new DownloadManager.Request(source);
            request.AllowScanningByMediaScanner();
            request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, source.LastPathSegment);

            var manager = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);
            manager.Enqueue(request);
        }
    }
}