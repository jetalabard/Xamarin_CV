using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using TalabardJeremyCv.XView.Fragment;
using Android.Support.V7.App;
using Android.Widget;
using Cv_Core;

namespace TalabardJeremyCv.XView.XFragment
{
    public class KnowledgeFragment : Android.App.Fragment
    {
        private static FragmentManager _FragmentManager;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (savedInstanceState == null || _FragmentManager == null)
            {
                _FragmentManager = Activity.FragmentManager;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.knowledges, container, false);
            TextView TitleLabel = view.FindViewById<TextView>(Resource.Id.TitleKnowledges);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                ((AppCompatActivity)Activity).SupportActionBar.Subtitle = Constants.PAGE_KNOWLEDGE;
                TitleLabel.Visibility = ViewStates.Gone;
            }
            else
            {
                TitleLabel.SetText(Constants.PAGE_KNOWLEDGE, TextView.BufferType.Normal);
                TitleLabel.Visibility = ViewStates.Visible;
            }
            ViewPager viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
            MyFragmentPagerAdapter _Adapter = new MyFragmentPagerAdapter(_FragmentManager);
            viewPager.Adapter = _Adapter;

            //TabLayout 
            TabLayout tabLayout = view.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            return view;
        }

    }
}