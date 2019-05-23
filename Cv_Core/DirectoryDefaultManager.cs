using Cv_Core;
using Cv_Core.ConfigurationManagement;
using System;
using System.IO;
using Xamarin.Forms;

namespace Cv_Forms.Controller
{
    public class DirectoryDefaultManager 
    {
        public string GetExternalDirectory(IDirectoryManager manager = null)
        {
            if(manager == null)
            {
                manager = DependencyService.Get<IDirectoryManager>();
            }
            return manager.GetExternalDirectory();
        }

        public string GetDownlaodDirectory()
        {
            return Path.Combine(GetExternalDirectory(), ConfigInstance.GetInstance().DownloadDirectoryName);
        }

        public string GetImageDirectory()
        {
            return Path.Combine(GetDownlaodDirectory(), ConfigInstance.GetInstance().ImageDirectoryName);
        }
    }
}