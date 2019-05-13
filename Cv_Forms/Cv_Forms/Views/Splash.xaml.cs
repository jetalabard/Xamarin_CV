using Cv_Core;
using Cv_Core.DataManagement;
using Cv_Core.Downloader;
using Cv_Forms.Controller;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProgressRingControl.Forms.Plugin;
using System;
using System.IO;
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
        private ProgressBar _ProgressBar;

        private IDownloader _Downloader = new Downloader(new DirectoryDefaultManager());

        private string _DownloadDirectoryPath;
        private string _ImgDirectoryPath;

        private string _UrlImage;
        private string _UrlDatabase;

        private bool _IsDev;
        private int _NbFileDownloaded = 0;

        public Splash(bool isDev, string urlDatabase, string urlImage, string imgDirectoryPath, string downloadDirectoryPath)
        {
            InitializeComponent();
            Image imageSplash = this.FindByName<Image>("SplashImage");
            imageSplash.Source = "icon.png";

            _ProgressBar = this.FindByName<ProgressBar>("SplashProgressBar");

            
            _DownloadDirectoryPath = downloadDirectoryPath;
            _ImgDirectoryPath = imgDirectoryPath;
            _UrlImage = urlImage;
            _UrlDatabase = urlDatabase;
            _IsDev = isDev;
            RunDataProcess();

        }

        protected async void RunDataProcess()
        {
            if (CheckInternet.HasConnexion() || CheckIsDownload())
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _ProgressBar.IsVisible = true;
                });
                await SimulateStartup();
            }
            else
            {
                if(await DisplayAlert("Erreur",
                      "Pas de connection internet, activer le Wifi ou les données mobiles et rééssayer!",
                      "Réésayer",
                      "Quitter"))
                {
                    OnAppearing();
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
            var closer = DependencyService.Get<ICloseApplication>();
            closer.CloseApplicationProcess();
        }

        private void HideProgressBar()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _ProgressBar.IsVisible = false;
            });
        }

        private bool CheckIsDownload()
        {
            string date = Preferences.Get(Constants.DATE_DOWNLAOD, "");
            string parameter = Preferences.Get(Constants.REFRESH_MODE_PREFERENCES, "");
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



        private async Task SimulateStartup()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status == PermissionStatus.Granted)
            {
                _Downloader.OnFileDownloaded += OnFileDownloaded;
                // Use the configuration value
                _Downloader.DownloadFile(_UrlDatabase, _DownloadDirectoryPath, Constants.DATABASE_FILE_NAME);
                _Downloader.DownloadFile(_UrlImage, _DownloadDirectoryPath, Constants.IMG_FILE_NAME);

                await Task.Delay(1);
            }
            else
            {
                await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage);
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Storage))
                {
                    OnAppearing();
                }
            }
        }

        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                Log.Warning("Download", "File have been downloaded with success");
                _NbFileDownloaded++;
                if (_NbFileDownloaded == 2)
                {
                    Preferences.Set(Constants.DATE_DOWNLAOD, DateTime.Now.ToString());
                    Preferences.Set(Constants.SHARED_DATABASE_PATH, Path.Combine(_DownloadDirectoryPath, Constants.DATABASE_FILE_NAME));

                        DataManager.GetInstance(_IsDev, Preferences.Get(Constants.SHARED_DATABASE_PATH, ""));
                        string pathZipFile = Path.Combine(_DownloadDirectoryPath, Constants.IMG_FILE_NAME);
                        ZipManager.Unzip(pathZipFile, _ImgDirectoryPath);


                    //delete temporary file after get values
                    File.Delete(Path.Combine(_DownloadDirectoryPath, Constants.IMG_FILE_NAME));
                    HideProgressBar();
                    Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                Log.Warning("Download", "Error while saving the file");
            }
        }

    }
}