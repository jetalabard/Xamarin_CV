using Cv_Core;
using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using Cv_Core.Downloader;
using Cv_Forms.Controller;
using Cv_Forms.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Cv_Forms.Views
{
    [DesignTimeVisible(true)]
    public partial class Home : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public List<HomeRedirectionItem> menuItems;
 
            public Home()
        {
            InitializeComponent();
            ICollection<Header> heads = DataManager.GetInstance().Headers();
            if(heads != null)
            {
                InitListViewItemRedirection(heads.ToList());
            }

            Cv cv = DataManager.GetInstance().Cv();
            if (cv != null)
            {
                ViewModels.Button b = this.FindByName<ViewModels.Button>("buttonCv");
                b.Init(cv.Url, cv.Title, "download.png", "", false);
            }
        }

        private void InitListViewItemRedirection(List<Header> heads)
        {
            menuItems = new List<HomeRedirectionItem>
            {
                new HomeRedirectionItem {Id = MenuItemType.Project, Title=Constants.PAGE_PROJECT,Icon = "work.png",Subtitle=heads.Where(head => head.Title.Equals(Constants.PAGE_PROJECT)).FirstOrDefault().Summary},
                new HomeRedirectionItem {Id = MenuItemType.Training, Title=Constants.PAGE_TRAINING,Icon = "school.png",Subtitle=heads.Where(head => head.Title.Equals(Constants.PAGE_TRAINING)).FirstOrDefault().Summary },
                new HomeRedirectionItem {Id = MenuItemType.PersonalProject, Title=Constants.PAGE_PERSONALPROJECT,Icon = "device.png",Subtitle=heads.Where(head => head.Title.Equals(Constants.PAGE_PERSONALPROJECT)).FirstOrDefault().Summary },
                new HomeRedirectionItem {Id = MenuItemType.Knowledge, Title=Constants.PAGE_KNOWLEDGE,Icon = "book.png",Subtitle=heads.Where(head => head.Title.Equals(Constants.PAGE_KNOWLEDGE)).FirstOrDefault().Summary },
                new HomeRedirectionItem {Id = MenuItemType.Job, Title=Constants.PAGE_JOB,Icon = "job.png",Subtitle=heads.Where(head => head.Title.Equals(Constants.PAGE_JOB)).FirstOrDefault().Summary },
                new HomeRedirectionItem {Id = MenuItemType.Hobie, Title=Constants.PAGE_HOBIE,Icon = "run.png",Subtitle=heads.Where(head => head.Title.Equals(Constants.PAGE_HOBIE)).FirstOrDefault().Summary}
            };

            ListViewRedirection.ItemsSource = menuItems;


            ListViewRedirection.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                if (CurrentPage.CURRENT_PAGE != ((HomeRedirectionItem)e.SelectedItem).Id)
                {
                    CurrentPage.CURRENT_PAGE = ((HomeRedirectionItem)e.SelectedItem).Id;
                    var id = (int)CurrentPage.CURRENT_PAGE;
                    await RootPage.NavigateFromMenu(id);
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListViewRedirection.SelectedItem = null;

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ListViewRedirection.SelectedItem = null;
        }

    }
}