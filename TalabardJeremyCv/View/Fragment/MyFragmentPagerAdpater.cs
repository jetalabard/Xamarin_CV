using System.Collections.Generic;
using Cv_Core;
using Java.Lang;

namespace TalabardJeremyCv.XView.Fragment
{
    public class MyFragmentPagerAdapter : Android.Support.V13.App.FragmentPagerAdapter
    {
        private static List<Android.App.Fragment> fgls = new List<Android.App.Fragment>();
        public MyFragmentPagerAdapter(Android.App.FragmentManager fm)
            : base(fm)
        {
            fgls.Add(KnowledgesLanguageFragment.NewInstance());
            fgls.Add(KnowledgesToolFragment.NewInstance());
        }

        public override Android.App.Fragment GetItem(int position)
        {
            return fgls[position];
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new String(Constants.TITLES_KNOWLEDGES[position]);
        }
        public override int Count
        {
            get { return Constants.TITLES_KNOWLEDGES.Length; }
        }


    }
}