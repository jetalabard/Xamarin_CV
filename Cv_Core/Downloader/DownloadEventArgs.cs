using System;
using System.Collections.Generic;

namespace Cv_Core.Downloader
{
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;

        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}