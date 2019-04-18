using System.Xml;

namespace TalabardJeremyCv.Model
{
    public class Job : Activity
    {
        public Job() : base(null)
        {
        }

        public Job(XmlNode node) : base(node) { }
    }
}
