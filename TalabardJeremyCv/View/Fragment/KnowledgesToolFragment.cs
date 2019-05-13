using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cv_Core.DataManagement;
using Cv_Core.DataModel;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.XView.Fragment
{
    public class KnowledgesToolFragment : Android.App.Fragment
    {
        private List<Knowledge> list;

        public KnowledgesToolFragment()
        {
            list = DataManager.GetInstance().Knowledges().Where(knowledge => knowledge.Type.Equals("tool")).ToList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.knowledgeTool, container, false);
            GridView galleryT = view.FindViewById<GridView>(Resource.Id.galleryT);
            galleryT.Adapter = new ImageAdapter(this.Activity, list);
            galleryT.ItemClick += (s, e) =>
            {
                Toast.MakeText(Activity, list[e.Position].Name, ToastLength.Short).Show();
            };

            return view;
        }

        internal static Android.App.Fragment NewInstance()
        {
            return new KnowledgesToolFragment();
        }
    }
}