using Cv_Forms.Controller;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace Cv_Core.Downloader
{
    public class Downloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        private IDirectoryManager _ManagerDirectory;


        public Downloader(IDirectoryManager managerDirectory = null)
        {
            _ManagerDirectory = managerDirectory;
        }

        public void DownloadFile(string url, string folder,string filename)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                
                folder = Path.Combine(new DirectoryDefaultManager().GetExternalDirectory(_ManagerDirectory), folder);
                if (!File.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string pathToNewFile = Path.Combine(folder, filename);
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);

            }
            catch
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