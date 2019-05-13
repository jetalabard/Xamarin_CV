using System.Xml;
using Newtonsoft.Json;

namespace Cv_Core.DataModel
{
    public class Project :Activity
    {
        public Project() : base(null)
        {
        }

        public Project(XmlNode node) : base(node) { }
    }
}
