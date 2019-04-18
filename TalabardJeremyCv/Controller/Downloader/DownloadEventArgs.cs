﻿using System;
using System.Collections.Generic;

namespace TalabardJeremyCv.Controller.Downloader
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