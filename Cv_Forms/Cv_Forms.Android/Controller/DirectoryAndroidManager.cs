using Cv_Core;

namespace Cv_Forms.Controller
{
    public class DirectoryAndroidManager : IDirectoryManager
    {
        public string GetExternalDirectory()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
        }
    }
}