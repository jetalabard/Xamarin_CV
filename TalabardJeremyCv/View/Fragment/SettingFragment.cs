
using Android.OS;
using Android.Views;
using Android.Widget;
using TalabardJeremyCv.Controller.Services;
using System;
using Android.Support.V7.App;
using Cv_Core;

namespace TalabardJeremyCv.XView.XFragment
{
    public class SettingFragment : Android.App.Fragment
    {
        public static SettingFragment NewInstance()
        {
            var bundle = new Bundle();
            return new SettingFragment { Arguments = bundle };
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            View view = inflater.Inflate(Resource.Layout.settings, container, false);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                ((AppCompatActivity)Activity).SupportActionBar.Subtitle = Constants.PAGE_SETTING;
            }
            else
            {
                TextView TitleLabel = view.FindViewById<TextView>(Resource.Id.TitleSetting);
                TitleLabel.SetText(Constants.PAGE_SETTING, TextView.BufferType.Normal);
                TitleLabel.Visibility = ViewStates.Invisible;
            }

            Spinner spRefreshData = view.FindViewById<Spinner>(Resource.Id.cbSettingsReload);
            spRefreshData.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this.Activity, Resource.Array.refresh_mode, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spRefreshData.Adapter = adapter;

            string refreshMode = SharedPreferenceManager.GetString(this.Activity, Constants.REFRESH_MODE_PREFERENCES);
            if (!string.IsNullOrEmpty(refreshMode))
            {
                int spinnerPosition = adapter.GetPosition(refreshMode);
                spRefreshData.SetSelection(spinnerPosition);
            }

            return view;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string refreshMode = spinner.GetItemAtPosition(e.Position).ToString();
            SharedPreferenceManager.SaveString(this.Activity, Constants.REFRESH_MODE_PREFERENCES, refreshMode);
            string toast = string.Format("Le mode de rafraîchissement est {0}", refreshMode);
            Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
        }
    }
}