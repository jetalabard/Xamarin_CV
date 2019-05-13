
using Android.App;
using Cv_Forms.Controller;

namespace Cv_Forms.Droid.Controller
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApplicationProcess()
        {
            ((Activity)Application.Context).FinishAffinity();
        }
    }
}