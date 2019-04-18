using Android.Content;
using System;
using System.Collections.Generic;
using System.Timers;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.DAO
{
    public class ListTimer<T> where T :Activity
    {
        public IList<T> List;

        private Timer _Timer;

        private long _Time;
        private Context _Context;


        public ListTimer(Context context, long timer = 300000)
        {
            _Context = context;
            _Time = timer;
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

        public ListTimer(Context context, List<T> activities, long timer) : this(context,timer) 
        {
            this.List = activities;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //reload list
            this.List = (List<T>) DataManager.GetInstance(_Context).GetActivities<T>();
            this._Timer = LoadTimer(_Time);
        }
    }
}
