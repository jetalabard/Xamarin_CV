using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace TalabardJeremyCv.Controller.Downloader
{
    public class Downloader : IDownloader
    {
        

        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public static string ExternalStorage = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
        

        public void DownloadFile(string url, string folder,string filename)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
               
                folder = Path.Combine(ExternalStorage, folder);
                if (!File.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string pathToNewFile = Path.Combine(folder, filename);
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);

            }
            catch (Exception ex)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
            else
            {
                if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
                    
                }
            }
        }
    }
}