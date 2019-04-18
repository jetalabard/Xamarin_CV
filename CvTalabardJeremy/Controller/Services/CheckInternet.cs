using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Connectivity;

namespace TalabardJeremyCv
{
    public static class CheckInternet
    {
        public static bool HasConnexion()
        {
            return CrossConnectivity.Current.IsConnected;
        }


        public static void ShowMessageIfNotConnected(Activity activity)
        {
                AlertDialog.Builder alert = new AlertDialog.Builder(activity);
                alert.SetTitle("Erreur");
                alert.SetMessage("Pas de connection internet ! ");
                alert.SetPositiveButton("Ok", (senderAlert, args) => {
                    //Toast.MakeText(activity, "Deleted!", ToastLength.Short).Show();
                });

               /* alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                    Toast.MakeText(activity, "Cancelled!", ToastLength.Short).Show();
                });*/

                Dialog dialog = alert.Create();
                dialog.Show();
            }
    }
}