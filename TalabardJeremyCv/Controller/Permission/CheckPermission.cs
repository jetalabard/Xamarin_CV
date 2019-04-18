using Android.Content;
using Android.Support.V4.Content;

namespace TalabardJeremyCv.Controller.Permission
{
    public static class CheckPermission
    {

        public static bool PermissionGranted(Context context,string permission)
        {
            // For Android < Android M, self permissions are always granted.
            bool result = false;

            if (ContextCompat.CheckSelfPermission(context, permission) == (int)Android.Content.PM.Permission.Granted)
            {
                // We have permission
                result = true;
            }

            return result;
        }

    }




}