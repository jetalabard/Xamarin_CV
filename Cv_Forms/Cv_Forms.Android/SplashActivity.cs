
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Cv_Core.ConfigurationManagement;
using Cv_Forms.Droid.Controller.Configuration;
using Xamarin.Essentials;
using Xamarin.Forms;
using Cv_Forms.Controller;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Permission = Plugin.Permissions.Abstractions.Permission;
using ImageCircle.Forms.Plugin.Droid;
using Cv_Core;
using System.Threading;
using Cv_Forms.Views;

namespace Cv_Forms.Droid
{
    [Activity(Label = "Talabard_Jérémy", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    public class SplashActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        private bool IsLoad = false;

        protected override async void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();

            DependencyService.Register<Controller.AndroidAppHandler>();
            DependencyService.Register<Controller.AndroidDownloader>();
            DependencyService.Register<Controller.CloseApplication>();
            DependencyService.Register<DirectoryAndroidManager>();
            DependencyService.Register<Controller.AndroidCreateImage>();

            Xamarin.FormsMaps.Init(this, savedInstanceState);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            ConfigurationManager.Initialize(new AndroidConfigurationStreamProviderFactory(() => this));
            using (var cts = new CancellationTokenSource())
            {
                // Create or get a cancellation token from somewhere
                var config = await ConfigurationManager.Instance.GetAsync(cts.Token);
                ConfigInstance.GetInstance(config);
            }

            var creator = DependencyService.Get<ICreateImage<string>>();
            ImageManager<string>.GetInstance(new DirectoryDefaultManager().GetImageDirectory(), creator);



            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status == PermissionStatus.Granted)
            {
                if (!IsLoad)
                {
                    LoadApplication(new App());
                    IsLoad = true;
                }
            }
        }


        protected override async void OnResume()
        {
            base.OnResume();
            if (!IsLoad)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];

                }
                if (status == PermissionStatus.Granted)
                {
                    LoadApplication(new App());
                    IsLoad = true;
                }
            }

        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}