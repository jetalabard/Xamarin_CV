using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace TalabardJeremyCv.XView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => {
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
        async void SimulateStartup()
        {
            await Task.Delay(2000); // Simulate a bit of startup work.
            StartActivity(new Intent(Application.Context, typeof(HomeActivity)));
        }

        public override void OnBackPressed() { }
    }
}