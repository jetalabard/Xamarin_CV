using System.Linq;
using System.Collections.Generic;
using Cv_Core.DataModel;
using Cv_Core.ConfigurationManagement;

namespace Cv_Core.DataManagement
{
    public class DataManager : IDataAccess
    {
        private static DataManager _Instance;
 
        private static IDataAccess _DataAccess;
        
        public static DataManager GetInstance(bool IsDev = false,string filename = "")
        {
            if (_Instance == null || _DataAccess == null)
            {
                _Instance = new DataManager(IsDev, filename);
            }
            return _Instance;
        }

       private DataManager(bool IsDev = false, string filename = "")
        {
            if (IsDev == true)
            {
                _DataAccess = new MockData();
            }
            else
            {
                _DataAccess = new DataXMLFile(filename);
            }

        }

      
        public IList<Hobie> Hobies()
        {
            return _DataAccess.Hobies();
        }

        public IList<Job> Jobs()
        {
            return _DataAccess.Jobs();
        }

        public IList<Project> Projects()
        {
            return _DataAccess.Projects();
        }

        public IList<PersonalProject> PersonalProjects()
        {
            return _DataAccess.PersonalProjects();
        }

        public IList<Training> Trainings()
        {
            return _DataAccess.Trainings();
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
            return list.OrderByDescending(l => l.Id).ToList();
        }


    }
}
