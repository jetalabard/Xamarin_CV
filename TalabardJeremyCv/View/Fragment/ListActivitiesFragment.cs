using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using TalabardJeremyCv.Controller.DAO;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.XView.XFragment
{
    public class ListActivitiesFragment<T> : ListFragment where T: Model.Activity
    {
        private List<T> _Activities;

        private SwipeRefreshLayout swipeContainer;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            _Activities = DataManager.GetInstance().GetActivities<T>().ToList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.content_main, container, false);
            
            ListView listActivities= view.FindViewById<ListView>(Resource.Id.listView);

            listActivities.Adapter = new ActivityAdapter<T>(_Activities);


            return view;
        }

        async void SwipeContainer_Refresh(object sender, EventArgs e)
        {
            await Task.Delay(5);
            _Activities = DataManager.GetInstance().GetActivities<T>().ToList();
            (sender as SwipeRefreshLayout).Refreshing = false;
        }
    }
}