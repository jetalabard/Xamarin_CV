using System;
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
    public class KnowledgesLanguageFragment : Android.App.Fragment
    {
        private List<Knowledge> list;


        public KnowledgesLanguageFragment()
        {
            list = DataManager.GetInstance().Knowledges().Where(knowledge => knowledge.Type.Equals("language")).ToList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.knowledgeLanguage, container, false);
            GridView galleryL = view.FindViewById<GridView>(Resource.Id.galleryL);
            galleryL.Adapter = new ImageAdapter(this.Activity, list);
            galleryL.ItemClick += (s, e) =>
            {
                Toast.MakeText(Activity.ApplicationContext, list[e.Position].Name, ToastLength.Short).Show();
            };
            return view;
        }

        internal static Android.App.Fragment NewInstance()
        {
            return new KnowledgesLanguageFragment();
        }
    }
}