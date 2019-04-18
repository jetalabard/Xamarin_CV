
using Android;
using Android.App;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace TalabardJeremyCv.XView
{
    [Activity(Label = "@string/adress", Theme = "@style/AppTheme")]
    public class MapWithMarkersActivity : AppCompatActivity, IOnMapReadyCallback
    {
        
        GoogleMap googleMap;

        private string _Adress;
        private double _Lat;
        private double _Long;

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;
            AddMarkersToMap();
        }


        public string IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            string msgError = string.Empty;
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    msgError = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    msgError = "Sorry, this device is not supported";
                }
            }
            else
            {
                msgError = "Google Play Services is available.";
            }
            return msgError;
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.map_content);
           // ActionBar.SetDisplayShowHomeEnabled(true);

            if (this.Intent.Extras.ContainsKey("Lat"))
            {
                _Lat = this.Intent.Extras.GetDouble("Lat");
            }

            if (this.Intent.Extras.ContainsKey("Long"))
            {
                _Long = this.Intent.Extras.GetDouble("Long");
            }

            if (this.Intent.Extras.ContainsKey("Adress"))
            {
                _Adress = this.Intent.Extras.GetString("Adress");
            }

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

           
            Toast.MakeText(this, IsPlayServicesAvailable(), ToastLength.Short).Show();
        }
        

        void AddMarkersToMap()
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(_Lat, _Long))
                      .SetTitle(_Adress)
                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            googleMap.AddMarker(marker);

            // We create an instance of CameraUpdate, and move the map to it.
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(new LatLng(_Lat, _Long), 17);
            googleMap.MoveCamera(cameraUpdate);
        }

        
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    NavUtils.NavigateUpFromSameTask(this);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}