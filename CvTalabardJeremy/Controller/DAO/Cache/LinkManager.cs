using Android.Content;
using TalabardJeremyCv.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.DAO
{
    public class LinkManager
    {

        private Timer _Timer;

        private long _Time;

        private Context _Context;

        private IList<Link> _Links;

        public LinkManager(Context context)
        {
            _Context = context;
            _Links = GetLinks();
            _Time = new TimeManager(_Context).GetTime();
            if(_Time> 0)
            {
                _Timer = LoadTimer(_Time);
            }
            
        }


        private Timer LoadTimer(long time)
        {
            Timer Timer = new Timer(time);
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
            return Timer;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //reload list
            _Links = DataManager.GetInstance(_Context).Links();
            _Timer = LoadTimer(_Time);
        }

        private IList<Link>  GetLinks()
        {
            if (_Links != null && _Links.Count != 0)
            {
                _Links = DataManager.GetInstance(_Context).Links();
            }
            return _Links;
        }

        public List<Link> GetLinksOfActivity(int linksId)
        {
            return GetLinks().Where(l => l.IdLinks == linksId).ToList();
        }

        public List<Link> GetLinksOfActivity(Activity act)
        {
            return GetLinks().Where(l => l.IdLinks == act.IdLinks).ToList();
        }
    }
}
