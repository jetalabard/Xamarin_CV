using Cv_Core;

namespace TalabardJeremyCv.Controller
{
    public class DirectoryAndroidManager : DirectoryManager
    {
        public override string GetExternalDirectory()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
        }
    }
}