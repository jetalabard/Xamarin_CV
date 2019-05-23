using Cv_Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cv_Core.DataManagement
{
    public class DataXMLFile : IDataAccess
    {
        private XmlReader _Reader;

        public DataXMLFile(string fileName)
        {
            try
            {
                _Reader = new XmlReader(fileName);
            }
            catch(ArgumentException)
            {
                _Reader = null;
            }
           
        }

        public Cv Cv()
        {
            return _Reader?.Documents?.FirstOrDefault();
        }

        public Description Description()
        {
            return _Reader?.Descriptions?.FirstOrDefault();
        }

        public ICollection<Header> Headers()
        {
            return _Reader?.Heads;
        }

        public ICollection<Hobie> Hobies()
        {
            return AddLinksToActivities(_Reader?.Hobies);
        }

        public ICollection<Job> Jobs()
        {
            return AddLinksToActivities(_Reader?.Jobs);
        }

        public ICollection<Knowledge> Knowledges()
        {
            return _Reader?.Knowledges;
        }

        public ICollection<Link> Links()
        {
            return _Reader?.Links;
        }

        public ICollection<PersonalProject> PersonalProjects()
        {
            return AddLinksToActivities(_Reader?.PersonalProjects);
        }

        public ICollection<Project> Projects()
        {
            return AddLinksToActivities(_Reader?.Projects);
        }

        public ICollection<Training> Trainings()
        {
            return AddLinksToActivities(_Reader?.Trainings);
        }

        private ICollection<T> AddLinksToActivities<T>(ICollection<T> activities) where T :Activity
        {
            if(activities != null)
            {
                foreach (T act in activities)
                {
                    act.Links = AddDependantLinks(act);
                }
            }
           
            return activities;
        }

        private List<Link> AddDependantLinks<T>(T act) where T : Activity
        {
            return Links().Where(link => link.IdLinks == act.IdLinks).ToList();
        }
    }
}