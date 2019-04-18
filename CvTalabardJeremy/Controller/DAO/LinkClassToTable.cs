using System;
using System.Collections.Generic;
using System.Text;
using TalabardJeremyCv.Model;

namespace TalabardJeremyCv
{
    public static class LinkClassToTable
    {
        public static Dictionary<string, Type> Get = new Dictionary<string, Type>
        {
            {Constants.TABLE_DESCRIPTION,typeof(Description)},
            {Constants.TABLE_DOCUMENT,typeof(Cv)},
            {Constants.TABLE_HEAD,typeof(Header)},
            {Constants.TABLE_HOBIE,typeof(Hobie)},
            {Constants.TABLE_JOB,typeof(Job)},
            {Constants.TABLE_KNOWLEDGE,typeof(Knowledge)},
            {Constants.TABLE_LINK,typeof(Link)},
            {Constants.TABLE_PERSONALPROJECT,typeof(PersonalProject)},
            {Constants.TABLE_PROJECT,typeof(Project)},
            {Constants.TABLE_TRAINING,typeof(Training)}
        };
    }
}
