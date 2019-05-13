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
using Plugin.Connectivity;
using TalabardJeremyCv.Controller;

namespace TalabardJeremyCv
{
    public static class CheckInternet
    {
        public static bool HasConnexion() => CrossConnectivity.Current.IsConnected;




        public static async Task<ShowDialog.MessageResult> ShowMessageIfNotConnected(Activity activity)
        {
            return await new ShowDialog().AlertAsync(activity, 
                "Erreur", 
                "Pas de connection internet, activer le Wifi ou les données mobiles et rééssayer!", 
                "Réésayer",
                "Quitter");
            }
    }
}