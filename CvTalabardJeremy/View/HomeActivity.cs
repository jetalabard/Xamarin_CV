using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using TalabardJeremyCv.XView.XFragment;
using TalabardJeremyCv.DAO;
using TalabardJeremyCv.Model;
using Android.Support.V4.App;

namespace TalabardJeremyCv.XView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", Icon ="@drawable/icon")]
    public class HomeActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IOnMapReadyCallback
    {
        private Description description;

        public static readonly int RC_INSTALL_GOOGLE_PLAY_SERVICES = 1000;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            description = DataManager.GetInstance(this).Description();

            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);


            Android.Support.V7.App.ActionBarDrawerToggle toggle = new Android.Support.V7.App.ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();


            var detailsFrag = DescriptionFragment.NewInstance();
            ChangeFragment(detailsFrag);

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
        

        private void ChangeFragment(Android.App.Fragment frag)
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
                    fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_adresse:

                    Intent i = new Intent(this, typeof(MapWithMarkersActivity));
                    i.PutExtra("Lat",description.Lat);
                    i.PutExtra("Long", description.Long);
                    i.PutExtra("Adress", description.Adress);
                    StartActivityFromChild(new MapWithMarkersActivity(), i,0);
                    startNewActivity = true;
                    break;
                case Resource.Id.nav_project:
                    //fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_persoproject:
                    //fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_facebook:
                   // fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_hobie:
                    //fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_job:
                   // fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_linkedin:
                   // fragment = DescriptionFragment.NewInstance();
                    break;
                case Resource.Id.nav_knowledges:
                  //  fragment = DescriptionFragment.NewInstance();
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
                        // call permission is not granted. If necessary display rationale & request.
                        Toast.MakeText(this, Constants.MESSAGE_CALL_PERMISSION_DENIED, ToastLength.Short).Show();
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
            }
            
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            LatLng location = new LatLng(50.897778, 3.013333);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);

            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(new LatLng(description.Lat, description.Long));
            markerOpt1.SetTitle(description.Adress);

            googleMap.AddMarker(markerOpt1);

            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            googleMap.MoveCamera(cameraUpdate);

            
        }


        bool TestIfGooglePlayServicesIsInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Dialog errorDialog = GoogleApiAvailability.Instance.GetErrorDialog(this, queryResult, RC_INSTALL_GOOGLE_PLAY_SERVICES);
                var dialogFrag = new XView.XFragment.ErrorDialogFragment(errorDialog);

                dialogFrag.Show(FragmentManager, "GooglePlayServicesDialog");
            }

            return false;
        }


    }
}

