
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Cv_Core.ConfigurationManagement;
using System.Threading;
using Cv_Forms.Droid.Controller.Configuration;
using Cv_Forms.Controller;

namespace Cv_Forms.Droid
{
    [Activity(Label = "Talabard_Jérémy", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    public class SplashActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            ConfigurationManager.Initialize(new AndroidConfigurationStreamProviderFactory(() => this));
            DirectoryDefaultManager dirManager = new DirectoryDefaultManager();
            bool IsDev = false;
            string UrlDatabase = string.Empty;
            string UrlImage = string.Empty;
            using (var cts = new CancellationTokenSource())
            {
                // Create or get a cancellation token from somewhere
                var config = await ConfigurationManager.Instance.GetAsync(cts.Token);
                IsDev = config.Dev;
                UrlDatabase = config.DatabaseUrl;
                UrlImage = config.ImageUrl;
                dirManager._DownloadDirectoryName = config.DownloadDirectoryName;
                dirManager._ImgDirectoryName = config.ImageDirectoryName;
            }
            
            LoadApplication(new App(IsDev, UrlDatabase, UrlImage, dirManager.GetImageDirectory(), dirManager.GetDownlaodDirectory()));


        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}