using System.Xml;
using Newtonsoft.Json;

namespace Cv_Core.DataModel
{
    public class PersonalProject : Activity
    {
        public PersonalProject() : base(null)
        {
        }

        public PersonalProject(XmlNode node) : base(node) { }
    }
}
