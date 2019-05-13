using Plugin.Connectivity;

namespace Cv_Forms.Controller
{
    public static class CheckInternet
    {
        public static bool HasConnexion() => CrossConnectivity.Current.IsConnected;
    }
}