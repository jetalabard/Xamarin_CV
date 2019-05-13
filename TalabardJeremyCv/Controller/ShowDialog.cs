using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace TalabardJeremyCv.Controller
{
    public class ShowDialog
    {

        public enum MessageResult
        {
            NONE = 0,
            OK = 1,
            CANCEL = 2,
            ABORT = 3,
            RETRY = 4,
            IGNORE = 5,
            YES = 6,
            NO = 7
        }

        public Task<MessageResult> AlertAsync(Context context, string title, string message, string positiveButton, string negativeButton)
        {
            var tcs = new TaskCompletionSource<MessageResult>();

            using (var db = new AlertDialog.Builder(context))
            {
                db.SetTitle(title);
                db.SetMessage(message);
                db.SetCancelable(false);
                db.SetPositiveButton(positiveButton, (sender, args) => { tcs.TrySetResult(MessageResult.YES); });
                db.SetNegativeButton(negativeButton, (sender, args) => { tcs.TrySetResult(MessageResult.NO); });
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    db.Show();
                });
            }

            return tcs.Task;
        }
    }
}