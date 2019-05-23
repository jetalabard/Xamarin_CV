
using Android.App;
using Cv_Forms.Controller;

namespace Cv_Forms.Droid.Controller
{
    public class CloseApplication : ICloseApplication
    {
        public void FinishApplication()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}