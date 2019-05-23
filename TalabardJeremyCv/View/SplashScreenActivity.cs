using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cv_Core;
using Cv_Core.ConfigurationManagement;
using Cv_Core.DataManagement;
using Cv_Core.Downloader;
using Cv_Forms.Controller;
using TalabardJeremyCv.Controller;
using TalabardJeremyCv.Controller.Configuration;
using TalabardJeremyCv.Controller.Services;
using Xamarin.Essentials;

namespace TalabardJeremyCv.XView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    public class SplashScreenActivity : AppCompatActivity
    {
        private ProgressBar mProgress;

        private IDownloader _Downloader = new Downloader(new DirectoryAndroidManager());

        private string _DownloadDirectoryPath;
        private string _ImgDirectoryPath;
        private int _NbFileDownloaded = 0;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ConfigurationManager.Initialize(new AndroidConfigurationStreamProviderFactory(() => this));
            DirectoryDefaultManager dirManager = new DirectoryDefaultManager();
            using (var cts = new CancellationTokenSource())
            {
                // Create or get a cancellation token from somewhere
                var config = await ConfigurationManager.Instance.GetAsync(cts.Token);
                ConfigInstance.GetInstance(config);
            }
            _ImgDirectoryPath = dirManager.GetImageDirectory();
            _DownloadDirectoryPath = dirManager.GetDownlaodDirectory();
            ImageManager<Bitmap>.GetInstance(_ImgDirectoryPath, new AndroidCreateImage());
        }

        // Launches the startup task
        protected override async void OnResume()
        {

            base.OnResume();

            if (CheckInternet.HasConnexion() || CheckIsDownload())
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    SetContentView(Resource.Layout.splash);
                    mProgress = FindViewById<ProgressBar>(Resource.Id.SplashProgressBar);
                    mProgress.Visibility = ViewStates.Visible;
                });
                SimulateStartup();
            }
            else
            {
                if (await CheckInternet.ShowMessageIfNotConnected(this) == Controller.ShowDialog.MessageResult.YES)
                {
                    OnResume();
                }
                else
                {
                    StopApplication();
                }
            }
                
        }

        private async void StopApplication()
        {
            await Task.Delay(2000); // Simulate a bit of startup work.
            HideProgressBar();
            FinishAffinity();
        }

        // Simulates background work that happens behind the splash screen
        private void SimulateStartup()
        {
            if (!CheckIsDownload() || !File.Exists(System.IO.Path.Combine(_DownloadDirectoryPath, Constants.DATABASE_FILE_NAME)))
            {
                if (Controller.Permission.CheckPermission.PermissionGranted(ApplicationContext, Manifest.Permission.WriteExternalStorage))
                {
                    //save temporary file
                    _Downloader.OnFileDownloaded += OnFileDownloaded;
                    // Use the configuration value
                    _Downloader.DownloadFile(ConfigInstance.GetInstance().DatabaseUrl, _DownloadDirectoryPath, Constants.DATABASE_FILE_NAME);
                    _Downloader.DownloadFile(ConfigInstance.GetInstance().ImageUrl, _DownloadDirectoryPath, Constants.IMG_FILE_NAME);

                }
                else
                {
                    Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage }, 5);
                    OnResume();

                }
            }
            else
            {
                Log.Debug("Download", "Database already save");
                DataManager.GetInstance(ConfigInstance.GetInstance().IsDev, SharedPreferenceManager.GetString(ApplicationContext, Constants.SHARED_DATABASE_PATH, ""));
                HideProgressBar();
                StartActivity(typeof(HomeActivity));
                Finish();
            }
        }


        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                Log.Debug("Download", "File have been downloaded with success");
                _NbFileDownloaded++;
                if (_NbFileDownloaded == 2)
                {
                    SharedPreferenceManager.SaveString(ApplicationContext, Constants.DATE_DOWNLAOD, DateTime.Now.ToString());
                    SharedPreferenceManager.SaveString(ApplicationContext, Constants.SHARED_DATABASE_PATH, System.IO.Path.Combine(_DownloadDirectoryPath, Constants.DATABASE_FILE_NAME));

                    if (Controller.Permission.CheckPermission.PermissionGranted(ApplicationContext, Manifest.Permission.ReadExternalStorage))
                    {
                        DataManager.GetInstance(ConfigInstance.GetInstance().IsDev, SharedPreferenceManager.GetString(ApplicationContext, Constants.SHARED_DATABASE_PATH, ""));
                        string pathZipFile = System.IO.Path.Combine(_DownloadDirectoryPath, Constants.IMG_FILE_NAME);
                        ZipManager.Unzip(pathZipFile, _ImgDirectoryPath);
                    }
                    else
                    {
                        Toast.MakeText(ApplicationContext, "Permission non accordée pour la lecture du fichier, aller dans les paramètres de l'application pour modifier cette valeur.", ToastLength.Long).Show();
                    }

                    //delete temporary file after get values
                    File.Delete(System.IO.Path.Combine(_DownloadDirectoryPath, Constants.IMG_FILE_NAME));
                    HideProgressBar();
                    StartActivity(typeof(HomeActivity));
                    Finish();
                }
            }
            else
            {
                Log.Error("Download", "Error while saving the file");
            }
        }


        private bool CheckIsDownload()
        {
            string date = SharedPreferenceManager.GetString(ApplicationContext, Constants.DATE_DOWNLAOD, "");
            string parameter = SharedPreferenceManager.GetString(ApplicationContext, Constants.REFRESH_MODE_PREFERENCES, "");
            switch (parameter)
            {
                case "Tous les jours":
                    return date != "" || Convert.ToDateTime(date) >= DateTime.Now.AddDays(-1);
                case "A chaque démarrage":
                    return false;
                case "Toutes les semaines":
                    return date != "" || Convert.ToDateTime(date) >= DateTime.Now.AddDays(-7); ;
                default:
                    return date != "" || Convert.ToDateTime(date) >= DateTime.Now.AddDays(-1);
            }

        }

        public override void OnBackPressed() { }

        private void HideProgressBar()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (mProgress != null)
                    mProgress.Visibility = ViewStates.Gone;
                Task.Delay(1500);
            });
        }
    }
}