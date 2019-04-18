using Android.Content;
using TalabardJeremyCv.Controller.Services;

namespace TalabardJeremyCv.Model
{
    public class TimeManager
    {
        private Context Context;

        public TimeManager(Context context)
        {
            this.Context = context;
        }

        private long ConvertStringToTime(string timeDescription)
        {
            long time = 0;

            if (timeDescription.Contains("5 min"))
            {
                time = 60 * 5 * 1000;
            }
            else if (timeDescription.Contains("30 sec"))
            {
                time = 30 * 1000;
            }
            return time;
        }

        public bool HasRefresh()
        {
            string timeDescription = SharedPreferenceManager.GetString(this.Context, Constants.REFRESH_MODE_PREFERENCES);
            return ConvertStringToTime(timeDescription) > 0;
        }

        public long GetTime()
        {
            string timeDescription = SharedPreferenceManager.GetString(this.Context, Constants.REFRESH_MODE_PREFERENCES);
            return ConvertStringToTime(timeDescription) ;
        }
    }
}