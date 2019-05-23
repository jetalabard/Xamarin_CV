using Xamarin.Forms;
using Cv_Forms.Views;
using Xamarin.Essentials;
using Cv_Core;
using Cv_Forms.Models;
using Cv_Core.DataManagement;
using Cv_Core.ConfigurationManagement;
using System;

namespace Cv_Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
                        
            if (Preferences.Get(Constants.SHARED_SCREENLAUNCH, -1) != -1)
            {
                try
                {
                    string databaseFile = Preferences.Get(Constants.SHARED_DATABASE_PATH, "");
                    if (string.IsNullOrEmpty(databaseFile))
                    {
                        throw new NullReferenceException("database path not save in preferences");
                    }

                    DataManager.GetInstance(ConfigInstance.GetInstance().IsDev, databaseFile);
                    ChangePage(new MainPage((MenuItemType)Preferences.Get(Constants.SHARED_SCREENLAUNCH, -1)));
                }
                catch
                {
                    MainPage = new Splash(this);
                }
               
            }
            else
            {
                MainPage = new Splash(this);
            }
            Preferences.Set(Constants.SHARED_SCREENLAUNCH, -1);

        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }


        protected override void OnSleep()
        {
            if(MainPage is MainPage)
            {
                MainPage page = (MainPage)MainPage;
                Preferences.Set(Constants.SHARED_SCREENLAUNCH, (int)page.Item);
            }
            
        }

        internal void ChangePage(Page mainPage)
        {
            MainPage = mainPage;
            
        }

    }
}
