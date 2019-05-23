using Cv_Core.DataModel;
using System.Collections.Generic;
using Cv_Core.DataManagement;

namespace Cv_Forms.ViewModels
{
    public class ActivityViewModel : BaseViewModel
    {

        public List<Activity> _Items;

        public List<Activity> Items
        {
            get { return _Items; }
            set
            {
                SetProperty(ref _Items, value);
                OnPropertyChanged();
            }
        }


        public ActivityViewModel(List<Activity> activities, string subtitle)
        {
            Items = activities;
            Description desc = DataManager.GetInstance().Description();
            Title = desc?.Name;
            Subtitle = subtitle;

        }
    }
}