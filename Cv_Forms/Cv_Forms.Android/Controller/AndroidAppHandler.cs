using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Cv_Forms.Controller;

namespace Cv_Forms.Droid.Controller
{
    public class AndroidAppHandler : IAppHandler
    {
        public Task<bool> LaunchApp(string uri)
        {
            bool result = false;

            try
            {
                var aUri = Android.Net.Uri.Parse(uri.ToString());
                var intent = new Intent(Intent.ActionView, aUri);
                Application.Context.StartActivity(intent);
                result = true;
            }
            catch (ActivityNotFoundException)
            {
                result = false;
            }

            return Task.FromResult(result);
        }
    }
}