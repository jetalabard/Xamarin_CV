using Cv_Core.DataModel;
using System.Collections.Generic;

namespace Cv_Core.DataManagement
{
    public interface IDataAccess
    {
        ICollection<Hobie> Hobies();
        ICollection<Job> Jobs();
        ICollection<Project> Projects();
        ICollection<PersonalProject> PersonalProjects();
        ICollection<Training> Trainings();

        ICollection<Link> Links();
        ICollection<Header> Headers();
        ICollection<Knowledge> Knowledges();

        Description Description();
        Cv Cv();
    }
}
