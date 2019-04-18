using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.IO;
using TalabardJeremyCv.Controller.DAO;
using TalabardJeremyCv.Controller.Downloader;
using TalabardJeremyCv.Controller.Services;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.XView.XFragment
{
    public class DescriptionFragment : Fragment
    {
        private Description _description;

        public static DescriptionFragment NewInstance()
        {
            return new DescriptionFragment ();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _description = DataManager.GetInstance().Description();
           
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.description, container, false);

           ImageView image =  view.FindViewById<ImageView>(Resource.Id.ProfilImage);
           TextView descriptionLabel =  view.FindViewById<TextView>(Resource.Id.labelDescription);
           TextView TitleLabel = view.FindViewById<TextView>(Resource.Id.labelTitle);
            if (_description != null)
            {
                

                descriptionLabel.SetText(_description.Summary, TextView.BufferType.Normal);
                TitleLabel.SetText(_description.Title, TextView.BufferType.Normal);

                image.SetImageBitmap(ImageManager.GetBitmapFromPath(System.IO.Path.Combine(System.IO.Path.Combine(Downloader.ExternalStorage, Constants.DOWNLOAD_DIRECTORY), _description.Image)));
            }


            return view;
        }
    }
}