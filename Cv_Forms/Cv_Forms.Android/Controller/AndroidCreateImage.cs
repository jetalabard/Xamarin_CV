using Android.Graphics;
using Cv_Core;

namespace Cv_Forms.Droid.Controller
{
    public class AndroidCreateImage : ICreateImage<string>
    {
        public string CreateImage(string imagePath)
        {
            return imagePath;
        }
    }
}