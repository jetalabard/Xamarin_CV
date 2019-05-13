using System.IO;

namespace Cv_Core
{
    public abstract class DirectoryManager
    {
        public string _DownloadDirectoryName { get; set; }
        public string _ImgDirectoryName { get; set; }

        public abstract string GetExternalDirectory();

        public string GetDownlaodDirectory()
        {
            return Path.Combine(GetExternalDirectory(), _DownloadDirectoryName);
        }

        public string GetImageDirectory()
        {
            return Path.Combine(GetDownlaodDirectory(), _ImgDirectoryName);
        }


    }
}
