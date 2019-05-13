using System.Xml;

namespace Cv_Core.DataModel
{
    public class Job : Activity
    {
        public Job() : base(null)
        {
        }

        public Job(XmlNode node) : base(node) { }
    }
}
