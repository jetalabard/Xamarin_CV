using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using TalabardJeremyCv.XView.XFragment;
using System.Collections.Generic;
using System.Linq;
using Cv_Core.DataModel;
using Cv_Core.DataManagement;

namespace TalabardJeremyCv.XView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", Icon ="@drawable/icon",ConfigurationChanges = ConfigChanges.Orientation| ConfigChanges.ScreenSize)]
    public class HomeActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private Description description;

        private static Android.App.Fragment currentFragment;

        private static List<Android.App.Fragment> lastFragments;

        public static readonly int RC_INSTALL_GOOGLE_PLAY_SERVICES = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            lastFragments = new List<Android.App.Fragment>();
            description = DataManager.GetInstance().Description();

            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            ActionBarDrawerToggle toggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();


            var detailsFrag = DescriptionFragment.NewInstance();
            
            if(currentFragment == null)
            {
                currentFragment = detailsFrag;
            }
            ChangeFragment(currentFragment);

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }


        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if(lastFragments.Count > 0)
            {
                ChangeFragment(lastFragments.Last());
                lastFragments.Remove(lastFragments.Last());
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                ChangeFragment(SettingFragment.NewInstance());
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        

        public void ChangeFragment(Android.App.Fragment frag)
        {
            if (FragmentManager.FindFragmentById(Resource.Id.container) == null)
            {
                FragmentManager.BeginTransaction()
                                .Add(Resource.Id.container, frag)
                                .Commit();
            }
            else
            {
                FragmentManager.BeginTransaction()
                           .Replace(Resource.Id.container, frag)
                           .Commit();
            }
        }
        
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            SendMail();
        }
        
        private void SendMail()
        {
            var email = new Intent(Intent.ActionSend);
            email.PutExtra(Intent.ExtraEmail, new string[] { description.Email });
            email.SetType("message/rfc822");
            StartActivity(email);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            bool startNewActivity = false;
            Android.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.nav_home:
                    fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_training:
                    fragment = new ListActivitiesFragment<Training>();
                    break;
                case Resource.Id.nav_adresse:
                    Intent newIntentMaps = null;
                    if(isAppInstalled(this, "com.android.maps") || isAppInstalled(this, "com.google.android.gms.maps"))
                    {
                        newIntentMaps = new Intent(Intent.ActionView, Android.Net.Uri.Parse("geo:"+description.Lat+","+description.Long+"?q="+description.Adress));
                    }
                    else
                    {
                        newIntentMaps = new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://www.google.com/maps/place/" + description.Adress + "/@" + description.Lat + "," + description.Long));
                    }
                    StartActivity(newIntentMaps);
                    break;
                case Resource.Id.nav_project:
                    fragment = new ListActivitiesFragment<Project>();
                    break;
                case Resource.Id.nav_persoproject:
                    fragment = new ListActivitiesFragment<PersonalProject>();
                    break;
                case Resource.Id.nav_facebook:
                    Intent newIntent = null;
                    try
                    {
                        if (isAppInstalled(this, "com.facebook.orca") || isAppInstalled(this, "com.facebook.katana")
                                || isAppInstalled(this, "com.example.facebook") || isAppInstalled(this, "com.facebook.android"))
                        {

                            newIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("fb://page/jeremy.talabard"));
                        }
                        else
                        {
                            newIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(description.FbLink));
                        }
                    }
                    catch { newIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(description.FbLink)); }
                    StartActivity(newIntent);
                    break;
                case Resource.Id.nav_hobie:
                    fragment = new ListActivitiesFragment<Hobie>();
                    break;
                case Resource.Id.nav_job:
                    fragment = new ListActivitiesFragment<Job>();
                    break;
                case Resource.Id.nav_linkedin:
                    try
                    {
                        Intent linkedinIntent = new Intent(Intent.ActionView);
                        linkedinIntent.SetData(Android.Net.Uri.Parse(description.LinkedinLink));
                        linkedinIntent.SetPackage("com.linkedin.android");
                        StartActivity(linkedinIntent);
                    }
                    catch
                    {
                        StartActivity(new Intent(Intent.ActionView,
                                Android.Net.Uri.Parse(description.LinkedinLink)));
                    }
                    break;
                case Resource.Id.nav_knowledges:
                    fragment = new KnowledgeFragment();
                    break;
                case Resource.Id.nav_phone:
                    if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) == (int)Permission.Granted)
                    {
                        Intent phone = new Intent(Intent.ActionCall,
                         Android.Net.Uri.Parse(string.Format("tel:{0}", description.Phone)));
                        StartActivity(phone);
                    }
                    else
                    {
                        Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, 5);
                    }
                   
                    break;
                case Resource.Id.nav_openinbrowser:
                    var uri = Android.Net.Uri.Parse("http://www.talabard-jeremy.fr.cr");
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_mail:
                    SendMail();
                    break;

                default:
                    break;
            }

            if(fragment != null && !startNewActivity)
            {
                ChangeFragment(fragment);
                if (!lastFragments.Contains(currentFragment))
                {
                    lastFragments.Add(currentFragment);
                }
                currentFragment = fragment;
            }
            
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private static bool isAppInstalled(Context context, String packageName)
        {
            try
            {
                context.PackageManager.GetApplicationInfo(packageName, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}

