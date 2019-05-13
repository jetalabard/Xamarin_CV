using Cv_Core.DataModel;
using System.Collections.Generic;

namespace Cv_Core.DataManagement
{
    public interface IDataAccess
    {
        IList<Hobie> Hobies();
        IList<Job> Jobs();
        IList<Project> Projects();
        IList<PersonalProject> PersonalProjects();
        IList<Training> Trainings();

        IList<Link> Links();
        IList<Header> Headers();
        IList<Knowledge> Knowledges();

        Description Description();
        Cv Cv();
    }
}
