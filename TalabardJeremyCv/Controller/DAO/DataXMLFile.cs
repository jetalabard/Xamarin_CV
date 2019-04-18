using System.Collections.Generic;
using System.Linq;
using TalabardJeremyCv.Controller.Services;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv.Controller.DAO
{
    internal class DataXMLFile : IDataAccess
    {
        private XmlReader _Reader;

        public DataXMLFile(string fileName)
        {
            _Reader = new XmlReader(fileName);
        }


        public Cv Cv()
        {
            return _Reader.Documents.FirstOrDefault();
        }

        public Description Description()
        {
            return _Reader.Descriptions.FirstOrDefault();
        }

        public IList<Header> Headers()
        {
            return _Reader.Heads.ToList();
        }

        public IList<Hobie> Hobies()
        {
            IList<Hobie> Hobies = _Reader.Hobies.ToList();
            foreach(Hobie h in Hobies)
            {
                h.Links = Links().Where(link => link.IdLinks == h.IdLinks).ToList();
            }
            return Hobies;
        }

        public IList<Job> Jobs()
        {
            IList<Job> Jobs = _Reader.Jobs.ToList();
            foreach (Job h in Jobs)
            {
                h.Links = Links().Where(link => link.IdLinks == h.IdLinks).ToList();
            }
            return Jobs;
        }

        public IList<Knowledge> Knowledges()
        {
            return _Reader.Knowledges.ToList();
        }

        public IList<Link> Links()
        {
            return _Reader.Links.ToList();
        }

        public IList<PersonalProject> PersonalProjects()
        {
            IList<PersonalProject> PersonalProjects = _Reader.PersonalProjects.ToList();
            foreach (PersonalProject h in PersonalProjects)
            {
                h.Links = Links().Where(link => link.IdLinks == h.IdLinks).ToList();
            }
            return PersonalProjects;
        }

        public IList<Project> Projects()
        {
            IList<Project> Projects = _Reader.Projects.ToList();
            foreach (Project h in Projects)
            {
                h.Links = Links().Where(link => link.IdLinks == h.IdLinks).ToList();
            }
            return Projects;
        }

        public IList<Training> Trainings()
        {
            IList<Training> Trainings = _Reader.Trainings.ToList();
            foreach (Training h in Trainings)
            {
                h.Links = Links().Where(link => link.IdLinks == h.IdLinks).ToList();
            }
            return Trainings;
        }
    }
}