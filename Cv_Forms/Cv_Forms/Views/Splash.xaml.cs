
using Cv_Core;
using Cv_Core.ConfigurationManagement;
using Cv_Core.DataManagement;
using Cv_Core.Downloader;
using Cv_Forms.Controller;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Cv_Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Splash : ContentPage
    {
        private App _App;

        private const int SIZE_PROGRESS_LONG = 1800;

        private const int SIZE_PROGRESS_SMALL = 900;

        private IDownloader _Downloader = new Downloader();

        private string _DownloadDirectoryPath;
        private string _ImgDirectoryPath;
        private int _NbFileDownloaded = 0;
        
        public Splash(App app)
        {
            _App = app;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ShowImageAndDownload();
        }

        private async void ShowImageAndDownload()
        {
            Image image = this.FindByName<Image>("SplashImage");
            image.Opacity = 0;
            await image.FadeTo(10, 1000);

            DownloadIfNecessary();
        }

        private async void DownloadIfNecessary()
        {
            if (CheckInternet.HasConnexion() || CheckIsDownload())
            {
                ProgressBar.IsVisible = true;
                await SimulateStartup();
            }
            else
            {
                if (await DisplayAlert("Erreur",
                     "Pas de connection internet, activer le Wifi ou les données mobiles et rééssayer!",
                     "Réésayer",
                     "Quitter"))
                {
                    DownloadIfNecessary();
                }
                else
                {
                    StopApplication();
                }
            }
        }

        private void StopApplication()
        {
            MainThread.BeginInvokeOnMainThread(() => { 
                var closer = DependencyService.Get<ICloseApplication>();
                closer.FinishApplication();
            });
        }

        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                Log.Warning("Download", "File have been downloaded with success");
                _NbFileDownloaded++;

                ProgressBar.ProgressTo(ProgressBar.Progress + 0.3, SIZE_PROGRESS_LONG, Easing.Linear);
                if (_NbFileDownloaded == 2)
                {
                    Preferences.Set(Constants.DATE_DOWNLAOD, DateTime.Now.ToString());
                    Preferences.Set(Constants.SHARED_DATABASE_PATH, Path.Combine(_DownloadDirectoryPath, Constants.DATABASE_FILE_NAME));

                    DataManager.GetInstance(ConfigInstance.GetInstance().IsDev, Preferences.Get(Constants.SHARED_DATABASE_PATH, ""));
                    string pathZipFile = Path.Combine(_DownloadDirectoryPath, Constants.IMG_FILE_NAME);
                    ZipManager.Unzip(pathZipFile, _ImgDirectoryPath);

                    //delete temporary file after get values
                    File.Delete(Path.Combine(_DownloadDirectoryPath, Constants.IMG_FILE_NAME));
                    ProgressBar.ProgressTo(0.99, SIZE_PROGRESS_LONG, Easing.Linear);
                    _App.ChangePage(new MainPage());
                }
            }
            else
            {
                Log.Warning("Download", "Error while saving the file");
            }
        }

        private bool CheckIsDownload()
        {
            string date = Preferences.Get(Constants.DATE_DOWNLAOD, string.Empty);
            double parameter = Preferences.Get(Constants.REFRESH_MODE_PREFERENCES_DAYS, 1.0);
            return date != string.Empty 
                ? Convert.ToDateTime(date) >= DateTime.Now.AddDays(-1 * parameter) 
                : false;

        }

        public async Task SimulateStartup()
        {
            DirectoryDefaultManager dirManager = new DirectoryDefaultManager();
            
            _ImgDirectoryPath = dirManager.GetImageDirectory();
            _DownloadDirectoryPath = dirManager.GetDownlaodDirectory();

            if (!CheckIsDownload() || !File.Exists(Path.Combine(_DownloadDirectoryPath, Constants.DATABASE_FILE_NAME)) || !Directory.Exists(_ImgDirectoryPath))
            {
                await ProgressBar.ProgressTo(0.1, SIZE_PROGRESS_LONG, Easing.Linear);
                //save temporary file
                _Downloader.OnFileDownloaded += OnFileDownloaded;
                // Use the configuration value
                _Downloader.DownloadFile(ConfigInstance.GetInstance().DatabaseUrl, _DownloadDirectoryPath, Constants.DATABASE_FILE_NAME);
                _Downloader.DownloadFile(ConfigInstance.GetInstance().ImageUrl, _DownloadDirectoryPath, Constants.IMG_FILE_NAME);

                await ProgressBar.ProgressTo(0.2, SIZE_PROGRESS_LONG, Easing.Linear);

            }
            else
            {
                Log.Warning("Download", "Database already save");
                DataManager.GetInstance(ConfigInstance.GetInstance().IsDev, Preferences.Get(Constants.SHARED_DATABASE_PATH, ""));

                await ProgressBar.ProgressTo(0.99, SIZE_PROGRESS_SMALL, Easing.Linear);
                _App.ChangePage(new MainPage());

            }


        }
    }
}