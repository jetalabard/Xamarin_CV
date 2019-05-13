using Xamarin.Forms;
using Cv_Forms.Views;

namespace Cv_Forms
{
    public partial class App : Application
    {

        public App(bool isDev, string urlDatabase, string urlImage, string imgDirectoryPath, string downloadDirectoryPath)
        {
            InitializeComponent();

            MainPage = new Splash(isDev,urlDatabase,urlImage,imgDirectoryPath,downloadDirectoryPath);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
