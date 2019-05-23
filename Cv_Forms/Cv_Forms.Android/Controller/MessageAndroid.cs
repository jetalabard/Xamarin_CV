
using Android.App;
using Android.Widget;
using Cv_Forms.Controller;
using Cv_Forms.Droid.Controller;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace Cv_Forms.Droid.Controller
{

    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}