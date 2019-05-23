using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using Cv_Forms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using Cv_Core;
using Cv_Forms.Controller;

namespace Cv_Forms.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : MasterDetailPage
    {

        private Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        
        public MenuItemType Item { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            
            NavigationPage.SetHasBackButton(this, false);
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
           
            menu.ChangeItemSelected(MenuItemType.Home);
        }

        public MainPage(MenuItemType menuItem = MenuItemType.Home)
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);

            menu.ChangeItemSelected(menuItem);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavigateFromMenu((int)menuItem);
            });
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavigateFromMenu((int)GetLastMenuItem());
            });
            return true;
        }


        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                ActivitiesPage page = null;
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new Home()));
                        break;

                    case (int)MenuItemType.Project:
                        page = new ActivitiesPage(Activity.Convert(DataManager.GetInstance().GetActivities<Project>()),Constants.PAGE_PROJECT);
                        MenuPages.Add(id, new NavigationPage(page));
                        break;

                    case (int)MenuItemType.Training:
                        page = new ActivitiesPage(Activity.Convert(DataManager.GetInstance().GetActivities<Training>()), Constants.PAGE_TRAINING);
                        MenuPages.Add(id, new NavigationPage(page));
                        break;

                    case (int)MenuItemType.PersonalProject:
                        page = new ActivitiesPage(Activity.Convert(DataManager.GetInstance().GetActivities<PersonalProject>()), Constants.PAGE_PERSONALPROJECT);
                        MenuPages.Add(id, new NavigationPage(page));
                        break;

                    case (int)MenuItemType.Hobie:
                        page = new ActivitiesPage(Activity.Convert(DataManager.GetInstance().GetActivities<Hobie>()), Constants.PAGE_HOBIE);
                        MenuPages.Add(id, new NavigationPage(page));
                        break;

                    case (int)MenuItemType.Job:
                        page = new ActivitiesPage(Activity.Convert(DataManager.GetInstance().GetActivities<Job>()), Constants.PAGE_HOBIE);
                        MenuPages.Add(id, new NavigationPage(page));
                        break;

                    case (int)MenuItemType.Knowledge:
                        MenuPages.Add(id, new NavigationPage(new KnowledgePage()));
                        break;

                    case (int)MenuItemType.Contact:
                        MenuPages.Add(id, new NavigationPage(new Contact()));
                        break;

                    case (int)MenuItemType.Redirection:
                        menu.ChangeItemSelected(MenuItemType.Redirection);
                        await DependencyService.Get<IAppHandler>().LaunchApp("http://www.talabard-jeremy.fr.cr");
                        break;
                }
            }
           

            if (id != (int)MenuItemType.Redirection)
            {
                var newPage = MenuPages[id];

                if (newPage != null && Detail != newPage)
                {
                    Detail = newPage;
                    Item = (MenuItemType)id;
                    menu.ChangeItemSelected(Item);
                    if (Device.RuntimePlatform == Device.Android)
                        await Task.Delay(100);

                    IsPresented = false;
                }
            }
           
        }

        private MenuItemType GetLastMenuItem()
        {
            MenuItemType menuItem = MenuItemType.Home;
            if (MenuPages.Count > 1)
            {
                menuItem = (MenuItemType)MenuPages.ElementAt(MenuPages.Count-2).Key;
            }
            return menuItem;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            int id = (int)MenuItemType.Settings;
            if (!MenuPages.ContainsKey(id))
            {
                MenuPages.Add(id, new NavigationPage(new SettingsPage()));
            }
                
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                IsPresented = false;
            }
        }
    }
}