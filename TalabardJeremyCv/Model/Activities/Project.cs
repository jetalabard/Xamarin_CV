using System.Xml;
using Newtonsoft.Json;

namespace TalabardJeremyCv.Model
{
    public class Project :Activity
    {
        public Project() : base(null)
        {
        }

        public Project(XmlNode node) : base(node) { }
    }
}
