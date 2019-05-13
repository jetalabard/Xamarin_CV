using System.Xml;

namespace Cv_Core.DataModel
{
    public class Hobie : Activity
    {
        public Hobie() : base(null)
        {
        }

        public Hobie(XmlNode node) : base(node) { }
    }
}
