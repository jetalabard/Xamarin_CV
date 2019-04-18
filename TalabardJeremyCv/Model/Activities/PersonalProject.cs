using System.Xml;
using Newtonsoft.Json;

namespace TalabardJeremyCv.Model
{
    public class PersonalProject : Activity
    {
        public PersonalProject() : base(null)
        {
        }

        public PersonalProject(XmlNode node) : base(node) { }
    }
}
