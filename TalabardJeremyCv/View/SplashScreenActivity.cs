using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using TalabardJeremyCv.Controller.DAO;
using TalabardJeremyCv.Controller.Downloader;
using TalabardJeremyCv.Controller.Services;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.XView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : AppCompatActivity
    {

        private IDownloader _Downloader = new Downloader();

        private int _NbFileDownloaded = 0;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() =>
            {
                if (CheckInternet.HasConnexion())
                {
                    SimulateStartup();
                }
                else
                {
                    CheckInternet.ShowMessageIfNotConnected(this);
                    StopApplication();
                }
            });
            startupWork.Start();
        }

        async void StopApplication()
        {
            await Task.Delay(5000); // Simulate a bit of startup work.
            FinishAffinity();
        }

        // Simulates background work that happens behind the splash screen
        void SimulateStartup()
        {
            string date = SharedPreferenceManager.GetString(ApplicationContext, "date_download_database", "");
            if (date == "" || Convert.ToDateTime(date) < DateTime.Now.AddDays(-1))
            {
                if (Controller.Permission.CheckPermission.PermissionGranted(ApplicationContext, Manifest.Permission.WriteExternalStorage))
                {
                    //save temporary file
                    _Downloader.OnFileDownloaded += OnFileDownloaded;
                    _Downloader.DownloadFile("https://www.dropbox.com/s/jlojnxpfj7iyxcs/database_cv.xml?dl=1", Constants.DOWNLOAD_DIRECTORY, "Database_cv.xml");
                    _Downloader.DownloadFile("https://www.dropbox.com/sh/agqxwfduq54sdg9/AADaoBJGIu7x-QhON6I0xlYha?dl=1", Constants.DOWNLOAD_DIRECTORY, "Img.zip");
                }
                else
                {
                    Toast.MakeText(ApplicationContext, "Permission non accordée pour l'écriture de fichier, aller dans les paramètres de l'application pour modifier cette valeur.", ToastLength.Long).Show();
                }
            }
            else
            {
                Log.Debug("Download", "Database already save");
                DataManager.GetInstance(SharedPreferenceManager.GetString(ApplicationContext, "path_database", ""));
                StartActivity(typeof(HomeActivity));
                Finish();
            }
        }
        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                Log.Debug("Download","File have been downloaded with success");
                _NbFileDownloaded++;
                if (_NbFileDownloaded == 2)
                {
                    SharedPreferenceManager.SaveString(ApplicationContext, "date_download_database", DateTime.Now.ToString());
                    SharedPreferenceManager.SaveString(ApplicationContext, "path_database", Path.Combine(Downloader.ExternalStorage, Constants.DOWNLOAD_DIRECTORY +"/Database_cv.xml"));
                    DataManager.GetInstance(SharedPreferenceManager.GetString(ApplicationContext,"path_database",""));
                    ZipFile.ExtractToDirectory(Path.Combine(Downloader.ExternalStorage, Constants.DOWNLOAD_DIRECTORY+"/Img.zip"), Path.Combine(Path.Combine(Downloader.ExternalStorage, "CV_Download"), "img"));

                    //delete temporary file after get values
                    File.Delete(Path.Combine(Downloader.ExternalStorage, Constants.DOWNLOAD_DIRECTORY+"/Img.zip"));
                   
                    StartActivity(typeof(HomeActivity));
                    Finish();
                }
            }
            else
            {
                Log.Error("Download", "Error while saving the file");
            }
        }
      
        public override void OnBackPressed() { }
    }
}