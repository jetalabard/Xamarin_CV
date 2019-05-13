using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Cv_Core;
using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.XView.XFragment
{
    public class ListActivitiesFragment<T> : Android.App.Fragment where T: Activity
    {
        private List<T> _Activities;
               
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _Activities = DataManager.GetInstance().GetActivities<T>().ToList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.content_main, container, false);


            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                ((AppCompatActivity)Activity).SupportActionBar.Subtitle = GetTitle();
            }
            else
            {
                TextView TitleLabel = view.FindViewById<TextView>(Resource.Id.TitleActivity);
                TitleLabel.SetText(GetTitle(), TextView.BufferType.Normal);
                TitleLabel.Visibility = ViewStates.Invisible;
            }

            ListView listActivities= view.FindViewById<ListView>(Resource.Id.listView);

            listActivities.Adapter = new ActivityAdapter<T>(Activity, _Activities);

            return view;
        }

        private string GetTitle()
        {
            string title = string.Empty;

            if(typeof(T) == typeof(Training))
            {
                title = Constants.PAGE_TRAINING;
            }
            else if (typeof(T) == typeof(Hobie))
            {
                title = Constants.PAGE_HOBIE;
            }
            else if(typeof(T) == typeof(Job))
            {
                title = Constants.PAGE_JOB;
            }
            else if(typeof(T) == typeof(Project))
            {
                title = Constants.PAGE_PROJECT;
            }
            else if(typeof(T) == typeof(PersonalProject))
            {
                title = Constants.PAGE_PERSONALPROJECT;
            }
            return title;
        }
    }
}