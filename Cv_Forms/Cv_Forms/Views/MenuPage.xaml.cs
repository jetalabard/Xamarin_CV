using Cv_Core;
using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using Cv_Forms.Controller;
using Cv_Forms.Models;
using Cv_Forms.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Cv_Forms.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        private List<HomeMenuItem> _MenuItems;

        public MenuPage()
        {
            InitializeComponent();
            string title = string.Empty;
            Description desc = DataManager.GetInstance().Description();
            if (desc != null)
            {
                BindingContext = new MenuViewModel
                {
                    Name = desc.Name,
                    Email = desc.Email,
                    Image = ImageManager<string>.GetInstance().GetImageFromPath(desc.Image)
                };
                title = DataManager.GetInstance().Description().Title;
            }

            InitListMenuItem(title);

        }

        private void InitListMenuItem(string title)
        {
            _MenuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title=title,Icon = "home.png" },
                new HomeMenuItem {Id = MenuItemType.Project, Title=Constants.PAGE_PROJECT,Icon = "work.png" },
                new HomeMenuItem {Id = MenuItemType.Training, Title=Constants.PAGE_TRAINING,Icon = "school.png" },
                new HomeMenuItem {Id = MenuItemType.PersonalProject, Title=Constants.PAGE_PERSONALPROJECT,Icon = "device.png" },
                new HomeMenuItem {Id = MenuItemType.Knowledge, Title=Constants.PAGE_KNOWLEDGE,Icon = "book.png" },
                new HomeMenuItem {Id = MenuItemType.Job, Title=Constants.PAGE_JOB,Icon = "job.png" },
                new HomeMenuItem {Id = MenuItemType.Hobie, Title=Constants.PAGE_HOBIE,Icon = "run.png"},
                new HomeMenuItem {Id = MenuItemType.Contact, Title=Constants.PAGE_CONTACTS,Icon = "contacts.png" },
                new HomeMenuItem {Id = MenuItemType.Redirection, Title="Version Ordinateur",Icon = "open_in_browser.png" }
            };

            ListViewMenu.ItemsSource = _MenuItems;
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                if (RootPage != null && CurrentPage.CURRENT_PAGE != MenuItemType.Redirection && CurrentPage.CURRENT_PAGE != ((HomeMenuItem)e.SelectedItem).Id)
                {
                    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                    await RootPage.NavigateFromMenu(id);
                }

            };
        }

        public void SetColorCell(int position)
        {
            IEnumerable<PropertyInfo> pInfos = (ListViewMenu as ItemsView<Cell>).GetType().GetRuntimeProperties();
            var templatedItems = pInfos.FirstOrDefault(info => info.Name == "TemplatedItems");
            if (templatedItems != null)
            {
                var cells = templatedItems.GetValue(ListViewMenu);
                ITemplatedItemsList<Cell> listCell = cells as ITemplatedItemsList<Cell>;
                ViewCell currentCell = listCell[position] as ViewCell;
                currentCell.View.BackgroundColor = Color.FromHex("#3498db");

                foreach (ViewCell cell in listCell)
                {
                    if (cell.BindingContext != null && currentCell != cell)
                    {
                        cell.View.BackgroundColor = Color.Transparent;
                    }
                }
            }
        }

        public void ChangeItemSelected(MenuItemType id)
        {
            HomeMenuItem itemMenu = _MenuItems.Where(item => item.Id == id).FirstOrDefault();
            CurrentPage.CURRENT_PAGE = id;
            SetColorCell(_MenuItems.IndexOf(_MenuItems.Where(item => item.Id == id).FirstOrDefault()));
            if (itemMenu != ListViewMenu.SelectedItem)
            {
                ListViewMenu.SelectedItem = itemMenu;
            }
        }


    }
}