using System;
using System.Collections.Generic;

namespace TalabardJeremyCv.Controller.Downloader
{
    public interface IDownloader
    {
        void DownloadFile(string url, string folder,string filename);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }
}