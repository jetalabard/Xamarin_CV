using System.Linq;
using System.Collections.Generic;
using Cv_Core.DataModel;

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

      
        public ICollection<Hobie> Hobies()
        {
            return _DataAccess.Hobies();
        }

        public ICollection<Job> Jobs()
        {
            return _DataAccess.Jobs();
        }

        public ICollection<Project> Projects()
        {
            return _DataAccess.Projects();
        }

        public ICollection<PersonalProject> PersonalProjects()
        {
            return _DataAccess.PersonalProjects();
        }

        public ICollection<Training> Trainings()
        {
            return _DataAccess.Trainings();
        }

        public ICollection<Link> Links()
        {
            return _DataAccess.Links();
        }

        public ICollection<Header> Headers()
        {
            return _DataAccess.Headers();
        }

        public ICollection<Knowledge> Knowledges()
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


         public ICollection<T> GetActivities<T>() where T :Activity
         {
            ICollection<T> list = null;
            if(typeof(T) == typeof(Hobie))
            {
                list = (ICollection<T>)Hobies();
            }
            else if (typeof(T) == typeof(Job))
            {
                list = (ICollection<T>)Jobs();
            }
            else if(typeof(T) == typeof(Project))
            {
                list = (ICollection<T>)Projects();
            }
            else if(typeof(T) == typeof(PersonalProject))
            {
                list = (ICollection<T>)PersonalProjects();
            }
            else if(typeof(T) == typeof(Training))
            {
                list = (ICollection<T>)Trainings();
            }
            return list?.OrderByDescending(l => l.Id).ToList();
        }


    }
}
