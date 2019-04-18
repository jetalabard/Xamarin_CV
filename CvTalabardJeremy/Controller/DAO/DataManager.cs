using Android.Content;
using TalabardJeremyCv.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.DAO
{
    public class DataManager : IDataAccess
    {
        private static DataManager _Instance;
 
        private static long _Time = 0;

        private static Context _Context;

        private static IDataAccess _DataAccess;

        private static LinkManager _LinkManager;

        private static IDictionary<Type,ListTimer<Activity>> _Activities = new Dictionary<Type,ListTimer<Activity>>() ;
        
        public static DataManager GetInstance(Context context)
        {
            _Context = context;
            _LinkManager = new LinkManager(_Context);
            SetTime(_Context);
            if (_Instance == null)
            {
                _Instance = new DataManager();
            }
            return _Instance;
        }

       private DataManager()
        {
            if (Constants.DEV == true)
            {
                _DataAccess = new MockData();
            }
            else
            {
                _DataAccess = new WebServiceManager();
            }

        }

        private static void SetTime(Context context)
        {
            if(context != null)
            {
                _Time = new TimeManager(context).GetTime();
            }
            else
            {
                _Time = 0;
            }
            
        }


        public IList<Hobie> Hobies()
        {
            if (!_Activities.ContainsKey(typeof(Job)))
            {
                _Activities.Add(typeof(Job), new ListTimer<Activity>(_Context,(List<Activity>)_DataAccess.Hobies(), _Time));
            }
            return (List<Hobie>)_Activities[typeof(Hobie)].List;

        }

        public IList<Job> Jobs()
        {
            if (!_Activities.ContainsKey(typeof(Job)))
            {
                _Activities.Add(typeof(Job), new ListTimer<Activity>(_Context, (List<Activity>)_DataAccess.Jobs(), _Time));
            }
            return (List<Job>)_Activities[typeof(Job)].List; ;
        }

        public IList<Project> Projects()
        {
            if (!_Activities.ContainsKey(typeof(Project)))
            {
                _Activities.Add(typeof(Project), new ListTimer<Activity>(_Context, (List<Activity>)_DataAccess.Projects(), _Time));
            }
            return (List<Project>)_Activities[typeof(Project)].List;
        }

        public IList<PersonalProject> PersonalProjects()
        {
            if (!_Activities.ContainsKey(typeof(PersonalProject)))
            {
                _Activities.Add(typeof(PersonalProject), new ListTimer<Activity>(_Context, (List<Activity>)_DataAccess.PersonalProjects(), _Time));
            }
            return (List<PersonalProject>)_Activities[typeof(PersonalProject)].List;
        }

        public IList<Training> Trainings()
        {
            if (!_Activities.ContainsKey(typeof(Training)))
            {
                _Activities.Add(typeof(Training), new ListTimer<Activity>(_Context, (List<Activity>)_DataAccess.Trainings(), _Time));
            }
            return (List<Training>)_Activities[typeof(Training)].List;
        }

        public IList<Link> Links()
        {
            return _DataAccess.Links();
        }

        public IList<Header> Headers()
        {
            return _DataAccess.Headers();
        }

        public IList<Knowledge> Knowledges()
        {
            return _DataAccess.Knowledges();
        }

        public Description Description()
        {
            return _DataAccess.Description();
        }

        public Cv Cv()
        {
            return _DataAccess.Cv();
        }


         public IList<T> GetActivities<T>() where T :Activity
         {
            IList<T> list = null;
            if(typeof(T) == typeof(Hobie))
            {
                list = (List<T>)Hobies();
            }
            else if (typeof(T) == typeof(Job))
            {
                list = (List<T>)Jobs();
            }
            else if(typeof(T) == typeof(Project))
            {
                list = (List<T>)Projects();
            }
            else if(typeof(T) == typeof(PersonalProject))
            {
                list = (List<T>)PersonalProjects();
            }
            else if(typeof(T) == typeof(Training))
            {
                list = (List<T>)Trainings();
            }
            return list;
        }


    }
}
