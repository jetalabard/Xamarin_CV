using System;
using System.Collections.Generic;

namespace Cv_Core.Downloader
{
    public interface IDownloader
    {
        void DownloadFile(string url, string folder,string filename);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }
}